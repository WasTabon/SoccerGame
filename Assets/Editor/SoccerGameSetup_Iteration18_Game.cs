using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration18_Game : EditorWindow
{
    [MenuItem("SoccerGame/Setup Lives - Game (Iteration 18)")]
    public static void Setup()
    {
        CreateLivesDisplay();
        EnsureLivesManager();
        Debug.Log("Iteration 18 Game setup complete!");
    }

    private static Transform GetUIParent()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        Debug.Assert(canvasObj != null, "GameCanvas not found!");
        Transform safeArea = canvasObj.transform.Find("SafeAreaPanel");
        return safeArea != null ? safeArea : canvasObj.transform;
    }

    private static void CreateLivesDisplay()
    {
        Transform uiParent = GetUIParent();

        Transform existing = uiParent.Find("LivesGamePanel");
        GameObject panelObj;
        if (existing != null)
            panelObj = existing.gameObject;
        else
        {
            panelObj = new GameObject("LivesGamePanel");
            panelObj.transform.SetParent(uiParent, false);
        }

        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        if (panelRect == null) panelRect = panelObj.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0, 1);
        panelRect.anchorMax = new Vector2(0, 1);
        panelRect.pivot = new Vector2(0, 1);
        panelRect.anchoredPosition = new Vector2(20, -230);
        panelRect.sizeDelta = new Vector2(120, 40);

        Image bg = panelObj.GetComponent<Image>();
        if (bg == null) bg = panelObj.AddComponent<Image>();
        bg.color = new Color(0, 0, 0, 0.5f);

        TextMeshProUGUI heartTmp = FindOrCreateTMP(panelObj.transform, "HeartIcon",
            new Vector2(0, 0), new Vector2(0.4f, 1), "<color=#FF4444>♥</color>", 24, FontStyles.Normal);

        TextMeshProUGUI livesTmp = FindOrCreateTMP(panelObj.transform, "LivesCount",
            new Vector2(0.4f, 0), new Vector2(1f, 1), "5", 24, FontStyles.Bold);

        LivesUI livesUI = panelObj.GetComponent<LivesUI>();
        if (livesUI == null) livesUI = panelObj.AddComponent<LivesUI>();
        livesUI.livesText = livesTmp;
        livesUI.timerText = null;

        EditorUtility.SetDirty(panelObj);
    }

    private static void EnsureLivesManager()
    {
        if (GameObject.Find("LivesManager") != null) return;

        GameObject obj = new GameObject("LivesManager");
        obj.AddComponent<LivesManager>();
        EditorUtility.SetDirty(obj);
    }

    private static TextMeshProUGUI FindOrCreateTMP(Transform parent, string name,
        Vector2 anchorMin, Vector2 anchorMax, string text, float fontSize, FontStyles style)
    {
        Transform existing = parent.Find(name);
        GameObject obj;
        if (existing != null) obj = existing.gameObject;
        else
        {
            obj = new GameObject(name);
            obj.transform.SetParent(parent, false);
        }

        RectTransform rect = obj.GetComponent<RectTransform>();
        if (rect == null) rect = obj.AddComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
        if (tmp == null) tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        tmp.fontStyle = style;
        return tmp;
    }
}
