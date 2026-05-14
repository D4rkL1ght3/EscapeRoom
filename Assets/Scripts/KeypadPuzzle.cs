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

    [Header("Player")]
    public MonoBehaviour playerController;

    [Header("Door Layers")]
    public GameObject doorLayer1;
    public GameObject doorLayer2;

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

        doorLayer1.SetActive(false);
        doorLayer2.SetActive(true);

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