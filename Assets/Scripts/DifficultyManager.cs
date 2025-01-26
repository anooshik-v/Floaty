using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
   [Header("Difficulty Settings")]
    [SerializeField] private float initialSpawnRate = 2f; // Initial time between spawns
    [SerializeField] private float minimumSpawnRate = 0.5f; // Minimum spawn interval
    [SerializeField] private float spawnRateReduction = 0.1f; // How much spawn rate decreases per step
    [SerializeField] private float initialObstacleSpeed = 2f; // Initial speed of obstacles
    [SerializeField] private float maxObstacleSpeed = 10f; // Maximum speed of obstacles
    [SerializeField] private float speedIncreaseRate = 0.1f; // How much speed increases per step
    [SerializeField] private float difficultyInterval = 10f; // Time (in seconds) between difficulty increases

public bool isPlaying=false;    
    private float currentSpawnRate;
    private float currentObstacleSpeed;
    private float elapsedTime;

    public float CurrentSpawnRate => currentSpawnRate;
    public float CurrentObstacleSpeed => currentObstacleSpeed;

    private void Start()
    {
        // Initialize variables
        currentSpawnRate = initialSpawnRate;
        currentObstacleSpeed = initialObstacleSpeed;

        // Start the difficulty progression loop
        InvokeRepeating(nameof(IncreaseDifficulty), difficultyInterval, difficultyInterval);
    }

    private void Update()
    {
        // Track elapsed time for difficulty scaling based on time
        elapsedTime += Time.deltaTime;
    }

    private void IncreaseDifficulty()
    {
        // Gradually decrease spawn rate
        if (currentSpawnRate > minimumSpawnRate)
        {
            currentSpawnRate -= spawnRateReduction;
        }

        // Gradually increase obstacle speed
        if (currentObstacleSpeed < maxObstacleSpeed)
        {
            currentObstacleSpeed += speedIncreaseRate;
        }

        Debug.Log($"Difficulty Increased! Spawn Rate: {currentSpawnRate}, Obstacle Speed: {currentObstacleSpeed}");
    }
}
