using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelectUI : MonoBehaviour
{
    public GameObject panel;
    public Transform contentParent;
    public GameObject levelButtonPrefab;
    public Button backButton;

    private List<LevelSelectButton> buttons = new List<LevelSelectButton>();

    private void OnEnable()
    {
        if (backButton != null)
        {
            backButton.onClick.RemoveListener(OnBack);
            backButton.onClick.AddListener(OnBack);
        }
    }

    private void OnDisable()
    {
        if (backButton != null)
            backButton.onClick.RemoveListener(OnBack);
    }

    public void Show()
    {
        panel.SetActive(true);
        RefreshButtons();
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    private void RefreshButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (buttons.Count == 0)
            CreateButtons();

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Setup(i + 1, (i + 1) <= unlockedLevel);
        }
    }

    private void CreateButtons()
    {
        for (int i = contentParent.childCount - 1; i >= 0; i--)
            Destroy(contentParent.GetChild(i).gameObject);

        buttons.Clear();

        for (int i = 0; i < LevelDatabase.TotalLevels; i++)
        {
            GameObject obj = Instantiate(levelButtonPrefab, contentParent);
            obj.SetActive(true);
            LevelSelectButton btn = obj.GetComponent<LevelSelectButton>();
            Debug.Assert(btn != null, "LevelSelectButton component missing on prefab!");
            buttons.Add(btn);
        }
    }

    private void OnBack()
    {
        Hide();
    }
}
