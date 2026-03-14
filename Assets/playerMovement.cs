using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundCheckDistance = 0.12f;
    public Vector3 groundCheckOffset = new Vector3(0f, -0.5f);
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float moveHorizontal;
    private float moveVertical;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 rayOrigin = groundCheck != null ? (Vector3)groundCheck.position : (Vector3)transform.position + groundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector3.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(moveHorizontal, moveVertical, jumpForce) * speed;
        }

        if (spriteRenderer != null)
        {
            if (moveVertical > 0.1f) spriteRenderer.flipX = false;
            else if (moveVertical < -0.1f) spriteRenderer.flipX = true;
        }

        if (animator != null)
        {
            animator.SetFloat("moveInput", Mathf.Abs(moveHorizontal));
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveVertical, moveHorizontal * speed, rb.linearVelocity.y);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + (Vector3)groundCheckOffset, transform.position + (Vector3)groundCheckOffset + Vector3.down * groundCheckDistance);
        }
    }
}