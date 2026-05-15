using UnityEngine;

public class ExitDoor : MonoBehaviour, Interactable
{
    [Header("UI")]
    public GameObject winScreenUI;

    [Header("Highlight")]
    private SpriteRenderer spriteRenderer;

    private Color originalColor;

    public Color highlightColor = Color.yellow;

    [Header("Player")]
    public MonoBehaviour playerController;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    public void Interact()
    {
        winScreenUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        Debug.Log("You escaped!");
    }

    public void CloseUI()
    {
        // This method is not needed for the exit door, but it's here to satisfy the Interactable interface
    }

    public void Highlight()
    {
        spriteRenderer.color = highlightColor;
    }

    public void RemoveHighlight()
    {
        spriteRenderer.color = originalColor;
    }
}
