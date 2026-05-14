using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BookshelfPuzzle : MonoBehaviour
{
    [Header("Correct Sequence")]
    public int[] correctSequence;

    private List<int> playerSequence = new List<int>();

    private bool solved = false;

    [Header("UI")]
    public GameObject bookshelfUI;

    [Header("Player")]
    public MonoBehaviour playerController;

    [Header("Transitions")]
    public SlidingBookshelf slidingBookshelf;
    public GameObject escapeDoor;

    [Header("Audio")]
    public AudioSource audioSource;

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

        if (slidingBookshelf != null)
            slidingBookshelf.SlideOpen();

        if (escapeDoor != null)
            escapeDoor.SetActive(true);

        if (audioSource != null)
            audioSource.Play();

        ClosePuzzleUI();
    }

    void ResetPuzzle()
    {
        playerSequence.Clear();
    }

    public void ClosePuzzleUI()
    {
        bookshelfUI.SetActive(false);
        playerController.enabled = true;
    }
}