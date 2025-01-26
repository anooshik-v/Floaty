using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // UI Text element to display the score
    private int score = 0;
    [SerializeField] private int bonusThreshold = 100; // Points needed for a bonus
    [SerializeField] private int bonusPoints = 50; // Bonus points awarded

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Score updated! Current score: {score}");

        // Check for bonus
        if (score >= bonusThreshold)
        {
            GrantBonus();
        }

        UpdateScoreUI();
    }
    private void GrantBonus()
    {
        score += bonusPoints;
        Debug.Log($"Bonus granted! Added {bonusPoints} points.");
        bonusThreshold += 100; // Increase threshold for the next bonus (optional)
    }
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
    public int GetScore()
    {
        return score;
    }
}
