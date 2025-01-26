using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Use TMPro if using TextMeshPro

public class EndGameManager : MonoBehaviour
{
    public TextMeshProUGUI endMessage; // Drag your Text component here (or TMP_Text for TextMeshPro)
    public string[] messages; // Add ominous messages in the Inspector
    public float messageDisplayTime = 5f; // Time each message shows
   public Button restartButton; // Button to restart the game
    public Button mainMenuButton; // Button to go to the main menu
   
  
    private void Start()
    {
              restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
      
        TriggerEndGameSequence();
     }
  // Method to restart the game
    private void RestartGame()
    {
        // Load the first scene again (Game scene)
        SceneManager.LoadScene("GameScene");
    }

    // Method to return to the main menu
    private void ReturnToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
    public void TriggerEndGameSequence()
    {
        StartCoroutine(ShowMessages());
    }

    private IEnumerator ShowMessages()
    {
        var message = messages[Random.Range(0, messages.Length)];
            DisplayMessage(message);
            yield return new WaitForSeconds(messageDisplayTime);
        

        // Optionally, return to the main menu or restart the game
        EndSequence();
    }

    private void DisplayMessage(string message)
    {
        endMessage.text = message; // Set the message
        endMessage.gameObject.SetActive(true); // Ensure it's visible
       
        StartCoroutine(FadeMessage());
    }

    private IEnumerator FadeMessage()
    {
        CanvasGroup canvasGroup = endMessage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = endMessage.gameObject.AddComponent<CanvasGroup>();
        }

        // Fade-in
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / 1f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Hold message
        yield return new WaitForSeconds(messageDisplayTime - 2f);

        // Fade-out
        elapsed = 0f;
        while (elapsed < 1f)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / 1f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        endMessage.gameObject.SetActive(false);
    }

    private void EndSequence()
    {
        Debug.Log("Endgame sequence completed.");
        // Add logic to return to the main menu or restart the game
    }
}
