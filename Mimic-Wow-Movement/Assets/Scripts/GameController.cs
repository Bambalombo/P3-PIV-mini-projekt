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
    private float timeInterval = 0;
    private float currentEnemies;
    private float spawnDelay;
    

    void Start()
    {
        Cursor.visible = false;
        enemyContainer = GameObject.FindWithTag("EnemyContainer");
        currentEnemies = (float)enemyContainer.transform.childCount;
        Debug.Log(currentEnemies);
        spawnDelay = 10 / enemySpawnLimit;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        EnemySpawnController();
    }

    private void EnemySpawnController()
    {
        timeInterval+= Time.deltaTime;

        if (timeInterval > timeBuffer)
        {
            Debug.Log("Time Interval reached");
            enemySpawnLimit += enemyIncrement;
            spawnDelay = 10/enemySpawnLimit;
            timeInterval = 0;
        }

        if (currentEnemies < enemySpawnLimit && timeInterval > spawnDelay)
        {
            //Debug.Log(" reached");
            
            float xPos = UnityEngine.Random.Range(40f, 100f);
            if (UnityEngine.Random.value > 0.5f) xPos *= -1;
            
            float zPos = UnityEngine.Random.Range(40f, 100f);
            if (UnityEngine.Random.value > 0.5f) zPos *= -1;
            
            Vector3 spawnPos = new Vector3(xPos, 25, zPos);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity,enemyContainer.transform);
            
            currentEnemies = enemyContainer.transform.childCount;
            
        }
    }
}