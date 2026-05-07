using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        int score = Mathf.FloorToInt(GameManager.Instance.Distance);

        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + GameManager.Instance.HighScore;

        if (GameManager.Instance.IsGameOver)
        {
            gameOverPanel.SetActive(true);
            finalScoreText.text = "Final Score: " + score;
        }

        pausePanel.SetActive(GameManager.Instance.IsPaused);
    }

    public void RestartButton()
    {
        GameManager.Instance.RestartGame();
    }

    public void ResumeButton()
    {
        GameManager.Instance.TogglePause();
    }

    public void MainMenuButton()
    {
        GameManager.Instance.LoadMainMenu();
    }
}