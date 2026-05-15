using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startingTime = 300f; // 5 minutes
    public bool timerRunning = true;

    [Header("UI")]
    public TMP_Text timerText;

    private float currentTime;
    private bool timeEnded = false;

    void Start()
    {
        currentTime = startingTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!timerRunning || timeEnded)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndTimer();
        }

        UpdateTimerUI();
    }

    public void DeductTime(float amount)
    {
        if (timeEnded)
            return;

        currentTime -= amount;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndTimer();
        }

        UpdateTimerUI();

        Debug.Log("Time deducted: " + amount + " seconds.");
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void EndTimer()
    {
        timeEnded = true;
        timerRunning = false;

        Debug.Log("Time's up!");

        // Later you can put your game over UI here.
        // Example:
        // gameOverUI.SetActive(true);
        // playerController.enabled = false;
    }
}