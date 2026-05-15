using UnityEngine;
using System.Collections;
using TMPro;


public class CardReaderPuzzle : MonoBehaviour
{
    [Header("UI")]
    public GameObject hintPopup;
    public GameObject keycardButton;
    public GameObject cardReaderUI;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip doorOpenSound;

    [Header("Player")]
    public MonoBehaviour playerController;

    [Header("Exit Door")]
    public GameObject doorLayer2;
    public GameObject exitDoor;
    public SpriteRenderer roomSprite;
    public Sprite doorOpen;

    public bool hasKeycard = false;
    private bool opened = false;

    private Coroutine hintPopupCoroutine;

    public void Interact()
    {
        if (opened)
            return;

        if (!hasKeycard)
        {
            if (hintPopupCoroutine != null)
                StopCoroutine(hintPopupCoroutine);

            hintPopupCoroutine = StartCoroutine(ShowHintPopup());

            Debug.Log("Door locked.");
            return;
        }

        OpenDoor();
    }

    IEnumerator ShowHintPopup()
    {
        if (hintPopup == null)
            yield break;

        hintPopup.SetActive(true);

        yield return new WaitForSeconds(3f);

        hintPopup.SetActive(false);
    }

    void OpenDoor()
    {
        opened = true;

        audioSource.PlayOneShot(doorOpenSound);

        roomSprite.sprite = doorOpen;
        doorLayer2.SetActive(false);
        exitDoor.SetActive(true);

        CloseUI();

        Debug.Log("Door Opened!");
    }

    public void CloseUI()
    {
        cardReaderUI.SetActive(false);
        playerController.enabled = true;
    }
}
