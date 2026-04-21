using UnityEngine;

public class ResetTrackPos : MonoBehaviour
{
    [SerializeField] private Transform trackRef;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Track"))
        {
            Transform trackPos = other.GetComponent<Transform>();
            trackPos.position.x = trackRef.position.x;
        }
    }
}
