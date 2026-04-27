using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;

    private float trackSpeed = 10;

    [Header("Track Objects")]
    [SerializeField] private GameObject track1;
    [SerializeField] private GameObject track2;
    [SerializeField] private GameObject track3;

    Rigidbody2D track1RB;
    Rigidbody2D track2RB;
    Rigidbody2D track3RB;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        pauseMenu.SetActive(false);
        isPaused = false;

        track1RB = track1.GetComponent<Rigidbody2D>();
        track2RB = track2.GetComponent<Rigidbody2D>();
        track3RB = track3.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
        }

        track1RB.linearVelocity = Vector2.left * trackSpeed;
        track2RB.linearVelocity = Vector2.left * trackSpeed;
        track3RB.linearVelocity = Vector2.left * trackSpeed;
    }

    void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public bool isPlaying = false;

    public void GameOver()
    {
        isPlaying = false;
    }
}
