using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int collectibleCount = 0;
    public TMP_Text collectibleText;
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;

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

        collectibleText.text = $"Collectibles: {collectibleCount}";
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
            Debug.Log("ESCAPE PRESSED");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
            Debug.Log("ESCAPE PRESSED");
        }
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

    public void AddCollectible()
    {
        collectibleCount++;
        UpdateCollectibleUI();
    }

    private void UpdateCollectibleUI()
    {
        if (collectibleText != null)
        {
            collectibleText.text = $"Collectibles: {collectibleCount}";
        }
    }
}
