using UnityEngine;
using System;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public int coins { get; private set; }

    public static event Action<int> OnCoinsChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        coins = PlayerPrefs.GetInt("Coins", 0);
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
        if (!isPlayerGoal)
        {
            AddCoins(1);
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
        OnCoinsChanged?.Invoke(coins);
    }

    public bool SpendCoins(int amount)
    {
        if (coins < amount) return false;
        coins -= amount;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
        OnCoinsChanged?.Invoke(coins);
        return true;
    }
}
