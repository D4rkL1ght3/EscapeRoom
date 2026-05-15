using System.Collections.Generic;
using System.Collections;
using TMPro;
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
    public AudioClip slidingSound;
    public AudioClip bookClickSound;

    [Header("Puzzle Lock")]
    public bool bookshelfUnlocked = false;
    public GameObject hintPopup;

    private Coroutine hintPopupCoroutine;

    public void PressBook(int bookID)
    {
        if (solved)
            return;

        if (!bookshelfUnlocked)
        {
            ShowLockedHint();
            TimerManager.Instance.DeductTime(5f);
            return;
        }

        playerSequence.Add(bookID);
        
        if (audioSource != null && bookClickSound != null)
            audioSource.PlayOneShot(bookClickSound);

        int currentIndex = playerSequence.Count - 1;

        // Wrong input
        if (playerSequence[currentIndex] != correctSequence[currentIndex])
        {
            Debug.Log("Wrong order!");

            ResetPuzzle();
            TimerManager.Instance.DeductTime(10f);

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

        if (audioSource != null && slidingSound != null)
            audioSource.PlayOneShot(slidingSound);

        ClosePuzzleUI();
    }

    void ResetPuzzle()
    {
        playerSequence.Clear();
    }

    void ShowLockedHint()
    {
        if (hintPopupCoroutine != null)
            StopCoroutine(hintPopupCoroutine);

        hintPopupCoroutine = StartCoroutine(ShowHintPopup());

        Debug.Log("Complete the math book puzzle first!");
    }

    public void UnlockBookshelfPuzzle()
    {
        bookshelfUnlocked = true;

        Debug.Log("Bookshelf puzzle unlocked!");
    }

    IEnumerator ShowHintPopup()
    {
        if (hintPopup == null)
            yield break;

        hintPopup.SetActive(true);

        yield return new WaitForSeconds(3f);

        hintPopup.SetActive(false);
    }

    public void ClosePuzzleUI()
    {
        bookshelfUI.SetActive(false);
        playerController.enabled = true;
    }
}