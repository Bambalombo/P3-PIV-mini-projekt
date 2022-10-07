using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject enemyContainer;
    public GameObject enemyPrefab;
    
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
    

    void Start()
    {
        Cursor.visible = false;
        enemyContainer = GameObject.FindWithTag("EnemyContainer");
        _currentEnemies = (float)enemyContainer.transform.childCount;
        Debug.Log(_currentEnemies);
        _spawnDelay = 20 / enemySpawnLimit;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        EnemySpawnController();
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

        if (/*currentEnemies < enemySpawnLimit && */_spawnInterval > _spawnDelay)
        {
            //Debug.Log(" reached");
            
            float xPos = UnityEngine.Random.Range(40f, 100f);
            if (UnityEngine.Random.value > 0.5f) xPos *= -1;
            
            float zPos = UnityEngine.Random.Range(40f, 100f);
            if (UnityEngine.Random.value > 0.5f) zPos *= -1;
            
            Vector3 spawnPos = new Vector3(xPos, 25, zPos);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity,enemyContainer.transform);
            
            _currentEnemies = enemyContainer.transform.childCount;
            _spawnInterval = 0;

        }
    }
}