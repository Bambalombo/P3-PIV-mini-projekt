using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _playerPos;
    
    [Header("Movement Settings")] 
    [Range(0.1f,25.0f)]
    public float movementSpeed;
    public float maxSpeed = 10f;
    public float minSpeed = 2f;

    private void Start()
    {
        movementSpeed = UnityEngine.Random.Range(minSpeed,maxSpeed);
        byte colorIntensity = (byte)(255-(255*movementSpeed/maxSpeed));
        GetComponent<Renderer>().material.color = new Color32(255,colorIntensity,colorIntensity,255);
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        
        bool isGrounded = Physics.Raycast(currentPos, Vector3.down, 0.6f);
        if (isGrounded)
            transform.position = Vector3.MoveTowards(currentPos, _player.transform.position, movementSpeed*Time.fixedDeltaTime);
        
        if (transform.position.y < -10) Destroy(gameObject);
    }
}
