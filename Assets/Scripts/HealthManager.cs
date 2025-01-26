using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] healthIcons;   // Array of Health Icons (heart images)
    public Sprite fullHeart;      // Full Heart Sprite
    public Sprite emptyHeart;     // Empty Heart Sprite

    private int currentHealth = 4;  // Player's current health (e.g., 5 hearts)
    private int maxHealth = 4;      // Maximum health (5 hearts)

public Image fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthUI();  // Initialize health UI when the game starts
    }

    // Update health UI to match the current health
    public void UpdateHealthUI()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                healthIcons[i].sprite = fullHeart;  // Set full heart icon
            }
            else
            {
                healthIcons[i].sprite = emptyHeart; // Set empty heart icon
            }
        }
    }

    int amount=1;
    // Reduce health by the specified amount
    public void ReduceHealth()
    {
        currentHealth -= amount;

        if (currentHealth <= 0){
              StartCoroutine(DieAndLoadEndGame());
      
        }

        UpdateHealthUI();  // Update health UI
    }


     // Coroutine for fade out effect
    private IEnumerator DieAndLoadEndGame()
    {
        // Fade in (make the screen black gradually)
        float timeElapsed = 0f;
        var fadeDuration= 5;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha); // Change the alpha to fade in
            yield return null;
        }

        // Now that the fade is complete, load the next scene
        SceneManager.LoadScene("EndScene");
    }

    // Increase health by the specified amount
    public void IncreaseHealth()
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();  // Update health UI
    }
}
