using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerMovementLaneSplitter : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xSpeed = 5;
    private float moveHorizontal = 1;
 
    [SerializeField] private float yIncrement;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
 
    void Update()
    {
        GetInput();
        Move();
    }
 
    void Move()
    {
        rb.linearVelocity = new Vector2(moveHorizontal * xSpeed, 0);
    }
 
    void GetInput()
    {
        if( Input.GetKeyDown( KeyCode.W ) )
        {
            ChangeLanes("Up");
        }
           
 
        if( Input.GetKeyDown( KeyCode.S ) )
        {
            ChangeLanes("Down");
        }
    }
 
    void ChangeLanes(string dir)
    {
        Vector2 newPosition = transform.position;
 
        if(dir == "Up" && newPosition.y != 1)
        {
            newPosition.y += yIncrement;
        }
        else if(dir == "Down" && newPosition.y != -1)
        {
            newPosition.y -= yIncrement;
        }
 
        transform.position = newPosition;
    }
}