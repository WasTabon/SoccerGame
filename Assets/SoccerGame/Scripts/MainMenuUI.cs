using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button matchButton;
    public Button endlessButton;
    public Button levelsButton;
    public LevelSelectUI levelSelectUI;

    private void OnEnable()
    {
        matchButton.onClick.RemoveListener(OnMatchClicked);
        matchButton.onClick.AddListener(OnMatchClicked);

        endlessButton.onClick.RemoveListener(OnEndlessClicked);
        endlessButton.onClick.AddListener(OnEndlessClicked);

        if (levelsButton != null)
        {
            levelsButton.onClick.RemoveListener(OnLevelsClicked);
            levelsButton.onClick.AddListener(OnLevelsClicked);
        }
    }

    private void OnDisable()
    {
        matchButton.onClick.RemoveListener(OnMatchClicked);
        endlessButton.onClick.RemoveListener(OnEndlessClicked);
        if (levelsButton != null)
            levelsButton.onClick.RemoveListener(OnLevelsClicked);
    }

    private void OnMatchClicked()
    {
        GetOrCreateGameStarter().StartGame(GameMode.Match);
    }

    private void OnEndlessClicked()
    {
        GetOrCreateGameStarter().StartGame(GameMode.Endless);
    }

    private void OnLevelsClicked()
    {
        if (levelSelectUI != null)
            levelSelectUI.Show();
    }

    private GameStarter GetOrCreateGameStarter()
    {
        if (GameStarter.Instance != null) return GameStarter.Instance;

        GameObject obj = new GameObject("GameStarter");
        return obj.AddComponent<GameStarter>();
    }
}
