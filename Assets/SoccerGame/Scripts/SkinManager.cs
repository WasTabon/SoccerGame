using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SkinData
{
    public string id;
    public string displayName;
    public int price;
    public Color color;
}

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance { get; private set; }

    public List<SkinData> allSkins;
    public static event Action<string> OnSkinChanged;

    private string activeSkinId;
    private HashSet<string> ownedSkins;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (allSkins == null || allSkins.Count == 0)
            CreateDefaultSkins();

        LoadData();
    }

    private void CreateDefaultSkins()
    {
        allSkins = new List<SkinData>
        {
            new SkinData { id = "default", displayName = "Classic", price = 0, color = Color.white },
            new SkinData { id = "fire", displayName = "Fire", price = 10, color = new Color(1f, 0.4f, 0.1f) },
            new SkinData { id = "ice", displayName = "Ice", price = 15, color = new Color(0.3f, 0.7f, 1f) },
            new SkinData { id = "toxic", displayName = "Toxic", price = 20, color = new Color(0.3f, 1f, 0.3f) },
            new SkinData { id = "gold", displayName = "Gold", price = 30, color = new Color(1f, 0.85f, 0f) },
            new SkinData { id = "shadow", displayName = "Shadow", price = 50, color = new Color(0.4f, 0.2f, 0.6f) }
        };
    }

    private void LoadData()
    {
        activeSkinId = PlayerPrefs.GetString("ActiveSkin", "default");
        ownedSkins = new HashSet<string>();

        string owned = PlayerPrefs.GetString("OwnedSkins", "default");
        foreach (string s in owned.Split(','))
        {
            if (!string.IsNullOrEmpty(s))
                ownedSkins.Add(s);
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("ActiveSkin", activeSkinId);
        PlayerPrefs.SetString("OwnedSkins", string.Join(",", ownedSkins));
        PlayerPrefs.Save();
    }

    public string GetActiveSkinId()
    {
        return activeSkinId;
    }

    public SkinData GetActiveSkin()
    {
        return allSkins.Find(s => s.id == activeSkinId);
    }

    public bool IsSkinOwned(string skinId)
    {
        return ownedSkins.Contains(skinId);
    }

    public bool BuySkin(string skinId)
    {
        SkinData skin = allSkins.Find(s => s.id == skinId);
        if (skin == null) return false;
        if (ownedSkins.Contains(skinId)) return false;

        if (CoinManager.Instance == null)
        {
            Debug.LogWarning("CoinManager not found!");
            return false;
        }

        if (!CoinManager.Instance.SpendCoins(skin.price)) return false;

        ownedSkins.Add(skinId);
        SaveData();
        return true;
    }

    public void EquipSkin(string skinId)
    {
        if (!ownedSkins.Contains(skinId)) return;
        activeSkinId = skinId;
        SaveData();
        OnSkinChanged?.Invoke(activeSkinId);
    }
}
