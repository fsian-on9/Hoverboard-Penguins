using UnityEngine;

public class ResetTrackPos : MonoBehaviour
{
    [SerializeField] private Transform trackRef;

    void OnTriggerEnter2D(Collider2D other)
    {
        Transform trackPos = other.GetComponent<Transform>();
        Vector3 pos = transform.position;
        pos.x = 115.9f;

        if(other.CompareTag("Track"))
        {
            trackPos.position = pos;

        }
    }
}
