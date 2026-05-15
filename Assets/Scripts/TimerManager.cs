using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Header("Timer Settings")]
    public float startingTime = 300f; // 5 minutes
    public bool timerRunning = true;

    [Header("UI")]
    public TMP_Text timerText;
    public GameObject gameOverUI;

    private float currentTime;
    private bool timeEnded = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

        timerText.text = minutes.ToString("0") + ":" + seconds.ToString("00");
    }

    void EndTimer()
    {
        timeEnded = true;
        timerRunning = false;

        Debug.Log("Time's up!");

        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene("MainMenu");
    }
}