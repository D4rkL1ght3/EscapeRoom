using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BookshelfPuzzleManager : MonoBehaviour
{
    [Header("Correct Sequence")]
    public int[] correctSequence;

    private List<int> playerSequence = new List<int>();

    [Header("Bookshelf")]
    public Animator bookshelfAnimator;

    [Header("UI")]
    public GameObject puzzleUI;

    private bool solved = false;

    public void PressBook(int bookID)
    {
        if (solved)
            return;

        playerSequence.Add(bookID);

        int currentIndex = playerSequence.Count - 1;

        // Wrong input
        if (playerSequence[currentIndex] != correctSequence[currentIndex])
        {
            Debug.Log("Wrong order!");

            ResetPuzzle();

            return;
        }

        // Full correct sequence
        if (playerSequence.Count == correctSequence.Length)
        {
            SolvePuzzle();
        }
    }

    void SolvePuzzle()
    {
        solved = true;

        Debug.Log("Puzzle Solved!");

        bookshelfAnimator.SetTrigger("Open");

        ClosePuzzleUI();
    }

    void ResetPuzzle()
    {
        playerSequence.Clear();
    }

    public void ClosePuzzleUI()
    {
        puzzleUI.SetActive(false);
        Time.timeScale = 1f;
        PauseManager.Instance.IsInUI = false;
    }
}