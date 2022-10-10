using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    private GameController _gameController;
    
    private GameObject _player;
    private Rigidbody _rb;
    private Vector3 _playerPos;
    
    public TextMeshProUGUI killsText;
    
    [Header("Movement Settings")] 
    [Range(0.1f,25.0f)]
    public float movementSpeed;
    public float maxSpeed = 10f;
    public float minSpeed = 2f;
    private float _minMoveDistance = 0.6f;

    [Header("Black Cube Jump Settings")] 
    public float jumpBuffer = 5;
    [Tooltip("The time between jumps of the black cube")]
    public float jumpInterval = 0;

    private static short _currentEnemyNum = 0;
    private static int _enemiesKilled = 0;
    
    enum CubeColor { Green, Red, Black};
    private CubeColor _cubeColor;
 
    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        GameObject canvas = GameObject.FindGameObjectWithTag("KillsText");
        killsText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody>();
        
        gameObject.transform.name += _currentEnemyNum++;

        SetCubeColor(_currentEnemyNum);
        SetupCube(_cubeColor);
    }

    void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        
        bool isGrounded = Physics.Raycast(currentPos, Vector3.down, _minMoveDistance);
        
        if (isGrounded && _cubeColor != CubeColor.Black)
            transform.position = Vector3.MoveTowards(currentPos, _player.transform.position, movementSpeed*Time.fixedDeltaTime);
        else if (!isGrounded && _cubeColor == CubeColor.Black)
            transform.position = Vector3.MoveTowards(currentPos, _player.transform.position, movementSpeed*Time.fixedDeltaTime);

        if (isGrounded && _cubeColor == CubeColor.Black)
            EnemyJump();

        if (transform.position.y < -10 ||
            transform.position.x is > 130 or < -130 ||
            transform.position.z is > 130 or < -130)
        {
            killsText.text = "Kills: "+ ++_enemiesKilled;
            Destroy(gameObject);

            switch (_cubeColor) {
                case CubeColor.Red: _gameController.AddPoints(10);
                    break;
                case CubeColor.Black: _gameController.AddPoints(50);
                    break;
                case CubeColor.Green: _gameController.AddPoints(100);
                    break;
            }
        }
    }

    private void EnemyJump()
    {
        jumpInterval += Time.fixedDeltaTime;

        if (jumpInterval > jumpBuffer)
        {
            _rb.AddForce(new Vector3(0,20,0),ForceMode.VelocityChange);
            jumpInterval = 0;
        }
    }
    
    private void SetCubeColor(short enemyCount)
    {
        if (enemyCount % 10 == 0)
            _cubeColor = CubeColor.Black;
        else if (enemyCount % 15 == 0)
            _cubeColor = CubeColor.Green;
        else
            _cubeColor = CubeColor.Red;
    }
    
    private void SetupCube(CubeColor color)
    {
        if (color == CubeColor.Green)
        {
            GetComponent<Renderer>().material.color = new Color32(0,255,0,255);
            movementSpeed = 8f;
            transform.localScale += new Vector3(3, 3, 3);
            _rb.drag = 0.5f;
            _rb.mass = 30f;
            _minMoveDistance = 2.1f;
        } 
        else if (color == CubeColor.Black)
        {
            GetComponent<Renderer>().material.color = new Color32(0,0,0,255);
            movementSpeed = 15f;
            _minMoveDistance = 1.2f;
            transform.localScale += new Vector3(1, 1, 1);
            _rb.mass = 10f;
        }
        else if (color == CubeColor.Red)
        {
            movementSpeed = UnityEngine.Random.Range(minSpeed,maxSpeed);
            byte colorIntensity = (byte)(255-255*((movementSpeed-minSpeed)/(maxSpeed-minSpeed)));
            GetComponent<Renderer>().material.color = new Color32(255,colorIntensity,colorIntensity,255);
        }
        else
        {
            Debug.Log("How the fuck");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && !_gameController.gameOver)
        {
            Destroy(gameObject);
            
            switch (_cubeColor) {
                case CubeColor.Red: _gameController.DealDamageToPlayer(5);
                    break;
                case CubeColor.Black: _gameController.DealDamageToPlayer(10);
                    break;
                case CubeColor.Green: _gameController.DealDamageToPlayer(50);
                    break;
            }
        }
    }
}
