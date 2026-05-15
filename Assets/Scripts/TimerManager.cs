using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Header("Timer Settings")]
    public float startingTime = 300f;
    public bool timerRunning = true;

    [Header("UI")]
    public TMP_Text timerText;

    [Header("Penalty Feedback")]
    public TMP_Text penaltyPopupText;
    public float penaltyPopupDuration = 1f;
    public float flashDuration = 1f;
    public Color normalTimerColor = Color.white;
    public Color penaltyTimerColor = Color.red;

    [Header("Game Over")]
    public GameObject gameOverUI;

    private float currentTime;
    private bool timeEnded = false;

    private Coroutine penaltyPopupCoroutine;
    private Coroutine flashCoroutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentTime = startingTime;

        if (timerText != null)
        {
            normalTimerColor = timerText.color;
        }

        if (penaltyPopupText != null)
        {
            penaltyPopupText.gameObject.SetActive(false);
        }

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
        ShowPenaltyFeedback(amount);

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
        if (timerText == null)
            return;

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = minutes.ToString("0") + ":" + seconds.ToString("00");
    }

    void ShowPenaltyFeedback(float amount)
    {
        if (penaltyPopupCoroutine != null)
        {
            StopCoroutine(penaltyPopupCoroutine);
        }

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        penaltyPopupCoroutine = StartCoroutine(PenaltyPopupCoroutine(amount));
        flashCoroutine = StartCoroutine(FlashTimerCoroutine());
    }

    IEnumerator PenaltyPopupCoroutine(float amount)
    {
        if (penaltyPopupText == null)
            yield break;

        penaltyPopupText.text = "-" + amount.ToString("0") + "s";
        penaltyPopupText.gameObject.SetActive(true);

        yield return new WaitForSeconds(penaltyPopupDuration);

        penaltyPopupText.gameObject.SetActive(false);
    }

    IEnumerator FlashTimerCoroutine()
    {
        if (timerText == null)
            yield break;

        timerText.color = penaltyTimerColor;

        yield return new WaitForSeconds(flashDuration);

        timerText.color = normalTimerColor;
    }

    void EndTimer()
    {
        timeEnded = true;
        timerRunning = false;

        Debug.Log("Time's up!");
        Time.timeScale = 0f; // Pause the game

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f; // Resume time
        Debug.Log("Returning to main menu...");
        SceneManager.LoadScene("MainMenu");
    }
}