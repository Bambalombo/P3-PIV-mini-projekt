using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameController : MonoBehaviour
{
    private GameObject _enemyContainer;
    public GameObject enemyPrefab;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    private float _scorePoints;
    public UnityEngine.UI.Slider healthbar;
    
    [Header("Player Settings")]
    public float playerHealth = 100f;
    
    [Header("Enemy Spawn Settings")]
    [Tooltip("The amount of enemies increased per interval")]
    public float enemyIncrement = 1;
    [Tooltip("The time it takes for the number of enemies to increase")]
    public float timeBuffer = 10;
    [Tooltip("The initial limit amount of to be spawned")]
    public float enemySpawnLimit = 5;
    private float _incrementInterval = 0;
    private float _spawnInterval = 0;
    private float _currentEnemies;
    private float _spawnDelay;

    private float _scoreTimerInterval;
    private float _scoreTimerBuffer = 0.2f;

    void Start()
    {
        Cursor.visible = false;
        
        _enemyContainer = GameObject.FindWithTag("EnemyContainer");
        _currentEnemies = (float)_enemyContainer.transform.childCount;
        _spawnDelay = 20 / enemySpawnLimit;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        EnemySpawnController();
        
        UpdateScore();
    }

    public void DealDamageToPlayer(float damage = 0)
    {
        Debug.Log("You got hit, shit!");
        
        playerHealth -= damage;

        if (playerHealth < 1)
            EndGame();
        else
            UpdateHealthBar();
        
        Debug.LogWarning("shit", gameObject);
    }

    private void EndGame()
    {
        healthbar.value = 0;
        healthText.text = "0";
        Debug.Log("Game over! ish");
    }
    

    private void UpdateHealthBar()
    {
        healthbar.value = playerHealth;
        healthText.text = "" + playerHealth;
    }
    
    private void UpdateScore()
    {
        _scoreTimerInterval += Time.deltaTime;

        if (_scoreTimerInterval > _scoreTimerBuffer)
        {
            AddPoints(1);
            _scoreTimerInterval = 0;
        }
    }

    public void AddPoints(float points = 0)
    {
        _scorePoints += points;
        scoreText.text = _scorePoints + " points";
    }

    private void EnemySpawnController()
    {
        _incrementInterval += Time.deltaTime;
        _spawnInterval += Time.deltaTime;

        if (_incrementInterval > timeBuffer)
        {
            enemySpawnLimit += enemyIncrement;
            _spawnDelay = 50/enemySpawnLimit;
            _incrementInterval = 0;
        }

        if (_currentEnemies < enemySpawnLimit && _spawnInterval > _spawnDelay)
        {
            float xPos = UnityEngine.Random.Range(40f, 100f);
            if (UnityEngine.Random.value > 0.5f) xPos *= -1;
            
            float zPos = UnityEngine.Random.Range(40f, 100f);
            if (UnityEngine.Random.value > 0.5f) zPos *= -1;
            
            Vector3 spawnPos = new Vector3(xPos, 25, zPos);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity,_enemyContainer.transform);
            
            _currentEnemies = _enemyContainer.transform.childCount;
            _spawnInterval = 0;

        }
    }
}