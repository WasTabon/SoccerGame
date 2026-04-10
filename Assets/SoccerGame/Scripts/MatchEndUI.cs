using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MatchEndUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI finalScoreText;
    public Button restartButton;

    private void OnEnable()
    {
        MatchManager.OnMatchEnd -= ShowResult;
        MatchManager.OnMatchEnd += ShowResult;

        if (restartButton != null)
        {
            restartButton.onClick.RemoveListener(OnRestart);
            restartButton.onClick.AddListener(OnRestart);
        }
    }

    private void OnDisable()
    {
        MatchManager.OnMatchEnd -= ShowResult;
    }

    private void Start()
    {
        panel.SetActive(false);
    }

    private void ShowResult(bool playerWon)
    {
        panel.SetActive(true);
        resultText.text = playerWon ? "YOU WIN!" : "YOU LOSE!";
        resultText.color = playerWon ? new Color(0.2f, 0.9f, 0.3f) : new Color(0.9f, 0.2f, 0.2f);
        finalScoreText.text = MatchManager.Instance.playerScore + " - " + MatchManager.Instance.opponentScore;
    }

    private void OnRestart()
    {
        panel.SetActive(false);
        MatchManager.Instance.ResetMatch();
    }
}
