using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeTrap : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;
    [SerializeField] float damage = 100f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(damage);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(0);
        }
    }
}