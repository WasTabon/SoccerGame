using UnityEngine;
using System;

public enum GameMode
{
    Match,
    Endless
}

public class MatchManager : MonoBehaviour
{
    public static MatchManager Instance { get; private set; }

    public Ball ball;
    public float respawnDelay = 1f;
    public int winScore = 5;
    public GameMode gameMode = GameMode.Match;

    public int playerScore { get; private set; }
    public int opponentScore { get; private set; }
    public bool isMatchOver { get; private set; }
    public int streak { get; private set; }
    public int bestScore { get; private set; }

    public static event Action<int, int> OnScoreChanged;
    public static event Action<bool> OnMatchEnd;
    public static event Action<int, int, int> OnEndlessScoreChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        bestScore = PlayerPrefs.GetInt("EndlessBestScore", 0);
    }

    private void OnEnable()
    {
        GoalZone.OnGoalScored -= HandleGoal;
        GoalZone.OnGoalScored += HandleGoal;
    }

    private void OnDisable()
    {
        GoalZone.OnGoalScored -= HandleGoal;
    }

    private void HandleGoal(bool isPlayerGoal)
    {
        if (isMatchOver) return;

        if (gameMode == GameMode.Match)
            HandleMatchGoal(isPlayerGoal);
        else
            HandleEndlessGoal(isPlayerGoal);
    }

    private void HandleMatchGoal(bool isPlayerGoal)
    {
        if (isPlayerGoal)
            opponentScore++;
        else
            playerScore++;

        OnScoreChanged?.Invoke(playerScore, opponentScore);

        ball.gameObject.SetActive(false);

        if (playerScore >= winScore)
        {
            isMatchOver = true;
            OnMatchEnd?.Invoke(true);
        }
        else if (opponentScore >= winScore)
        {
            isMatchOver = true;
            OnMatchEnd?.Invoke(false);
        }
        else
        {
            Invoke(nameof(RespawnBall), respawnDelay);
        }
    }

    private void HandleEndlessGoal(bool isPlayerGoal)
    {
        ball.gameObject.SetActive(false);

        if (isPlayerGoal)
        {
            streak = 0;
            opponentScore++;
            OnEndlessScoreChanged?.Invoke(playerScore, streak, bestScore);
            Invoke(nameof(RespawnBall), respawnDelay);
        }
        else
        {
            streak++;
            int points = 1 + (streak / 3);
            playerScore += points;
            if (playerScore > bestScore)
            {
                bestScore = playerScore;
                PlayerPrefs.SetInt("EndlessBestScore", bestScore);
            }
            OnEndlessScoreChanged?.Invoke(playerScore, streak, bestScore);
            Invoke(nameof(RespawnBall), respawnDelay);
        }
    }

    private void RespawnBall()
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        ball.transform.position = Vector3.zero;
        ball.gameObject.SetActive(true);
        ball.Launch();
    }

    public void ResetMatch()
    {
        isMatchOver = false;
        playerScore = 0;
        opponentScore = 0;
        streak = 0;
        OnScoreChanged?.Invoke(playerScore, opponentScore);
        OnEndlessScoreChanged?.Invoke(playerScore, streak, bestScore);

        if (DifficultyManager.Instance != null)
            DifficultyManager.Instance.ResetDifficulty();

        RespawnBall();
    }

    public void SetGameMode(GameMode mode)
    {
        gameMode = mode;
        ResetMatch();
    }
}
