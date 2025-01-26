using UnityEngine;

public class BubbleSpawnerStartScreen : MonoBehaviour
{
    public GameObject bubblePrefab; // Bubble prefab to spawn
    public float spawnInterval = 1f; // Time between spawning bubbles
    public float spawnHeight = 10f; // Height from which bubbles will spawn
    public float mergeDistance = 0.5f; // Distance to check if two bubbles should merge

    private void Start()
    {
        InvokeRepeating("SpawnBubble", 0f, spawnInterval); // Start spawning bubbles at intervals
    }

    private void Update()
    {
        // Check for nearby bubbles to merge (optional)
        CheckForMergingBubbles();
    }

    void SpawnBubble()
    {
        // Instantiate a bubble at a random position at the top of the screen
        float randomX = Random.Range(-6f, 6f);
        GameObject newBubble = Instantiate(bubblePrefab, new Vector2(randomX, spawnHeight), Quaternion.identity);

        // Assign a random size to the bubble
        int randomSize = Random.Range(1, 4);
        newBubble.GetComponent<Bubble>().SetSize( randomSize);
        newBubble.GetComponent<Bubble>().UpdateSize();
    }

    void CheckForMergingBubbles()
    {
        // Find all bubbles in the scene and check for nearby ones to merge
        Bubble[] allBubbles = FindObjectsByType<Bubble>(FindObjectsSortMode.None);

        for (int i = 0; i < allBubbles.Length; i++)
        {
            for (int j = i + 1; j < allBubbles.Length; j++)
            {
                float distance = Vector2.Distance(allBubbles[i].transform.position, allBubbles[j].transform.position);

                // If the bubbles are close enough, merge them
                if (distance <= mergeDistance)
                {
                    allBubbles[i].MergeBubble(allBubbles[j]);
                }
            }
        }
    }
}
