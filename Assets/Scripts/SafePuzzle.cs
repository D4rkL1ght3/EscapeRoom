using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafePuzzle : MonoBehaviour
{
    [Header("Code")]
    public string correctCode = "1234";

    private string currentInput = "";

    [Header("UI")]
    public TMP_Text displayText;
    public GameObject safeUI;
    public GameObject keypadUI;
    public GameObject keypadButton;
    public GameObject keycardButton;
    public Image safeDoor;
    public Sprite safeOpen;

    [Header("Player")]
    public MonoBehaviour playerController;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip keypadPressSound;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private bool solved = false;

    void Start()
    {
        UpdateDisplay();
    }

    public void OpenKeypad()
    {
        if (solved)
            return;

        keypadUI.SetActive(true);
        safeUI.SetActive(false);
    }

    public void AddDigit(string digit)
    {
        if (solved)
            return;

        if (currentInput.Length >= 4)
            return;

        currentInput += digit;

        audioSource.PlayOneShot(keypadPressSound);

        UpdateDisplay();
    }

    public void ClearInput()
    {
        currentInput = "";

        UpdateDisplay();
    }

    public void SubmitCode()
    {
        if (currentInput == correctCode)
        {
            SolvePuzzle();
        }
        else
        {
            Debug.Log("Wrong Code!");

            ClearInput();
        }
    }

    void SolvePuzzle()
    {
        solved = true;

        Debug.Log("Safe Unlocked!");

        safeDoor.sprite = safeOpen;
        keypadButton.SetActive(false);
        keycardButton.SetActive(true);

        CloseUI();
    }

    void UpdateDisplay()
    {
        displayText.text = currentInput;
    }

    public void CloseUI()
    {
        if (keypadUI.activeSelf)
        {
            keypadUI.SetActive(false);
            safeUI.SetActive(true);
            return;
        }

        safeUI.SetActive(false);
        playerController.enabled = true;
    }
}
