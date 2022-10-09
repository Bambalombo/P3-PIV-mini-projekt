using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class LaserAim : MonoBehaviour
{
    public GameObject aimController;
    private Transform _player;
    private Vector3 _playerPos;
    
    [Header("Camera Settings")]
    [Range(0.1f,2.0f)]
    public float fireRate = 0.5f;
    private float _fireTimer;

    [Header("Laser Settings")]
    public float gunRange = 50;
    [Range(0.1f,10.0f)]
    public float laserDuration = 0.2f;
    private LineRenderer _laserLine;
    private LineRenderer _aimLine;

    [Header("Explosion Settings")]
    public float explosionRadius = 100f;
    public float explosionForce = 100f;

    private void Start()
    {
        _player = transform.parent;
        
        _laserLine = GetComponent<LineRenderer>();
        _laserLine.enabled = false;
        
        _aimLine = aimController.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (_aimLine.enabled)
            SetAimRangeAndColor();
    }

    private void FixedUpdate()
    {
        _fireTimer += Time.fixedDeltaTime;

        if (Input.GetButton("Fire1") && _fireTimer > fireRate)
        {
            _fireTimer = 0;
            Vector3 originPoint = transform.position;
            
            _laserLine.SetPosition(0, originPoint);

            if (Physics.Raycast(originPoint, transform.parent.transform.forward, out var laserHit, gunRange))
            {
                _laserLine.SetPosition(1, laserHit.point);
                
                Collider[] nearbyObjects = Physics.OverlapSphere(laserHit.point, explosionRadius);
                foreach (var nearby in nearbyObjects)
                {
                    var rb = nearby.GetComponent<Rigidbody>();
                    if (rb == null) continue;
                    rb.AddExplosionForce(explosionForce / (laserHit.point - nearby.transform.position).magnitude, laserHit.point,
                        explosionRadius);
                }
            }
            else
                _laserLine.SetPosition(1, originPoint + transform.parent.transform.forward * gunRange);
            
            StartCoroutine(ShootLaser());
        }
    }

    private IEnumerator ShootLaser()
    {
        _laserLine.enabled = true;
        _aimLine.enabled = false;
        
        yield return new WaitForSeconds(laserDuration);
        
        _laserLine.enabled = false;
        
        if (!Input.GetButton("Fire1"))
            _aimLine.enabled = true;
        else
        {
            yield return new WaitForSeconds(laserDuration);
            _aimLine.enabled = true;
        }
    }

    private void SetAimRangeAndColor()
    {
        _playerPos = _player.position;
        _aimLine.SetPosition(0, _playerPos);

        if (Physics.Raycast(_playerPos, transform.parent.transform.forward, out var aimHit, gunRange))
        {
            _aimLine.SetPosition(1, aimHit.point);
            SetLineColor(_aimLine,Color.red);
        }
        else
        {
            _aimLine.SetPosition(1, _playerPos + _player.transform.forward*gunRange);
            SetLineColor(_aimLine,Color.cyan);
        }
    }
    
    private void SetLineColor(LineRenderer line, Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }
}