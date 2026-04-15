using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GoalEffect : MonoBehaviour
{
    public static GoalEffect Instance { get; private set; }

    public Image flashImage;
    public float slowmoDuration = 0.4f;
    public float slowmoScale = 0.2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        GoalZone.OnGoalScored -= OnGoal;
        GoalZone.OnGoalScored += OnGoal;
    }

    private void OnDisable()
    {
        GoalZone.OnGoalScored -= OnGoal;
    }

    private void OnGoal(bool isPlayerGoal)
    {
        PlayFlash(isPlayerGoal);
        PlaySlowmo();

        if (ScreenShake.Instance != null)
            ScreenShake.Instance.ShakeHeavy();

        Ball ball = FindObjectOfType<Ball>();
        if (ball != null)
        {
            BallEffects be = ball.GetComponent<BallEffects>();
            if (be != null) be.PlayGoalEffect();
        }
    }

    private void PlayFlash(bool isPlayerGoal)
    {
        if (flashImage == null) return;

        Color flashColor = isPlayerGoal
            ? new Color(1f, 0.2f, 0.2f, 0.6f)
            : new Color(0.2f, 1f, 0.3f, 0.6f);

        flashImage.DOComplete();
        flashImage.color = flashColor;
        flashImage.DOFade(0f, 0.4f).SetUpdate(true);
    }

    private void PlaySlowmo()
    {
        Time.timeScale = slowmoScale;
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1f, slowmoDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
            });
        Time.fixedDeltaTime = 0.02f * slowmoScale;
    }
}
