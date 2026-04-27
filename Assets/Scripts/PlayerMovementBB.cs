using System.Collections;
using UnityEngine;

public class GridMovement : MonoBehaviour {
  // Allows you to hold down a key for movement.
  [SerializeField] private bool isRepeatedMovement = false;
  // Time in seconds to move between one grid position and the next.
  [SerializeField] private float moveDuration = 0.1f;
  // The size of the grid
  [SerializeField] private float gridSize = 1f;

  private bool isMoving = false;
  public float speed = 8f;
  private Rigidbody2D rb;
  private bool isGrounded;
  public Transform groundCheck;
  public float groundCheckDistance = 0.12f;
  public Vector3 groundCheckOffset = new Vector3(0f, -0.5f);
  public LayerMask groundLayer;
  private SpriteRenderer spriteRenderer;
  private Animator animator;
  private float moveHorizontal;
  private float vertical_pos = 1;

  void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }


  // Update is called once per frame
  private void Update() {
    moveHorizontal = 0;
    // Only process on move at a time.
    if (!isMoving) {
      // Accomodate two different types of moving.
      System.Func<KeyCode, bool> inputFunction;
      if (isRepeatedMovement) {
        // GetKey repeatedly fires.
        inputFunction = Input.GetKey;
      } else {
        // GetKeyDown fires once per keypress
        inputFunction = Input.GetKeyDown;
      }

      // If the input function is active, move in the appropriate direction.
      if (inputFunction(KeyCode.UpArrow) && vertical_pos < 2 ) {
        StartCoroutine(Move(Vector2.up));
        vertical_pos ++;
      } else if (inputFunction(KeyCode.DownArrow) && vertical_pos > 0) {
        StartCoroutine(Move(Vector2.down));
        vertical_pos --;
      }
    }
  }

  void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(0, 0, rb.linearVelocity.x);
    }


  // Smooth movement between grid positions.
  private IEnumerator Move(Vector2 direction) {
    // Record that we're moving so we don't accept more input.
    isMoving = true;

    // Make a note of where we are and where we are going.
    Vector2 startPosition = transform.position;
    Vector2 endPosition = startPosition + (direction * gridSize);

    // Smoothly move in the desired direction taking the required time.
    float elapsedTime = 0;
    while (elapsedTime < moveDuration) {
      elapsedTime += Time.deltaTime;
      float percent = elapsedTime / moveDuration;
      transform.position = Vector2.Lerp(startPosition, endPosition, percent);
      yield return null;
    }
    Vector3 rayOrigin = groundCheck != null ? (Vector3)groundCheck.position : (Vector3)transform.position + groundCheckOffset;
    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector3.down, groundCheckDistance, groundLayer);
    isGrounded = hit.collider != null;

    if (animator != null)
    {
        animator.SetFloat("moveInput", Mathf.Abs(moveHorizontal));
        animator.SetBool("isGrounded", isGrounded);
    }

    // Make sure we end up exactly where we want.
    transform.position = endPosition;

    // We're no longer moving so we can accept another move input.
    isMoving = false;
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
