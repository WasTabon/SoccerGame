using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class SkinShopUI : MonoBehaviour
{
    public GameObject panel;
    public Transform contentParent;
    public GameObject skinItemPrefab;
    public Button backButton;
    public TextMeshProUGUI coinDisplayText;

    private List<SkinShopItem> items = new List<SkinShopItem>();

    private void OnEnable()
    {
        if (backButton != null)
        {
            backButton.onClick.RemoveListener(OnBack);
            backButton.onClick.AddListener(OnBack);
        }

        CoinManager.OnCoinsChanged -= UpdateCoinDisplay;
        CoinManager.OnCoinsChanged += UpdateCoinDisplay;
    }

    private void OnDisable()
    {
        if (backButton != null)
            backButton.onClick.RemoveListener(OnBack);

        CoinManager.OnCoinsChanged -= UpdateCoinDisplay;
    }

    public void Show()
    {
        panel.SetActive(true);
        UpdateCoinDisplay(CoinManager.Instance != null ? CoinManager.Instance.coins : PlayerPrefs.GetInt("Coins", 0));
        RefreshItems();
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    private void RefreshItems()
    {
        if (SkinManager.Instance == null) return;

        if (items.Count == 0)
            CreateItems();

        string activeId = SkinManager.Instance.GetActiveSkinId();

        for (int i = 0; i < items.Count; i++)
        {
            SkinData skin = SkinManager.Instance.allSkins[i];
            bool owned = SkinManager.Instance.IsSkinOwned(skin.id);
            bool equipped = skin.id == activeId;
            items[i].Setup(skin, owned, equipped);
        }
    }

    private void CreateItems()
    {
        for (int i = contentParent.childCount - 1; i >= 0; i--)
            Destroy(contentParent.GetChild(i).gameObject);

        items.Clear();

        foreach (SkinData skin in SkinManager.Instance.allSkins)
        {
            GameObject obj = Instantiate(skinItemPrefab, contentParent);
            obj.SetActive(true);
            SkinShopItem item = obj.GetComponent<SkinShopItem>();
            Debug.Assert(item != null, "SkinShopItem missing on prefab!");
            item.shopUI = this;
            items.Add(item);
        }
    }

    public void OnSkinAction(SkinData skin)
    {
        if (SkinManager.Instance == null) return;

        if (SkinManager.Instance.IsSkinOwned(skin.id))
        {
            SkinManager.Instance.EquipSkin(skin.id);
        }
        else
        {
            SkinManager.Instance.BuySkin(skin.id);
        }

        RefreshItems();
    }

    private void UpdateCoinDisplay(int amount)
    {
        if (coinDisplayText != null)
            coinDisplayText.text = amount.ToString();
    }

    private void OnBack()
    {
        Hide();
    }
}
