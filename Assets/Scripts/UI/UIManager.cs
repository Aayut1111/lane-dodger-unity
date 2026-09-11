using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private Button restartButton;

    private int _lastScore;

    private void OnEnable()
    {
        gameManager.OnScoreChanged += HandleScoreChanged;
        gameManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        gameManager.OnScoreChanged -= HandleScoreChanged;
        gameManager.OnGameOver -= HandleGameOver;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        restartButton.onClick.AddListener(gameManager.RestartGame);
    }

    private void HandleScoreChanged(int score)
    {
        _lastScore = score;
        scoreText.text = $"Score: {score}";
    }

    private void HandleGameOver()
    {
        finalScoreText.text = $"You crashed! Final score: {_lastScore}";
        gameOverPanel.SetActive(true);
    }
}