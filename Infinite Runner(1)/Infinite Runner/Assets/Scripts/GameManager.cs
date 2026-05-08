using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameConfig config;

    public float ScrollSpeed { get; private set; }
    public float Distance { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool IsPaused { get; private set; }

    public int HighScore { get; private set; }

    public int Coins { get; private set; }

public void AddCoin()
{
    Coins++;
}

    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
            return; 
        }

        Instance = this;
        ScrollSpeed = config.startSpeed;
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        Time.timeScale = 1f;
    }

    void Update()
{
    if (Keyboard.current.rKey.wasPressedThisFrame)
    {
        RestartGame();
    }

    if (IsGameOver || IsPaused) return;

    ScrollSpeed = Mathf.Min(
        ScrollSpeed + config.speedIncreaseRate * Time.deltaTime,
        config.maxSpeed
    );

    Distance += ScrollSpeed * Time.deltaTime;
}
    public void GameOver()
    {
        if (IsGameOver) return;

        IsGameOver = true;

        int finalScore = Mathf.FloorToInt(Distance);

        if (finalScore > HighScore)
        {
            HighScore = finalScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TogglePause()
    {
        if (IsGameOver) return;

        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}