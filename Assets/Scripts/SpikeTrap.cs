using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpikeTrap : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;
    [SerializeField] float damage = 100f;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(damage);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // print("die 2");
            audioManager.PlaySFX(audioManager.DeathSFX);
            audioManager.StopMusic();
            Scene currentScene = SceneManager.GetActiveScene();
            Time.timeScale = 1f;
            GameManager.Instance.isPlaying = false;
            SceneManager.LoadScene("main menu");
        }
    }
}