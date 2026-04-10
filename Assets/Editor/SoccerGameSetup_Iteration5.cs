using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SoccerGameSetup_Iteration5 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Score UI (Iteration 5)")]
    public static void Setup()
    {
        CreateEventSystem();
        Canvas canvas = FindOrCreateCanvas();
        CreateScoreUI(canvas);
        CreateMatchEndUI(canvas);
        Debug.Log("Iteration 5 setup complete! Score UI and Match End panel created.");
    }

    private static void CreateEventSystem()
    {
        if (Object.FindObjectOfType<EventSystem>() != null) return;
        GameObject obj = new GameObject("EventSystem");
        obj.AddComponent<EventSystem>();
        obj.AddComponent<StandaloneInputModule>();
    }

    private static Canvas FindOrCreateCanvas()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        if (canvasObj != null) return canvasObj.GetComponent<Canvas>();

        canvasObj = new GameObject("GameCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        scaler.matchWidthOrHeight = 0.5f;

        canvasObj.AddComponent<GraphicRaycaster>();

        return canvas;
    }

    private static void CreateScoreUI(Canvas canvas)
    {
        Transform existing = canvas.transform.Find("ScorePanel");
        GameObject panelObj;
        if (existing != null)
        {
            panelObj = existing.gameObject;
        }
        else
        {
            panelObj = new GameObject("ScorePanel");
            panelObj.transform.SetParent(canvas.transform, false);
        }

        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        if (panelRect == null) panelRect = panelObj.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0, 1);
        panelRect.anchorMax = new Vector2(1, 1);
        panelRect.pivot = new Vector2(0.5f, 1);
        panelRect.anchoredPosition = new Vector2(0, -20);
        panelRect.sizeDelta = new Vector2(0, 120);

        Image panelBg = panelObj.GetComponent<Image>();
        if (panelBg == null) panelBg = panelObj.AddComponent<Image>();
        panelBg.color = new Color(0, 0, 0, 0.5f);

        Transform scoreTextT = panelObj.transform.Find("ScoreText");
        GameObject scoreTextObj;
        if (scoreTextT != null)
        {
            scoreTextObj = scoreTextT.gameObject;
        }
        else
        {
            scoreTextObj = new GameObject("ScoreText");
            scoreTextObj.transform.SetParent(panelObj.transform, false);
        }

        RectTransform textRect = scoreTextObj.GetComponent<RectTransform>();
        if (textRect == null) textRect = scoreTextObj.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        TextMeshProUGUI scoreTmp = scoreTextObj.GetComponent<TextMeshProUGUI>();
        if (scoreTmp == null) scoreTmp = scoreTextObj.AddComponent<TextMeshProUGUI>();
        scoreTmp.text = "0 - 0";
        scoreTmp.fontSize = 72;
        scoreTmp.alignment = TextAlignmentOptions.Center;
        scoreTmp.color = Color.white;
        scoreTmp.fontStyle = FontStyles.Bold;

        ScoreUI scoreUI = panelObj.GetComponent<ScoreUI>();
        if (scoreUI == null) scoreUI = panelObj.AddComponent<ScoreUI>();
        scoreUI.scoreText = scoreTmp;

        EditorUtility.SetDirty(panelObj);
    }

    private static void CreateMatchEndUI(Canvas canvas)
    {
        Transform existing = canvas.transform.Find("MatchEndPanel");
        GameObject panelObj;
        if (existing != null)
        {
            panelObj = existing.gameObject;
        }
        else
        {
            panelObj = new GameObject("MatchEndPanel");
            panelObj.transform.SetParent(canvas.transform, false);
        }

        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        if (panelRect == null) panelRect = panelObj.AddComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        Image panelBg = panelObj.GetComponent<Image>();
        if (panelBg == null) panelBg = panelObj.AddComponent<Image>();
        panelBg.color = new Color(0, 0, 0, 0.85f);

        Transform contentT = panelObj.transform.Find("Content");
        GameObject contentObj;
        if (contentT != null)
        {
            contentObj = contentT.gameObject;
        }
        else
        {
            contentObj = new GameObject("Content");
            contentObj.transform.SetParent(panelObj.transform, false);
        }

        RectTransform contentRect = contentObj.GetComponent<RectTransform>();
        if (contentRect == null) contentRect = contentObj.AddComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0.5f, 0.5f);
        contentRect.anchorMax = new Vector2(0.5f, 0.5f);
        contentRect.pivot = new Vector2(0.5f, 0.5f);
        contentRect.sizeDelta = new Vector2(800, 500);
        contentRect.anchoredPosition = Vector2.zero;

        TextMeshProUGUI resultText = FindOrCreateTMP(contentObj.transform, "ResultText",
            new Vector2(0, 0.55f), new Vector2(1, 1), "YOU WIN!", 80, FontStyles.Bold);

        TextMeshProUGUI finalScoreText = FindOrCreateTMP(contentObj.transform, "FinalScoreText",
            new Vector2(0, 0.3f), new Vector2(1, 0.55f), "5 - 3", 56, FontStyles.Normal);

        GameObject btnObj = FindOrCreateButton(contentObj.transform, "RestartButton",
            new Vector2(0.15f, 0f), new Vector2(0.85f, 0.3f), "RESTART");

        MatchEndUI matchEndUI = panelObj.GetComponent<MatchEndUI>();
        if (matchEndUI == null) matchEndUI = panelObj.AddComponent<MatchEndUI>();
        matchEndUI.panel = panelObj;
        matchEndUI.resultText = resultText;
        matchEndUI.finalScoreText = finalScoreText;
        matchEndUI.restartButton = btnObj.GetComponent<Button>();

        panelObj.SetActive(false);

        EditorUtility.SetDirty(panelObj);
    }

    private static TextMeshProUGUI FindOrCreateTMP(Transform parent, string name,
        Vector2 anchorMin, Vector2 anchorMax, string defaultText, float fontSize, FontStyles style)
    {
        Transform existing = parent.Find(name);
        GameObject obj;
        if (existing != null)
        {
            obj = existing.gameObject;
        }
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
        tmp.text = defaultText;
        tmp.fontSize = fontSize;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        tmp.fontStyle = style;

        return tmp;
    }

    private static GameObject FindOrCreateButton(Transform parent, string name,
        Vector2 anchorMin, Vector2 anchorMax, string label)
    {
        Transform existing = parent.Find(name);
        GameObject btnObj;
        if (existing != null)
        {
            btnObj = existing.gameObject;
        }
        else
        {
            btnObj = new GameObject(name);
            btnObj.transform.SetParent(parent, false);
        }

        RectTransform btnRect = btnObj.GetComponent<RectTransform>();
        if (btnRect == null) btnRect = btnObj.AddComponent<RectTransform>();
        btnRect.anchorMin = anchorMin;
        btnRect.anchorMax = anchorMax;
        btnRect.offsetMin = Vector2.zero;
        btnRect.offsetMax = Vector2.zero;

        Image btnImg = btnObj.GetComponent<Image>();
        if (btnImg == null) btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.2f, 0.7f, 0.3f);

        Button btn = btnObj.GetComponent<Button>();
        if (btn == null) btn = btnObj.AddComponent<Button>();

        Transform labelT = btnObj.transform.Find("Label");
        GameObject labelObj;
        if (labelT != null)
        {
            labelObj = labelT.gameObject;
        }
        else
        {
            labelObj = new GameObject("Label");
            labelObj.transform.SetParent(btnObj.transform, false);
        }

        RectTransform labelRect = labelObj.GetComponent<RectTransform>();
        if (labelRect == null) labelRect = labelObj.AddComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;

        TextMeshProUGUI labelTmp = labelObj.GetComponent<TextMeshProUGUI>();
        if (labelTmp == null) labelTmp = labelObj.AddComponent<TextMeshProUGUI>();
        labelTmp.text = label;
        labelTmp.fontSize = 48;
        labelTmp.alignment = TextAlignmentOptions.Center;
        labelTmp.color = Color.white;
        labelTmp.fontStyle = FontStyles.Bold;

        return btnObj;
    }
}
