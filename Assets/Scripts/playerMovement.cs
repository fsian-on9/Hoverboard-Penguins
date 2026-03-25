using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedX = 8f;
    public float speedY = 1f;

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

    public Transform topTrack;
    public Transform middleTrack;
    public Transform bottomTrack;

    public float gridSize = 1f;
    // public enum Track {TopTrack, MiddleTrack, BottomTrack}
    // public Track m_Track = Track.TopTrack;
    // private float NewYPos;
    // private float YValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // playerPos = 1;

    }

    void Update()
    {
        moveHorizontal = 1;
        moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 rayOrigin = groundCheck != null ? (Vector3)groundCheck.position : (Vector3)transform.position + groundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector3.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        if (animator != null)
        {
            animator.SetFloat("moveInput", Mathf.Abs(moveHorizontal));
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveHorizontal * speedX, moveVertical * speedY, rb.linearVelocity.x);
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