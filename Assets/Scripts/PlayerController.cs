using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Map Bounds")]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 moveInput;
    private Vector2 lastMoveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput = moveInput.normalized;

        // Save last direction
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;
        }

        // Send values to Animator
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);

        animator.SetFloat("LastMoveX", lastMoveDirection.x);
        animator.SetFloat("LastMoveY", lastMoveDirection.y);

        animator.SetBool("IsMoving", moveInput != Vector2.zero);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        // Clamp position inside map bounds
        Vector2 clampedPosition = rb.position;

        clampedPosition.x = Mathf.Clamp(
            clampedPosition.x,
            minBounds.x,
            maxBounds.x
        );

        clampedPosition.y = Mathf.Clamp(
            clampedPosition.y,
            minBounds.y,
            maxBounds.y
        );

        rb.position = clampedPosition;
    }
}