using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public static GameStarter Instance { get; private set; }
    public GameMode selectedMode = GameMode.Match;
    public int selectedLevel = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            SetupGameScene();
        }
    }

    private void SetupGameScene()
    {
        if (MatchManager.Instance == null)
        {
            Debug.LogWarning("GameStarter: MatchManager not found on Game scene!");
            return;
        }

        Ball ball = MatchManager.Instance.ball;
        if (ball != null)
        {
            ball.autoLaunch = false;
            ball.gameObject.SetActive(false);
        }

        SpawnObstacles();
        SetupMode();
        SetupUI();

        if (CountdownUI.Instance != null)
        {
            CountdownUI.OnCountdownFinished -= OnCountdownDone;
            CountdownUI.OnCountdownFinished += OnCountdownDone;
            CountdownUI.Instance.StartCountdown();
        }
        else
        {
            OnCountdownDone();
        }
    }

    private void SpawnObstacles()
    {
        if (ObstacleSpawner.Instance == null)
        {
            Debug.LogWarning("GameStarter: ObstacleSpawner not found!");
            return;
        }

        if (selectedMode == GameMode.Levels)
        {
            LevelConfig config = LevelDatabase.GetLevel(selectedLevel);
            ObstacleSpawner.Instance.SpawnForLevel(config);
        }
        else
        {
            ObstacleSpawner.Instance.SpawnForMatchEndless();
        }
    }

    private void SetupMode()
    {
        if (selectedMode == GameMode.Levels)
        {
            MatchManager.Instance.SetupLevel(selectedLevel);
        }
        else
        {
            MatchManager.Instance.SetGameMode(selectedMode);
        }
    }

    private void SetupUI()
    {
        Transform canvasT = GameObject.Find("GameCanvas")?.transform;
        if (canvasT == null) return;

        GameObject scorePanel = canvasT.Find("ScorePanel")?.gameObject;
        GameObject endlessPanel = canvasT.Find("EndlessPanel")?.gameObject;
        GameObject levelPanel = canvasT.Find("LevelPanel")?.gameObject;

        if (scorePanel != null) scorePanel.SetActive(selectedMode == GameMode.Match);
        if (endlessPanel != null) endlessPanel.SetActive(selectedMode == GameMode.Endless);
        if (levelPanel != null) levelPanel.SetActive(selectedMode == GameMode.Levels);
    }

    private void OnCountdownDone()
    {
        CountdownUI.OnCountdownFinished -= OnCountdownDone;
        Ball ball = MatchManager.Instance.ball;
        if (ball != null)
        {
            ball.gameObject.SetActive(true);
            ball.Launch();
        }
    }

    public void StartGame(GameMode mode)
    {
        selectedMode = mode;
        if (SceneTransition.Instance != null)
            SceneTransition.Instance.LoadScene("Game");
        else
            SceneManager.LoadScene("Game");
    }

    public void StartLevel(int level)
    {
        selectedMode = GameMode.Levels;
        selectedLevel = level;
        if (SceneTransition.Instance != null)
            SceneTransition.Instance.LoadScene("Game");
        else
            SceneManager.LoadScene("Game");
    }
}
