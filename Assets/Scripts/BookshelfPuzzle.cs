using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BookshelfPuzzleManager : MonoBehaviour
{
    [Header("Correct Sequence")]
    public int[] correctSequence;

    private List<int> playerSequence = new List<int>();

    [Header("UI")]
    public GameObject puzzleUI;

    private bool solved = false;

    [Header("Player")]
    public MonoBehaviour playerController;

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

        ClosePuzzleUI();
    }

    void ResetPuzzle()
    {
        playerSequence.Clear();
    }

    public void ClosePuzzleUI()
    {
        puzzleUI.SetActive(false);
        playerController.enabled = true;
    }
}