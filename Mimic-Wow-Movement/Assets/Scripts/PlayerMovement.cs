using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Control Values")]
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpHeight = 10;
    [Range(1f,100f)] 
    [SerializeField] private float vLookSpeed = 50f;
    
    private Rigidbody _rb;
    private Transform _transform;
    private Camera _camera;
    private Transform _cameraTransform;
    
    private float _w,_a,_s,_d,_q,_e,_vSpeed,_hSpeed, _mouseX, _vertical, _horizontal;
    private bool _isJumping, _isGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
        moveSpeed *= Time.fixedDeltaTime*100;
    }

    private void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        // Player movement
        Vector3 dirVector = ((_transform.forward * _vertical) + (_transform.right * _horizontal)).normalized;
        _rb.velocity = new Vector3(dirVector.x * moveSpeed, _rb.velocity.y, dirVector.z * moveSpeed);
        
        // Player Rotation
        _transform.Rotate(Vector3.up * (_mouseX * vLookSpeed));

        // Player Jump
        GroundCheck();
        if (_isJumping)
        {
            _rb.velocity += (new Vector3(0, jumpHeight, 0));
            _isJumping = false;
        }
    }

    private void GetInputs()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        //_mouseY = Input.GetAxis("Mouse Y");

        if (_isGrounded && Input.GetButtonDown("Jump")) _isJumping = true;
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.01f);
    }
}
