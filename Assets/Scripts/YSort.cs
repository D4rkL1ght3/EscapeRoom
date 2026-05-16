using UnityEngine;

public class YSort : MonoBehaviour
{
    [Header("Sorting")]
    public int sortingPrecision = 100;
    public int sortingOffset = 0;

    [Header("Optional")]
    public Transform sortPoint;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (sortPoint == null)
        {
            sortPoint = transform;
        }
    }

    void LateUpdate()
    {
        if (spriteRenderer == null)
            return;

        float yPosition = sortPoint.position.y;

        spriteRenderer.sortingOrder =
            Mathf.RoundToInt(-yPosition * sortingPrecision) + sortingOffset;
    }
}