using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    AudioManager audioManager;
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;
    public bool isPlaying = true;
    private float trackSpeed = 10;
    private float elapsedtime = 100;

    [Header("Track Objects")]
    [SerializeField] private GameObject track1;
    [SerializeField] private GameObject track2;
    [SerializeField] private GameObject track3;

    Rigidbody2D track1RB;
    Rigidbody2D track2RB;
    Rigidbody2D track3RB;

    private void Awake()
    {

        Instance = this;
        DontDestroyOnLoad(gameObject);

        pauseMenu.SetActive(false);
        isPaused = false;
        isPlaying = true;
        track1RB = track1.GetComponent<Rigidbody2D>();
        track2RB = track2.GetComponent<Rigidbody2D>();
        track3RB = track3.GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlayMusic();
    }
    
    void Update()
    {
        
        if (isPlaying == true)
        {   
            elapsedtime += Time.deltaTime;
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
            
            print("race tracks baby");
            if(elapsedtime >= 120)
            {
            EndGame();
            }
        }
        else
        {
            audioManager.StopMusic();
        }
        
    }

    public void LoadScene()
    {
        if (isPlaying == true)
        {
            track1RB.linearVelocity = Vector2.left * trackSpeed;
            track2RB.linearVelocity = Vector2.left * trackSpeed;
            track3RB.linearVelocity = Vector2.left * trackSpeed;
            print("jack's mum");
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        print("pause");
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        isPlaying = false;
        print("pickles");
    }

    public void EndGame()
    {
        isPlaying = false;
        SceneLoader.LoadScene("End Scene");
    }
}
