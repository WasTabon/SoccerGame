using UnityEngine;
using TMPro;
using DG.Tweening;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private void OnEnable()
    {
        CoinManager.OnCoinsChanged -= UpdateCoins;
        CoinManager.OnCoinsChanged += UpdateCoins;
    }

    private void OnDisable()
    {
        CoinManager.OnCoinsChanged -= UpdateCoins;
    }

    private void Start()
    {
        int current = CoinManager.Instance != null ? CoinManager.Instance.coins : PlayerPrefs.GetInt("Coins", 0);
        UpdateCoins(current);
    }

    private void UpdateCoins(int amount)
    {
        coinText.text = amount.ToString();
        coinText.transform.DOComplete();
        coinText.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 5).SetUpdate(true);
    }
}
