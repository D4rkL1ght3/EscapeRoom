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
        exitDoor.SetActive(true);
        CloseUI();

        Debug.Log("Door Opened!");
    }

    public void GiveKeycard()
    {
        hasKeycard = true;

        keycardButton.SetActive(false);

        Debug.Log("Player got keycard!");
    }

    public void CloseUI()
    {
        cardReaderUI.SetActive(false);
        playerController.enabled = true;
    }
}
