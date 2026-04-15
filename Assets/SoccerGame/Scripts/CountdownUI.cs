using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class CountdownUI : MonoBehaviour
{
    public static CountdownUI Instance { get; private set; }

    public TextMeshProUGUI countdownText;
    public float stepDuration = 0.7f;

    public static event Action OnCountdownFinished;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartCountdown()
    {
        if (countdownText == null)
        {
            OnCountdownFinished?.Invoke();
            return;
        }

        countdownText.gameObject.SetActive(true);
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() => ShowStep("3", Color.white));
        seq.AppendInterval(stepDuration);
        seq.AppendCallback(() => ShowStep("2", Color.white));
        seq.AppendInterval(stepDuration);
        seq.AppendCallback(() => ShowStep("1", Color.yellow));
        seq.AppendInterval(stepDuration);
        seq.AppendCallback(() => ShowStep("GO!", new Color(0.2f, 1f, 0.3f)));
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {
            countdownText.gameObject.SetActive(false);
            OnCountdownFinished?.Invoke();
        });

        seq.SetUpdate(true);
    }

    private void ShowStep(string text, Color color)
    {
        countdownText.text = text;
        countdownText.color = color;
        countdownText.transform.localScale = Vector3.one * 2f;
        countdownText.transform.DOScale(Vector3.one, stepDuration * 0.6f).SetEase(Ease.OutQuad).SetUpdate(true);
        countdownText.DOFade(1f, 0.05f).SetUpdate(true);
        countdownText.DOFade(0.3f, stepDuration * 0.8f).SetDelay(stepDuration * 0.2f).SetUpdate(true);
    }
}
