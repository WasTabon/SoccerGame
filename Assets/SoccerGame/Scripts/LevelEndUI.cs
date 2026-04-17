using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelEndUI : MonoBehaviour
{
    public GameObject panel;
    public RectTransform contentRect;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI levelInfoText;
    public Button retryButton;
    public Button nextButton;
    public Button menuButton;

    private void OnEnable()
    {
        MatchManager.OnLevelEnd -= ShowResult;
        MatchManager.OnLevelEnd += ShowResult;

        if (retryButton != null)
        {
            retryButton.onClick.RemoveListener(OnRetry);
            retryButton.onClick.AddListener(OnRetry);
        }
        if (nextButton != null)
        {
            nextButton.onClick.RemoveListener(OnNext);
            nextButton.onClick.AddListener(OnNext);
        }
        if (menuButton != null)
        {
            menuButton.onClick.RemoveListener(OnMenu);
            menuButton.onClick.AddListener(OnMenu);
        }
    }

    private void OnDisable()
    {
        MatchManager.OnLevelEnd -= ShowResult;
    }

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    private void ShowResult(bool won, int level)
    {
        panel.SetActive(true);

        if (won)
        {
            resultText.text = "LEVEL COMPLETE!";
            resultText.color = new Color(0.2f, 0.9f, 0.3f);
            levelInfoText.text = "Level " + level + " cleared!";
            nextButton.gameObject.SetActive(level < LevelDatabase.TotalLevels);
            retryButton.gameObject.SetActive(true);
        }
        else
        {
            resultText.text = "LEVEL FAILED!";
            resultText.color = new Color(0.9f, 0.2f, 0.2f);
            levelInfoText.text = "Level " + level;
            nextButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);
        }

        if (contentRect != null)
        {
            contentRect.localScale = Vector3.zero;
            contentRect.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetUpdate(true);
        }

        if (resultText != null)
        {
            resultText.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 5).SetUpdate(true).SetDelay(0.3f);
        }
    }

    private void OnRetry()
    {
        AnimateOut(() =>
        {
            if (GameStarter.Instance != null)
                GameStarter.Instance.StartLevel(MatchManager.Instance.currentLevel);
        });
    }

    private void OnNext()
    {
        int nextLevel = MatchManager.Instance.currentLevel + 1;
        AnimateOut(() =>
        {
            if (GameStarter.Instance != null)
                GameStarter.Instance.StartLevel(nextLevel);
        });
    }

    private void OnMenu()
    {
        Time.timeScale = 1f;
        MatchEndUI.GoToMenu();
    }

    private void AnimateOut(TweenCallback onComplete)
    {
        if (contentRect != null)
        {
            contentRect.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).SetUpdate(true)
                .OnComplete(() =>
                {
                    panel.SetActive(false);
                    Time.timeScale = 1f;
                    onComplete?.Invoke();
                });
        }
        else
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
            onComplete?.Invoke();
        }
    }
}
