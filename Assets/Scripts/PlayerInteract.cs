using UnityEngine;
using TMPro;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
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