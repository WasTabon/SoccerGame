using UnityEngine;
using System;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance { get; private set; }

    public int maxLives = 5;
    public float regenMinutes = 20f;

    public int currentLives { get; private set; }
    public DateTime nextLifeTime { get; private set; }

    public static event Action<int, float> OnLivesChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
        RegenerateLives();
    }

    private void OnEnable()
    {
        MatchManager.OnMatchEnd -= OnMatchEnd;
        MatchManager.OnMatchEnd += OnMatchEnd;
        MatchManager.OnLevelEnd -= OnLevelEnd;
        MatchManager.OnLevelEnd += OnLevelEnd;
    }

    private void OnDisable()
    {
        MatchManager.OnMatchEnd -= OnMatchEnd;
        MatchManager.OnLevelEnd -= OnLevelEnd;
    }

    private void Update()
    {
        if (currentLives < maxLives && DateTime.Now >= nextLifeTime)
        {
            RegenerateLives();
        }
    }

    private void OnMatchEnd(bool playerWon)
    {
        if (!playerWon)
            LoseLife();
    }

    private void OnLevelEnd(bool won, int level)
    {
        if (!won)
            LoseLife();
    }

    public bool HasLives()
    {
        return currentLives > 0;
    }

    public void LoseLife()
    {
        if (currentLives <= 0) return;

        currentLives--;

        if (currentLives == maxLives - 1)
            nextLifeTime = DateTime.Now.AddMinutes(regenMinutes);

        SaveData();
        NotifyChanged();
    }

    public float GetSecondsUntilNextLife()
    {
        if (currentLives >= maxLives) return 0f;
        return Mathf.Max(0f, (float)(nextLifeTime - DateTime.Now).TotalSeconds);
    }

    private void RegenerateLives()
    {
        if (currentLives >= maxLives) return;

        string timeStr = PlayerPrefs.GetString("NextLifeTime", "");
        if (string.IsNullOrEmpty(timeStr))
        {
            currentLives = maxLives;
            SaveData();
            NotifyChanged();
            return;
        }

        DateTime savedTime;
        if (!DateTime.TryParse(timeStr, out savedTime))
        {
            currentLives = maxLives;
            SaveData();
            NotifyChanged();
            return;
        }

        double minutesPassed = (DateTime.Now - savedTime).TotalMinutes;
        int livesGained = Mathf.FloorToInt((float)(minutesPassed / regenMinutes));

        if (minutesPassed >= 0)
            livesGained += 1;

        currentLives = Mathf.Min(currentLives + livesGained, maxLives);

        if (currentLives < maxLives)
        {
            double remainderMinutes = minutesPassed % regenMinutes;
            nextLifeTime = DateTime.Now.AddMinutes(regenMinutes - remainderMinutes);
        }

        SaveData();
        NotifyChanged();
    }

    private void LoadData()
    {
        currentLives = PlayerPrefs.GetInt("Lives", maxLives);

        string timeStr = PlayerPrefs.GetString("NextLifeTime", "");
        if (!string.IsNullOrEmpty(timeStr))
        {
            DateTime savedTime;
            if (DateTime.TryParse(timeStr, out savedTime))
                nextLifeTime = savedTime;
            else
                nextLifeTime = DateTime.Now;
        }
        else
        {
            nextLifeTime = DateTime.Now;
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Lives", currentLives);
        if (currentLives < maxLives)
            PlayerPrefs.SetString("NextLifeTime", nextLifeTime.ToString("o"));
        else
            PlayerPrefs.DeleteKey("NextLifeTime");
        PlayerPrefs.Save();
    }

    private void NotifyChanged()
    {
        OnLivesChanged?.Invoke(currentLives, GetSecondsUntilNextLife());
    }
}
