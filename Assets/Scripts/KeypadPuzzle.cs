using UnityEngine;
using TMPro;

public class KeypadPuzzle : MonoBehaviour
{
    [Header("Code")]
    public string correctCode = "1234";

    private string currentInput = "";

    [Header("UI")]
    public TMP_Text displayText;

    public GameObject keypadUI;

    [Header("Door")]
    public Animator doorAnimator;

    [Header("Player")]
    public MonoBehaviour playerController;

    private bool solved = false;

    void Start()
    {
        UpdateDisplay();
    }

    public void AddDigit(string digit)
    {
        if (solved)
            return;

        if (currentInput.Length >= 4)
            return;

        currentInput += digit;

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

        Debug.Log("Door Unlocked!");

        doorAnimator.SetTrigger("Open");

        CloseUI();
    }

    void UpdateDisplay()
    {
        displayText.text = currentInput;
    }

    public void CloseUI()
    {
        keypadUI.SetActive(false);

        playerController.enabled = true;
    }
}