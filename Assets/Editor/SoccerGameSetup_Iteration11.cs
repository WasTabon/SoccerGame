using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration11 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Level System - Game (Iteration 11)")]
    public static void Setup()
    {
        RemoveStaticObstacles();
        CreateObstacleSpawner();
        CreateLevelPanel();
        CreateLevelEndUI();
        Debug.Log("Iteration 11 setup complete! Level system added to Game scene.");
    }

    private static void RemoveStaticObstacles()
    {
        GameObject obstacles = GameObject.Find("Obstacles");
        if (obstacles != null)
        {
            for (int i = obstacles.transform.childCount - 1; i >= 0; i--)
                Object.DestroyImmediate(obstacles.transform.GetChild(i).gameObject);
        }
    }

    private static void CreateObstacleSpawner()
    {
        GameObject obj = GameObject.Find("ObstacleSpawner");
        if (obj == null)
            obj = new GameObject("ObstacleSpawner");

        ObstacleSpawner spawner = obj.GetComponent<ObstacleSpawner>();
        if (spawner == null) spawner = obj.AddComponent<ObstacleSpawner>();

        spawner.bounceMaterial = AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>("Assets/SoccerGame/BallBounce.physicsMaterial2D");
        spawner.squareSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/SoccerGame/SquareSprite.asset");

        EditorUtility.SetDirty(obj);
    }

    private static void CreateLevelPanel()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        Debug.Assert(canvasObj != null, "GameCanvas not found!");

        Transform existing = canvasObj.transform.Find("LevelPanel");
        GameObject panelObj;
        if (existing != null)
            panelObj = existing.gameObject;
        else
        {
            panelObj = new GameObject("LevelPanel");
            panelObj.transform.SetParent(canvasObj.transform, false);
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

        TextMeshProUGUI titleText = FindOrCreateTMP(panelObj.transform, "LevelTitleText",
            new Vector2(0, 0.5f), new Vector2(0.5f, 1f), "LEVEL 1", 36, FontStyles.Bold);

        TextMeshProUGUI goalsText = FindOrCreateTMP(panelObj.transform, "GoalsText",
            new Vector2(0.5f, 0.5f), new Vector2(1f, 1f), "0 / 3", 48, FontStyles.Bold);

        TextMeshProUGUI goalsLabel = FindOrCreateTMP(panelObj.transform, "GoalsLabel",
            new Vector2(0, 0), new Vector2(1f, 0.5f), "GOALS", 24, FontStyles.Normal);
        goalsLabel.color = new Color(0.7f, 0.7f, 0.7f);

        LevelUI levelUI = panelObj.GetComponent<LevelUI>();
        if (levelUI == null) levelUI = panelObj.AddComponent<LevelUI>();
        levelUI.levelTitleText = titleText;
        levelUI.goalsText = goalsText;

        panelObj.SetActive(false);
        EditorUtility.SetDirty(panelObj);
    }

    private static void CreateLevelEndUI()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        Debug.Assert(canvasObj != null, "GameCanvas not found!");

        Transform handlerT = canvasObj.transform.Find("LevelEndHandler");
        GameObject handlerObj;
        if (handlerT != null)
            handlerObj = handlerT.gameObject;
        else
        {
            handlerObj = new GameObject("LevelEndHandler");
            handlerObj.transform.SetParent(canvasObj.transform, false);
            RectTransform hRect = handlerObj.AddComponent<RectTransform>();
            hRect.anchorMin = Vector2.zero;
            hRect.anchorMax = Vector2.one;
            hRect.offsetMin = Vector2.zero;
            hRect.offsetMax = Vector2.zero;
        }

        Transform panelT = handlerObj.transform.Find("LevelEndPanel");
        GameObject panelObj;
        if (panelT != null)
            panelObj = panelT.gameObject;
        else
        {
            panelObj = new GameObject("LevelEndPanel");
            panelObj.transform.SetParent(handlerObj.transform, false);
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
            contentObj = contentT.gameObject;
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
        contentRect.sizeDelta = new Vector2(800, 600);
        contentRect.anchoredPosition = Vector2.zero;

        TextMeshProUGUI resultText = FindOrCreateTMP(contentObj.transform, "ResultText",
            new Vector2(0, 0.7f), new Vector2(1, 1), "LEVEL COMPLETE!", 64, FontStyles.Bold);

        TextMeshProUGUI levelInfoText = FindOrCreateTMP(contentObj.transform, "LevelInfoText",
            new Vector2(0, 0.55f), new Vector2(1, 0.7f), "Level 1 cleared!", 40, FontStyles.Normal);

        GameObject retryBtn = CreateButton(contentObj.transform, "RetryButton",
            new Vector2(0.1f, 0.25f), new Vector2(0.48f, 0.45f), "RETRY", new Color(0.8f, 0.4f, 0.2f));

        GameObject nextBtn = CreateButton(contentObj.transform, "NextButton",
            new Vector2(0.52f, 0.25f), new Vector2(0.9f, 0.45f), "NEXT LEVEL", new Color(0.2f, 0.7f, 0.3f));

        GameObject menuBtn = CreateButton(contentObj.transform, "MenuButton",
            new Vector2(0.2f, 0.05f), new Vector2(0.8f, 0.22f), "MENU", new Color(0.5f, 0.5f, 0.5f));

        LevelEndUI levelEndUI = handlerObj.GetComponent<LevelEndUI>();
        if (levelEndUI == null) levelEndUI = handlerObj.AddComponent<LevelEndUI>();
        levelEndUI.panel = panelObj;
        levelEndUI.contentRect = contentRect;
        levelEndUI.resultText = resultText;
        levelEndUI.levelInfoText = levelInfoText;
        levelEndUI.retryButton = retryBtn.GetComponent<Button>();
        levelEndUI.nextButton = nextBtn.GetComponent<Button>();
        levelEndUI.menuButton = menuBtn.GetComponent<Button>();

        panelObj.SetActive(false);

        Transform transitionFade = canvasObj.transform.Find("TransitionFade");
        if (transitionFade != null)
            transitionFade.SetAsLastSibling();

        EditorUtility.SetDirty(handlerObj);
        EditorUtility.SetDirty(panelObj);
    }

    private static TextMeshProUGUI FindOrCreateTMP(Transform parent, string name,
        Vector2 anchorMin, Vector2 anchorMax, string defaultText, float fontSize, FontStyles style)
    {
        Transform existing = parent.Find(name);
        GameObject obj;
        if (existing != null)
            obj = existing.gameObject;
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

    private static GameObject CreateButton(Transform parent, string name,
        Vector2 anchorMin, Vector2 anchorMax, string label, Color color)
    {
        Transform existing = parent.Find(name);
        GameObject btnObj;
        if (existing != null)
            btnObj = existing.gameObject;
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
        btnImg.color = color;

        Button btn = btnObj.GetComponent<Button>();
        if (btn == null) btn = btnObj.AddComponent<Button>();

        Transform labelT = btnObj.transform.Find("Label");
        GameObject labelObj;
        if (labelT != null)
            labelObj = labelT.gameObject;
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
        labelTmp.fontSize = 36;
        labelTmp.alignment = TextAlignmentOptions.Center;
        labelTmp.color = Color.white;
        labelTmp.fontStyle = FontStyles.Bold;

        return btnObj;
    }
}
