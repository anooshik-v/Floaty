using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;       // Reference to the UI Text for instructions
    public GameObject tutorialSection;    // Reference to the spiked enemy prefab
    public GameObject nonSpikedEnemyPrefab; // Reference to the non-spiked enemy prefab
    public GameObject spikedEnemyPrefab;    // Reference to the spiked enemy prefab
    public Transform spawnPoint;    // Where enemies appear
    private PlayerMovement player; // Reference to the player

    private int tutorialStep = 0;   // Tracks the current step in the tutorial
    private bool stepCompleted = false;

    void Start()
    {
        player=FindFirstObjectByType<PlayerMovement>();
        // Start the first tutorial step
         if (PlayerPrefs.GetInt("TutorialCompleted", 0) == 1)
        {
            EndTutorial();
        }
        else
        {
            StartCoroutine(RunTutorial());
        }
    }

    private void EndTutorial()
    {
        PlayerPrefs.SetInt("TutorialCompleted", 1);
        PlayerPrefs.Save();
        player.EndTutorial();
        tutorialSection.SetActive(false);
   
    }

    IEnumerator RunTutorial()
    {
        // Step 1: Explain inflating and deflating the bubble
        tutorialText.text = "Hold SPACE to inflate your bubble. Release to shrink it.";
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Space)); // Wait for player to press Space
        yield return new WaitForSeconds(2f); // Allow them time to practice
        tutorialText.text = "";

        // Step 2: Warn about over-inflation
        tutorialText.text = "Careful! Over-inflating or shrinking too much will pop the bubble.";
        yield return new WaitForSeconds(3f);
        tutorialText.text = "";

        // Step 3: Introduce non-spiked enemies
        tutorialText.text = "Here comes a non-spiked Ghost. Pop it by clicking on it!";
        SpawnEnemy(nonSpikedEnemyPrefab);
        yield return new WaitForSeconds(5f); // Give the player time to pop the enemy
        tutorialText.text = "";

        // Step 4: Introduce spiked enemies
        tutorialText.text = "Watch out! Spiked Poseidon will pop your bubble. Avoid them.";
        SpawnEnemy(spikedEnemyPrefab);
        yield return new WaitForSeconds(4f); // Give time to interact with the spiked enemy
        tutorialText.text = "";

        tutorialText.text = "Yikes! You gain immunity for three seconds after getting popped. Use that time wisely.";
        yield return new WaitForSeconds(5f); // Give time to interact with the spiked enemy
        tutorialText.text = "";
        // Step 5: Complete tutorial
        tutorialText.text = "Good luck! You're ready to play!";
        yield return new WaitForSeconds(3f);
        tutorialText.text = "";

        // Enable the main gameplay loop
       EndTutorial();

    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
