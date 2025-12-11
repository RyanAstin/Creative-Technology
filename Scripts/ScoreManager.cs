using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton for easy access

    public int score = 0;
    public int highScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Awake()
    {
        // Assign this as the singleton instance
        instance = this;

        // Load saved high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }

    public void AddScore(int amount)
    {
        // Add points and update UI
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public void CheckHighScore()
    {
        // If new high score, save it
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public void ResetScore()
    {
        // Reset for a new game session
        score = 0;
        scoreText.text = "Score: 0";
    }
}
