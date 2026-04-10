using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button matchButton;
    public Button endlessButton;

    private void OnEnable()
    {
        matchButton.onClick.RemoveListener(OnMatchClicked);
        matchButton.onClick.AddListener(OnMatchClicked);

        endlessButton.onClick.RemoveListener(OnEndlessClicked);
        endlessButton.onClick.AddListener(OnEndlessClicked);
    }

    private void OnDisable()
    {
        matchButton.onClick.RemoveListener(OnMatchClicked);
        endlessButton.onClick.RemoveListener(OnEndlessClicked);
    }

    private void OnMatchClicked()
    {
        GetOrCreateGameStarter().StartGame(GameMode.Match);
    }

    private void OnEndlessClicked()
    {
        GetOrCreateGameStarter().StartGame(GameMode.Endless);
    }

    private GameStarter GetOrCreateGameStarter()
    {
        if (GameStarter.Instance != null) return GameStarter.Instance;

        GameObject obj = new GameObject("GameStarter");
        return obj.AddComponent<GameStarter>();
    }
}
