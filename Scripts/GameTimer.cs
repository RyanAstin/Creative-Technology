using UnityEngine;
using TMPro;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public float maxTime = 30f;        
    public float timeLeft;             

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI countdownText;

    public TargetSpawner spawner;

    private bool gameRunning = false;

    void Start()
    {
        // Hide countdown until StartGame is pressed
        countdownText.gameObject.SetActive(false);

        // Initialize UI timer
        timerText.text = "Time: " + maxTime;
        timeLeft = maxTime;
    }

    void Update()
    {
        // If game isn't running, do not update timer
        if (!gameRunning) return;

        // Count down time
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timeLeft);

        // If time runs out
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            gameRunning = false;

            // Stop spawning targets
            spawner.StopSpawning();

            // Destroy all remaining targets
            DestroyAllTargets();

            // Update high score if needed
            ScoreManager.instance.CheckHighScore();
        }
    }

    public void StartTimer()
    {
        // Begin countdown animation
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        // Display countdown on screen
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1);

        countdownText.text = "2";
        yield return new WaitForSeconds(1);

        countdownText.text = "1";
        yield return new WaitForSeconds(1);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);

        // Hide countdown
        countdownText.gameObject.SetActive(false);

        // Reset timer to full duration
        ResetTimer();

        // Start gameplay timer and spawning
        gameRunning = true;
        spawner.StartSpawning();
    }

    public void ResetTimer()
    {
        // Reset UI timer text
        timeLeft = maxTime;
        timerText.text = "Time: " + Mathf.Ceil(timeLeft);
    }

    void DestroyAllTargets()
    {
        // Find every active target and destroy it
        Target[] remaining = FindObjectsOfType<Target>();
        foreach (Target t in remaining)
            Destroy(t.gameObject);
    }
}
