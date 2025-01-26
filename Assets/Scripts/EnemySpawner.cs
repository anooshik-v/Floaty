using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemyTypes; // Array of Scriptable Objects
    public GameObject[] enemyPrefabs; // Assign your enemy prefab
    public Vector2 spawnYRange = new Vector2(-4f, 4f); // Y position range for spawning
    private float nextSpawnTime;
    private DifficultyManager difficultyManager;
    void Start()
    {

        difficultyManager = FindFirstObjectByType<DifficultyManager>();
        ScheduleNextSpawn();
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime && difficultyManager.isPlaying)
        {
            SpawnEnemy();
            ScheduleNextSpawn();
        }
    }

    private void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + difficultyManager.CurrentSpawnRate;
    }


    void SpawnEnemy()
    {
        // Randomly select an enemy type and prefab
        int randomIndex = Random.Range(0, enemyTypes.Length);
        EnemyData randomEnemyData = enemyTypes[randomIndex];
        GameObject randomPrefab = enemyPrefabs[randomIndex];

        // Spawn the prefab at a random position
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(spawnYRange.x, spawnYRange.y), 0);
        GameObject newEnemy = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

        // Assign the Scriptable Object data to the prefab
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        enemyScript.enemyData = randomEnemyData;

        Rigidbody2D rb = newEnemy.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.left * difficultyManager.CurrentObstacleSpeed;
        }
    }
}
