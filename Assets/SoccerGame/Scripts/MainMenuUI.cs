using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button matchButton;
    public Button endlessButton;
    public Button levelsButton;
    public Button shopButton;
    public LevelSelectUI levelSelectUI;
    public TutorialUI tutorialUI;
    public SkinShopUI skinShopUI;

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

        if (shopButton != null)
        {
            shopButton.onClick.RemoveListener(OnShopClicked);
            shopButton.onClick.AddListener(OnShopClicked);
        }
    }

    private void OnDisable()
    {
        matchButton.onClick.RemoveListener(OnMatchClicked);
        endlessButton.onClick.RemoveListener(OnEndlessClicked);
        if (levelsButton != null)
            levelsButton.onClick.RemoveListener(OnLevelsClicked);
        if (shopButton != null)
            shopButton.onClick.RemoveListener(OnShopClicked);
    }

    private void Start()
    {
        if (tutorialUI != null && tutorialUI.ShouldShow())
            tutorialUI.Show();
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

    private void OnShopClicked()
    {
        if (skinShopUI != null)
            skinShopUI.Show();
    }

    private GameStarter GetOrCreateGameStarter()
    {
        if (GameStarter.Instance != null) return GameStarter.Instance;

        GameObject obj = new GameObject("GameStarter");
        return obj.AddComponent<GameStarter>();
    }
}
