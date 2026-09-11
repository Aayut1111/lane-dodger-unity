using System;
using UnityEngine;

public class GameManager : MonoBehaviour, ISpeedProvider
{
    [SerializeField] private PlayerController player;
    [SerializeField] private AnimationCurve speedCurve = AnimationCurve.Linear(0, 1, 1, 2.5f);
    [SerializeField] private float difficultyRampTime = 60f;
    [SerializeField] private float baseSpeed = 8f;

    private DifficultyService _difficulty;
    private ScoreService _scoreService;
    private float _elapsedTime;
    private float _currentSpeed;
    private int _obstaclesPassed;
    private bool _isGameOver;

    public float CurrentSpeed => _currentSpeed;
    public event Action<int> OnScoreChanged;
    public event Action OnGameOver;

    private void Awake()
    {
        _difficulty = new DifficultyService(speedCurve, difficultyRampTime);
        _scoreService = new ScoreService();
        player.OnPlayerHit += HandlePlayerHit;
    }

    private void Update()
    {
        if (_isGameOver) return;

        _elapsedTime += Time.deltaTime;
        _currentSpeed = baseSpeed * _difficulty.GetSpeedMultiplier(_elapsedTime);
    }

    public void RegisterObstaclePassed()
    {
        if (_isGameOver) return;
        _obstaclesPassed++;
        OnScoreChanged?.Invoke(_scoreService.CalculateScore(_obstaclesPassed));
    }

    private void HandlePlayerHit()
    {
        if (_isGameOver) return;
        _isGameOver = true;
        OnGameOver?.Invoke();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}