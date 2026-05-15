using UnityEngine;
using TMPro;


public class CardReaderPuzzle : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text hintText;
    public GameObject keycardButton;
    public GameObject cardReaderUI;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Player")]
    public MonoBehaviour playerController;

    [Header("Exit Door")]
    public GameObject doorLayer2;
    public GameObject exitDoor;
    public SpriteRenderer roomSprite;
    public Sprite doorOpen;

    public bool hasKeycard = false;

    private bool opened = false;

    public void Interact()
    {
        if (opened)
            return;

        if (!hasKeycard)
        {
            Debug.Log("Door locked.");
            return;
        }

        OpenDoor();
    }

    void OpenDoor()
    {
        opened = true;

        audioSource.Play();

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
