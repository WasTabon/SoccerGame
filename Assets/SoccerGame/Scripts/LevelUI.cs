using UnityEngine;
using TMPro;
using DG.Tweening;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI levelTitleText;
    public TextMeshProUGUI goalsText;

    private void OnEnable()
    {
        MatchManager.OnLevelScoreChanged -= UpdateGoals;
        MatchManager.OnLevelScoreChanged += UpdateGoals;
    }

    private void OnDisable()
    {
        MatchManager.OnLevelScoreChanged -= UpdateGoals;
    }

    private void Start()
    {
        if (MatchManager.Instance != null && MatchManager.Instance.gameMode == GameMode.Levels)
        {
            levelTitleText.text = "LEVEL " + MatchManager.Instance.currentLevel;
            UpdateGoals(0, MatchManager.Instance.levelGoalsRequired);
        }
    }

    private void UpdateGoals(int scored, int required)
    {
        goalsText.text = scored + " / " + required;
        goalsText.transform.DOComplete();
        goalsText.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f, 5).SetUpdate(true);
    }
}
