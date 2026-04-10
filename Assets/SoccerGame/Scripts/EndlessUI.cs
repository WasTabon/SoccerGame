using UnityEngine;
using TMPro;

public class EndlessUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI streakText;
    public TextMeshProUGUI bestScoreText;

    private void OnEnable()
    {
        MatchManager.OnEndlessScoreChanged -= UpdateEndlessUI;
        MatchManager.OnEndlessScoreChanged += UpdateEndlessUI;
    }

    private void OnDisable()
    {
        MatchManager.OnEndlessScoreChanged -= UpdateEndlessUI;
    }

    private void Start()
    {
        UpdateEndlessUI(0, 0, PlayerPrefs.GetInt("EndlessBestScore", 0));
    }

    private void UpdateEndlessUI(int score, int streak, int best)
    {
        scoreText.text = score.ToString();
        streakText.text = streak > 0 ? "STREAK x" + streak : "";
        bestScoreText.text = "BEST: " + best;
    }
}
