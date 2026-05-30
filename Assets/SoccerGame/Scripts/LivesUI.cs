using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timerText;

    private void OnEnable()
    {
        LivesManager.OnLivesChanged -= UpdateDisplay;
        LivesManager.OnLivesChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        LivesManager.OnLivesChanged -= UpdateDisplay;
    }

    private void Start()
    {
        if (LivesManager.Instance != null)
            UpdateDisplay(LivesManager.Instance.currentLives, LivesManager.Instance.GetSecondsUntilNextLife());
    }

    private void Update()
    {
        if (LivesManager.Instance == null) return;
        if (LivesManager.Instance.currentLives >= LivesManager.Instance.maxLives)
        {
            if (timerText != null) timerText.text = "";
            return;
        }

        float seconds = LivesManager.Instance.GetSecondsUntilNextLife();
        if (timerText != null)
        {
            int min = Mathf.FloorToInt(seconds / 60f);
            int sec = Mathf.FloorToInt(seconds % 60f);
            timerText.text = string.Format("{0}:{1:00}", min, sec);
        }
    }

    private void UpdateDisplay(int lives, float secondsUntilNext)
    {
        if (livesText != null)
            livesText.text = lives.ToString();
    }
}
