using UnityEngine;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;

public class PlayerInteract : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactText;

    private Interactable currentInteractable;

    void Start()
    {
        interactText.SetActive(false);
    }

    void Update()
    {
        // Press E to interact
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0f)
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && currentInteractable.isOpen)
        {
            // Close any open UI
            if (currentInteractable != null)
            {
                currentInteractable.CloseUI();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable =
            other.GetComponent<Interactable>();

        if (interactable != null)
        {
            currentInteractable = interactable;

            interactable.Highlight();

            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable =
            other.GetComponent<Interactable>();

        if (interactable != null &&
            interactable == currentInteractable)
        {
            interactable.RemoveHighlight();

            currentInteractable = null;

            interactText.SetActive(false);
        }
    }
}