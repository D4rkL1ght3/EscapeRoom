using UnityEngine;
using TMPro;


public class CardReaderPuzzle : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text hintText;

    public GameObject cardReaderUI;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Player")]
    public MonoBehaviour playerController;

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

        Debug.Log("Door Opened!");
    }

    public void GiveKeycard()
    {
        hasKeycard = true;

        Debug.Log("Player got keycard!");
    }
}
