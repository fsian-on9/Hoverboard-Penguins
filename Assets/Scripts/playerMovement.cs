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
    private bool moveVertical;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public enum Track {TopTrack, MiddleTrack, BottomTrack}
    public Track m_Track = Track.TopTrack;
    private float NewYPos;
    private float YValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveHorizontal = 1;
        moveVertical = Input.GetButtonDown("Vertical");

        if (moveVertical)
        {
            if (m_Track == Track.MiddleTrack)
            {
                NewYPos = 0;
            }
            else if (m_Track == Track.TopTrack)
            {
                NewYPos = YValue;
            }
            else if (m_Track == Track.BottomTrack)
            {
                NewYPos = -YValue;
            }
        }

        Debug.Log(moveVertical);

        Vector3 rayOrigin = groundCheck != null ? (Vector3)groundCheck.position : (Vector3)transform.position + groundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector3.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        if (animator != null)
        {
            animator.SetFloat("moveInput", Mathf.Abs(moveHorizontal));
            animator.SetBool("isGrounded", isGrounded);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        
        // if (moveVertical == 1)
        // {
        //     this.transform.position.y = transform.position.y + 10;
        // }
        // else if (moveVertical == -1)
        // {
        //     this.transform.position.y = transform.position.y - 10;
        // }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveHorizontal * speed, 0, rb.linearVelocity.x);
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