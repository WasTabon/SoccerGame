using UnityEngine;
using TMPro;
using DG.Tweening;

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
        scoreText.transform.DOComplete();
        scoreText.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f, 5).SetUpdate(true);

        if (streak > 0)
        {
            streakText.text = "STREAK x" + streak;
            streakText.transform.DOComplete();
            streakText.transform.localScale = Vector3.one * 0.5f;
            streakText.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
        }
        else
        {
            streakText.text = "";
        }

        bestScoreText.text = "BEST: " + best;
    }
}
