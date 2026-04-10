using UnityEngine;
using TMPro;

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
    }
}
