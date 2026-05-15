using UnityEngine;
using System.Collections;
using TMPro;

public class MathBookPuzzle : MonoBehaviour
{
    [Header("Correct Answers")]
    public string correctX = "255";
    public string correctY = "0";
    public string correctZ = "128";

    [Header("Input Fields")]
    public TMP_InputField xInput;
    public TMP_InputField yInput;
    public TMP_InputField zInput;

    [Header("Bookshelf Puzzle")]
    public BookshelfPuzzle bookshelfPuzzle;
    public GameObject hintPopup;

    private Coroutine hintPopupCoroutine;
    private bool solved = false;

    void Update()
    {
        if (solved)
            return;

        CheckAnswers();
    }

    void CheckAnswers()
    {
        if (xInput == null || yInput == null || zInput == null)
            return;

        string xAnswer = xInput.text.Trim();
        string yAnswer = yInput.text.Trim();
        string zAnswer = zInput.text.Trim();

        bool xCorrect = xAnswer == correctX;
        bool yCorrect = yAnswer == correctY;
        bool zCorrect = zAnswer == correctZ;

        if (xCorrect && yCorrect && zCorrect)
        {
            SolvePuzzle();
        }
    }

    void SolvePuzzle()
    {
        solved = true;

        Debug.Log("Math book puzzle solved!");

        if (hintPopupCoroutine != null)
            StopCoroutine(hintPopupCoroutine);

        hintPopupCoroutine = StartCoroutine(ShowHintPopup());

        if (xInput != null)
            xInput.interactable = false;

        if (yInput != null)
            yInput.interactable = false;

        if (zInput != null)
            zInput.interactable = false;

        if (bookshelfPuzzle != null)
        {
            bookshelfPuzzle.UnlockBookshelfPuzzle();
        }
    }

    IEnumerator ShowHintPopup()
    {
        if (hintPopup == null)
            yield break;

        hintPopup.SetActive(true);

        yield return new WaitForSeconds(5f);

        hintPopup.SetActive(false);
    }
}