using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MatchEndUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI finalScoreText;
    public Button restartButton;
    public Button menuButton;
    public RectTransform contentRect;

    private void OnEnable()
    {
        MatchManager.OnMatchEnd -= ShowResult;
        MatchManager.OnMatchEnd += ShowResult;

        if (restartButton != null)
        {
            restartButton.onClick.RemoveListener(OnRestart);
            restartButton.onClick.AddListener(OnRestart);
        }

        if (menuButton != null)
        {
            menuButton.onClick.RemoveListener(OnMenu);
            menuButton.onClick.AddListener(OnMenu);
        }
    }

    private void OnDisable()
    {
        MatchManager.OnMatchEnd -= ShowResult;
    }

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    private void ShowResult(bool playerWon)
    {
        panel.SetActive(true);
        resultText.text = playerWon ? "YOU WIN!" : "YOU LOSE!";
        resultText.color = playerWon ? new Color(0.2f, 0.9f, 0.3f) : new Color(0.9f, 0.2f, 0.2f);
        finalScoreText.text = MatchManager.Instance.playerScore + " - " + MatchManager.Instance.opponentScore;

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

    private void OnRestart()
    {
        if (contentRect != null)
        {
            contentRect.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).SetUpdate(true)
                .OnComplete(() =>
                {
                    panel.SetActive(false);
                    Time.timeScale = 1f;
                    MatchManager.Instance.ResetMatch();
                });
        }
        else
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
            MatchManager.Instance.ResetMatch();
        }
    }

    private void OnMenu()
    {
        Time.timeScale = 1f;
        GoToMenu();
    }

    public static void GoToMenu()
    {
        if (GameStarter.Instance != null)
            Object.Destroy(GameStarter.Instance.gameObject);

        if (SceneTransition.Instance != null)
            SceneTransition.Instance.LoadScene("MainMenu");
        else
            SceneManager.LoadScene("MainMenu");
    }
}
