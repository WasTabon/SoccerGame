using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    public Ball ball;
    public AIGoalkeeper goalkeeper;
    public float ballSpeedIncreaseRate = 0.3f;
    public float goalkeeperSpeedIncreaseRate = 0.15f;
    public float maxBallSpeedMultiplier = 2.5f;
    public float maxGoalkeeperSpeedMultiplier = 1.8f;

    private float elapsedTime;
    private float baseBallMinSpeed;
    private float baseBallMaxSpeed;
    private float baseBallInitialSpeed;
    private float baseGoalkeeperSpeed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        StoreBases();
    }

    private void StoreBases()
    {
        if (ball != null)
        {
            baseBallMinSpeed = ball.minSpeed;
            baseBallMaxSpeed = ball.maxSpeed;
            baseBallInitialSpeed = ball.initialSpeed;
        }
        if (goalkeeper != null)
        {
            baseGoalkeeperSpeed = goalkeeper.speed;
        }
    }

    private void Update()
    {
        if (MatchManager.Instance != null && MatchManager.Instance.isMatchOver) return;

        elapsedTime += Time.deltaTime;
        ApplyDifficulty();
    }

    private void ApplyDifficulty()
    {
        float ballMult = Mathf.Min(1f + elapsedTime * ballSpeedIncreaseRate * 0.01f, maxBallSpeedMultiplier);
        float keeperMult = Mathf.Min(1f + elapsedTime * goalkeeperSpeedIncreaseRate * 0.01f, maxGoalkeeperSpeedMultiplier);

        if (ball != null)
        {
            ball.minSpeed = baseBallMinSpeed * ballMult;
            ball.maxSpeed = baseBallMaxSpeed * ballMult;
            ball.initialSpeed = baseBallInitialSpeed * ballMult;
        }

        if (goalkeeper != null)
        {
            goalkeeper.speed = baseGoalkeeperSpeed * keeperMult;
        }
    }

    public void ResetDifficulty()
    {
        elapsedTime = 0f;
        if (ball != null)
        {
            ball.minSpeed = baseBallMinSpeed;
            ball.maxSpeed = baseBallMaxSpeed;
            ball.initialSpeed = baseBallInitialSpeed;
        }
        if (goalkeeper != null)
        {
            goalkeeper.speed = baseGoalkeeperSpeed;
        }
    }
}
