using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // degrees per second

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddCollectible();
            if (AudioManager.Instance != null && AudioManager.Instance.collectibleSFX != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.collectibleSFX);
            }
            Destroy(gameObject);
        }
    }
}
