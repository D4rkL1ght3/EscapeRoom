using UnityEngine;

public class Bookshelf : MonoBehaviour, Interactable
{
    public GameObject puzzleUI;

    private SpriteRenderer spriteRenderer;

    private Color originalColor;

    public Color highlightColor =
        Color.yellow;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    public void Interact()
    {
        puzzleUI.SetActive(true);

        Time.timeScale = 0f;
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