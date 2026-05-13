using UnityEngine;

public class Puzzle : MonoBehaviour, Interactable
{
    public GameObject puzzleUI;

    private SpriteRenderer spriteRenderer;

    private Color originalColor;

    public Color highlightColor = Color.yellow;

    public bool isOpen { get; private set; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    public void Interact()
    {
        puzzleUI.SetActive(true);
        Time.timeScale = 0f;
        isOpen = true;
        PauseManager.Instance.IsInUI = true;
    }

    public void CloseUI()
    {
        puzzleUI.SetActive(false);
        Time.timeScale = 1f;
        isOpen = false;
        PauseManager.Instance.IsInUI = false;
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