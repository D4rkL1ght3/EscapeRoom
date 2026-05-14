using System.Collections;
using UnityEngine;

public class SlidingBookshelf : MonoBehaviour
{
    [Header("Slide Settings")]
    public Vector3 slideOffset = new Vector3(3f, 0f, 0f);
    public float slideDuration = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private bool hasSlid = false;
    private bool isSliding = false;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + slideOffset;
    }

    public void SlideOpen()
    {
        if (hasSlid || isSliding)
            return;

        StartCoroutine(SlideCoroutine());
    }

    private IEnumerator SlideCoroutine()
    {
        isSliding = true;

        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / slideDuration;

            // Smooth movement instead of robotic linear movement
            t = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                t
            );

            yield return null;
        }

        transform.position = targetPosition;

        hasSlid = true;
        isSliding = false;

        Debug.Log("Bookshelf finished sliding.");
    }
}