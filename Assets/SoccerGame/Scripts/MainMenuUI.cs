using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public Button matchButton;
    public Button endlessButton;
    public Button levelsButton;
    public Button shopButton;
    public LevelSelectUI levelSelectUI;
    public TutorialUI tutorialUI;
    public SkinShopUI skinShopUI;
    public GameObject noLivesPopup;
    public TextMeshProUGUI bestScoreText;

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

        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        if (bestScoreText != null)
        {
            int best = PlayerPrefs.GetInt("EndlessBestScore", 0);
            bestScoreText.text = "BEST: " + best;
        }
    }

    private bool CheckLives()
    {
        if (LivesManager.Instance != null && !LivesManager.Instance.HasLives())
        {
            if (noLivesPopup != null) noLivesPopup.SetActive(true);
            return false;
        }
        return true;
    }

    private void OnMatchClicked()
    {
        if (!CheckLives()) return;
        GetOrCreateGameStarter().StartGame(GameMode.Match);
    }

    private void OnEndlessClicked()
    {
        if (!CheckLives()) return;
        GetOrCreateGameStarter().StartGame(GameMode.Endless);
    }

    private void OnLevelsClicked()
    {
        if (!CheckLives()) return;
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
