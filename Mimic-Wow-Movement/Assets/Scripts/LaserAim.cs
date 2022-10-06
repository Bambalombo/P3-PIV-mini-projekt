using System.Collections;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [Header("Camera Settings")]
    [Range(0.1f,2.0f)]
    public float fireRate = 0.5f;
    private float _fireTimer;

    [Header("Laser Settings")]
    public float gunRange = 50;
    [Range(0.1f,10.0f)]
    public float laserDuration = 0.2f;
    private LineRenderer _laserGunLine;
    private LineRenderer _laserSightLine;
    private bool _laserLineEnabled;

    [Header("Explosion Settings")]
    public float explosionRadius = 100f;
    public float explosionForce = 100f;

    private void Start()
    {
        _laserGunLine = GetComponent<LineRenderer>();
        _laserLineEnabled = _laserGunLine.enabled;
    }

    private void FixedUpdate()
    {
        _fireTimer += Time.fixedDeltaTime;

        if (Input.GetButton("Fire1") && _fireTimer > fireRate)
        {
            _fireTimer = 0;
            var rayOrigin = transform.position;
            _laserGunLine.SetPosition(0, rayOrigin);

            if (Physics.Raycast(rayOrigin, transform.parent.transform.forward, out var hit, gunRange))
            {
                _laserGunLine.SetPosition(1, hit.point);
                
                Collider[] _nearbyObjects = Physics.OverlapSphere(hit.point, explosionRadius);
                foreach (var nearby in _nearbyObjects)
                {
                    var rb = nearby.GetComponent<Rigidbody>();
                    if (rb == null) continue;
                    rb.AddExplosionForce(explosionForce / (hit.point - nearby.transform.position).magnitude, hit.point,
                        explosionRadius);
                }
                _nearbyObjects = null;
            }
            else
                _laserGunLine.SetPosition(1, rayOrigin + transform.parent.transform.forward * gunRange);
            
            StartCoroutine(ShootLaser());
        }
    }

    private IEnumerator ShootLaser()
    {
        _laserLineEnabled = true;
        yield return new WaitForSeconds(laserDuration);
        _laserLineEnabled = false;
    }

    //private IEnumerator AOEExplosion()
    //{
        
    //}
}