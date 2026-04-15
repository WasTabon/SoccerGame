using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        MatchManager.OnScoreChanged -= UpdateScore;
        MatchManager.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        MatchManager.OnScoreChanged -= UpdateScore;
    }

    private void Start()
    {
        UpdateScore(0, 0);
    }

    private void UpdateScore(int player, int opponent)
    {
        scoreText.text = player + " - " + opponent;
        scoreText.transform.DOComplete();
        scoreText.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f, 5).SetUpdate(true);
    }
}
