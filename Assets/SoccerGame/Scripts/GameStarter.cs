using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public static GameStarter Instance { get; private set; }
    public GameMode selectedMode = GameMode.Match;

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

        MatchManager.Instance.SetGameMode(selectedMode);

        GameObject scorePanel = GameObject.Find("GameCanvas")?.transform.Find("ScorePanel")?.gameObject;
        GameObject endlessPanel = GameObject.Find("GameCanvas")?.transform.Find("EndlessPanel")?.gameObject;

        if (selectedMode == GameMode.Match)
        {
            if (scorePanel != null) scorePanel.SetActive(true);
            if (endlessPanel != null) endlessPanel.SetActive(false);
        }
        else
        {
            if (scorePanel != null) scorePanel.SetActive(false);
            if (endlessPanel != null) endlessPanel.SetActive(true);
        }
    }

    public void StartGame(GameMode mode)
    {
        selectedMode = mode;
        SceneManager.LoadScene("Game");
    }
}
