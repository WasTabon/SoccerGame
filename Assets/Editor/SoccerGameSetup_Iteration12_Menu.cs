using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration12_Menu : EditorWindow
{
    [MenuItem("SoccerGame/Setup Level Select - Menu (Iteration 12)")]
    public static void Setup()
    {
        string scenePath = "Assets/SoccerGame/Scenes/MainMenu.unity";
        var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (currentScene.path != scenePath)
        {
            if (System.IO.File.Exists(scenePath))
                EditorSceneManager.OpenScene(scenePath);
            else
            {
                Debug.LogWarning("MainMenu scene not found! Run Iteration 8 Menu setup first.");
                return;
            }
        }

        CreateLevelButtonPrefab();
        AddLevelsButton();
        CreateLevelSelectPanel();
        LinkReferences();
        EditorSceneManager.SaveOpenScenes();
        Debug.Log("Iteration 12 Menu setup complete!");
    }

    private static void CreateLevelButtonPrefab()
    {
        string prefabPath = "Assets/SoccerGame/Prefabs/LevelButton.prefab";

        if (!AssetDatabase.IsValidFolder("Assets/SoccerGame/Prefabs"))
            AssetDatabase.CreateFolder("Assets/SoccerGame", "Prefabs");

        GameObject existing = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (existing != null)
        {
            Debug.Log("LevelButton prefab already exists, updating...");
        }

        GameObject btnObj = new GameObject("LevelButton");

        RectTransform btnRect = btnObj.AddComponent<RectTransform>();
        btnRect.sizeDelta = new Vector2(200, 220);

        Image btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.2f, 0.55f, 0.3f);

        Button btn = btnObj.AddComponent<Button>();
        ColorBlock colors = btn.colors;
        colors.highlightedColor = new Color(0.3f, 0.7f, 0.4f);
        colors.pressedColor = new Color(0.15f, 0.4f, 0.2f);
        colors.disabledColor = new Color(0.3f, 0.3f, 0.3f);
        btn.colors = colors;

        GameObject numberObj = new GameObject("LevelNumber");
        numberObj.transform.SetParent(btnObj.transform, false);
        RectTransform numRect = numberObj.AddComponent<RectTransform>();
        numRect.anchorMin = new Vector2(0, 0.35f);
        numRect.anchorMax = new Vector2(1, 0.85f);
        numRect.offsetMin = Vector2.zero;
        numRect.offsetMax = Vector2.zero;
        TextMeshProUGUI numTmp = numberObj.AddComponent<TextMeshProUGUI>();
        numTmp.text = "1";
        numTmp.fontSize = 56;
        numTmp.alignment = TextAlignmentOptions.Center;
        numTmp.color = Color.white;
        numTmp.fontStyle = FontStyles.Bold;

        GameObject goalsObj = new GameObject("GoalsInfo");
        goalsObj.transform.SetParent(btnObj.transform, false);
        RectTransform goalsRect = goalsObj.AddComponent<RectTransform>();
        goalsRect.anchorMin = new Vector2(0, 0.05f);
        goalsRect.anchorMax = new Vector2(1, 0.35f);
        goalsRect.offsetMin = Vector2.zero;
        goalsRect.offsetMax = Vector2.zero;
        TextMeshProUGUI goalsTmp = goalsObj.AddComponent<TextMeshProUGUI>();
        goalsTmp.text = "3 goals";
        goalsTmp.fontSize = 24;
        goalsTmp.alignment = TextAlignmentOptions.Center;
        goalsTmp.color = new Color(0.85f, 0.85f, 0.85f);
        goalsTmp.fontStyle = FontStyles.Normal;

        GameObject lockObj = new GameObject("LockIcon");
        lockObj.transform.SetParent(btnObj.transform, false);
        RectTransform lockRect = lockObj.AddComponent<RectTransform>();
        lockRect.anchorMin = new Vector2(0.2f, 0.25f);
        lockRect.anchorMax = new Vector2(0.8f, 0.85f);
        lockRect.offsetMin = Vector2.zero;
        lockRect.offsetMax = Vector2.zero;
        TextMeshProUGUI lockTmp = lockObj.AddComponent<TextMeshProUGUI>();
        lockTmp.text = "LOCKED";
        lockTmp.fontSize = 28;
        lockTmp.alignment = TextAlignmentOptions.Center;
        lockTmp.color = new Color(0.6f, 0.6f, 0.6f);

        LevelSelectButton lsb = btnObj.AddComponent<LevelSelectButton>();
        lsb.levelNumberText = numTmp;
        lsb.goalsInfoText = goalsTmp;
        lsb.lockIcon = lockObj;
        lsb.button = btn;
        lsb.background = btnImg;

        if (existing != null)
        {
            PrefabUtility.SaveAsPrefabAsset(btnObj, prefabPath);
        }
        else
        {
            PrefabUtility.SaveAsPrefabAsset(btnObj, prefabPath);
        }

        Object.DestroyImmediate(btnObj);
        Debug.Log("LevelButton prefab saved to " + prefabPath);
    }

    private static void AddLevelsButton()
    {
        GameObject canvasObj = GameObject.Find("MenuCanvas");
        Debug.Assert(canvasObj != null, "MenuCanvas not found!");

        Transform matchBtn = canvasObj.transform.Find("MatchButton");
        Transform endlessBtn = canvasObj.transform.Find("EndlessButton");

        if (matchBtn != null)
        {
            RectTransform r = matchBtn.GetComponent<RectTransform>();
            r.anchorMin = new Vector2(0.15f, 0.5f);
            r.anchorMax = new Vector2(0.85f, 0.58f);
            r.offsetMin = Vector2.zero;
            r.offsetMax = Vector2.zero;
        }

        if (endlessBtn != null)
        {
            RectTransform r = endlessBtn.GetComponent<RectTransform>();
            r.anchorMin = new Vector2(0.15f, 0.38f);
            r.anchorMax = new Vector2(0.85f, 0.46f);
            r.offsetMin = Vector2.zero;
            r.offsetMax = Vector2.zero;
        }

        Transform existingLevels = canvasObj.transform.Find("LevelsButton");
        GameObject levelsObj;
        if (existingLevels != null)
            levelsObj = existingLevels.gameObject;
        else
        {
            levelsObj = new GameObject("LevelsButton");
            levelsObj.transform.SetParent(canvasObj.transform, false);
        }

        RectTransform btnRect = levelsObj.GetComponent<RectTransform>();
        if (btnRect == null) btnRect = levelsObj.AddComponent<RectTransform>();
        btnRect.anchorMin = new Vector2(0.15f, 0.26f);
        btnRect.anchorMax = new Vector2(0.85f, 0.34f);
        btnRect.offsetMin = Vector2.zero;
        btnRect.offsetMax = Vector2.zero;

        Image btnImg = levelsObj.GetComponent<Image>();
        if (btnImg == null) btnImg = levelsObj.AddComponent<Image>();
        btnImg.color = new Color(0.7f, 0.5f, 0.2f);

        Button btn = levelsObj.GetComponent<Button>();
        if (btn == null) btn = levelsObj.AddComponent<Button>();

        Transform labelT = levelsObj.transform.Find("Label");
        GameObject labelObj;
        if (labelT != null)
            labelObj = labelT.gameObject;
        else
        {
            labelObj = new GameObject("Label");
            labelObj.transform.SetParent(levelsObj.transform, false);
        }

        RectTransform labelRect = labelObj.GetComponent<RectTransform>();
        if (labelRect == null) labelRect = labelObj.AddComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;

        TextMeshProUGUI labelTmp = labelObj.GetComponent<TextMeshProUGUI>();
        if (labelTmp == null) labelTmp = labelObj.AddComponent<TextMeshProUGUI>();
        labelTmp.text = "LEVELS";
        labelTmp.fontSize = 52;
        labelTmp.alignment = TextAlignmentOptions.Center;
        labelTmp.color = Color.white;
        labelTmp.fontStyle = FontStyles.Bold;

        EditorUtility.SetDirty(levelsObj);
    }

    private static void CreateLevelSelectPanel()
    {
        GameObject canvasObj = GameObject.Find("MenuCanvas");
        Debug.Assert(canvasObj != null, "MenuCanvas not found!");

        Transform existingPanel = canvasObj.transform.Find("LevelSelectPanel");
        GameObject panelObj;
        if (existingPanel != null)
            panelObj = existingPanel.gameObject;
        else
        {
            panelObj = new GameObject("LevelSelectPanel");
            panelObj.transform.SetParent(canvasObj.transform, false);
        }

        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        if (panelRect == null) panelRect = panelObj.AddComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        Image panelBg = panelObj.GetComponent<Image>();
        if (panelBg == null) panelBg = panelObj.AddComponent<Image>();
        panelBg.color = new Color(0.08f, 0.12f, 0.08f, 1f);

        CreateHeader(panelObj.transform);
        CreateScrollArea(panelObj.transform);
        CreateBackButton(panelObj.transform);

        Transform transitionFade = canvasObj.transform.Find("TransitionFade");
        if (transitionFade != null)
            transitionFade.SetAsLastSibling();

        panelObj.SetActive(false);
        EditorUtility.SetDirty(panelObj);
    }

    private static void CreateHeader(Transform parent)
    {
        Transform existing = parent.Find("Header");
        GameObject obj;
        if (existing != null)
            obj = existing.gameObject;
        else
        {
            obj = new GameObject("Header");
            obj.transform.SetParent(parent, false);
        }

        RectTransform rect = obj.GetComponent<RectTransform>();
        if (rect == null) rect = obj.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0.9f);
        rect.anchorMax = new Vector2(1, 1f);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        Image bg = obj.GetComponent<Image>();
        if (bg == null) bg = obj.AddComponent<Image>();
        bg.color = new Color(0, 0, 0, 0.5f);

        Transform titleT = obj.transform.Find("Title");
        GameObject titleObj;
        if (titleT != null)
            titleObj = titleT.gameObject;
        else
        {
            titleObj = new GameObject("Title");
            titleObj.transform.SetParent(obj.transform, false);
        }

        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        if (titleRect == null) titleRect = titleObj.AddComponent<RectTransform>();
        titleRect.anchorMin = Vector2.zero;
        titleRect.anchorMax = Vector2.one;
        titleRect.offsetMin = Vector2.zero;
        titleRect.offsetMax = Vector2.zero;

        TextMeshProUGUI titleTmp = titleObj.GetComponent<TextMeshProUGUI>();
        if (titleTmp == null) titleTmp = titleObj.AddComponent<TextMeshProUGUI>();
        titleTmp.text = "SELECT LEVEL";
        titleTmp.fontSize = 52;
        titleTmp.alignment = TextAlignmentOptions.Center;
        titleTmp.color = Color.white;
        titleTmp.fontStyle = FontStyles.Bold;
    }

    private static void CreateScrollArea(Transform parent)
    {
        Transform existing = parent.Find("ScrollArea");
        GameObject scrollObj;
        if (existing != null)
            scrollObj = existing.gameObject;
        else
        {
            scrollObj = new GameObject("ScrollArea");
            scrollObj.transform.SetParent(parent, false);
        }

        RectTransform scrollRect = scrollObj.GetComponent<RectTransform>();
        if (scrollRect == null) scrollRect = scrollObj.AddComponent<RectTransform>();
        scrollRect.anchorMin = new Vector2(0.05f, 0.1f);
        scrollRect.anchorMax = new Vector2(0.95f, 0.88f);
        scrollRect.offsetMin = Vector2.zero;
        scrollRect.offsetMax = Vector2.zero;

        Image scrollBg = scrollObj.GetComponent<Image>();
        if (scrollBg == null) scrollBg = scrollObj.AddComponent<Image>();
        scrollBg.color = new Color(0, 0, 0, 0);

        ScrollRect scroll = scrollObj.GetComponent<ScrollRect>();
        if (scroll == null) scroll = scrollObj.AddComponent<ScrollRect>();
        scroll.horizontal = false;
        scroll.vertical = true;
        scroll.movementType = ScrollRect.MovementType.Elastic;

        Mask mask = scrollObj.GetComponent<Mask>();
        if (mask == null) mask = scrollObj.AddComponent<Mask>();
        mask.showMaskGraphic = false;
        scrollBg.color = new Color(0, 0, 0, 0.01f);

        Transform contentT = scrollObj.transform.Find("Content");
        GameObject contentObj;
        if (contentT != null)
            contentObj = contentT.gameObject;
        else
        {
            contentObj = new GameObject("Content");
            contentObj.transform.SetParent(scrollObj.transform, false);
        }

        RectTransform contentRect = contentObj.GetComponent<RectTransform>();
        if (contentRect == null) contentRect = contentObj.AddComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0, 1);
        contentRect.anchorMax = new Vector2(1, 1);
        contentRect.pivot = new Vector2(0.5f, 1);
        contentRect.anchoredPosition = Vector2.zero;

        GridLayoutGroup grid = contentObj.GetComponent<GridLayoutGroup>();
        if (grid == null) grid = contentObj.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(200, 220);
        grid.spacing = new Vector2(30, 30);
        grid.padding = new RectOffset(20, 20, 20, 20);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 3;
        grid.childAlignment = TextAnchor.UpperCenter;

        ContentSizeFitter fitter = contentObj.GetComponent<ContentSizeFitter>();
        if (fitter == null) fitter = contentObj.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        scroll.content = contentRect;
        scroll.viewport = scrollRect;

        EditorUtility.SetDirty(scrollObj);
    }

    private static void CreateBackButton(Transform parent)
    {
        Transform existing = parent.Find("BackButton");
        GameObject btnObj;
        if (existing != null)
            btnObj = existing.gameObject;
        else
        {
            btnObj = new GameObject("BackButton");
            btnObj.transform.SetParent(parent, false);
        }

        RectTransform btnRect = btnObj.GetComponent<RectTransform>();
        if (btnRect == null) btnRect = btnObj.AddComponent<RectTransform>();
        btnRect.anchorMin = new Vector2(0.25f, 0.02f);
        btnRect.anchorMax = new Vector2(0.75f, 0.08f);
        btnRect.offsetMin = Vector2.zero;
        btnRect.offsetMax = Vector2.zero;

        Image btnImg = btnObj.GetComponent<Image>();
        if (btnImg == null) btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.5f, 0.5f, 0.5f);

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
        labelTmp.text = "BACK";
        labelTmp.fontSize = 40;
        labelTmp.alignment = TextAlignmentOptions.Center;
        labelTmp.color = Color.white;
        labelTmp.fontStyle = FontStyles.Bold;

        EditorUtility.SetDirty(btnObj);
    }

    private static void LinkReferences()
    {
        GameObject canvasObj = GameObject.Find("MenuCanvas");
        Debug.Assert(canvasObj != null, "MenuCanvas not found!");

        Transform menuRoot = canvasObj.transform.Find("MenuRoot");
        Debug.Assert(menuRoot != null, "MenuRoot not found!");

        MainMenuUI menuUI = menuRoot.GetComponent<MainMenuUI>();
        Debug.Assert(menuUI != null, "MainMenuUI not found on MenuRoot!");

        Transform levelsBtn = canvasObj.transform.Find("LevelsButton");
        if (levelsBtn != null)
            menuUI.levelsButton = levelsBtn.GetComponent<Button>();

        Transform levelSelectPanel = canvasObj.transform.Find("LevelSelectPanel");
        if (levelSelectPanel == null)
        {
            Debug.LogWarning("LevelSelectPanel not found!");
            return;
        }

        LevelSelectUI selectUI = levelSelectPanel.GetComponent<LevelSelectUI>();
        if (selectUI == null) selectUI = levelSelectPanel.gameObject.AddComponent<LevelSelectUI>();

        selectUI.panel = levelSelectPanel.gameObject;

        Transform scrollArea = levelSelectPanel.Find("ScrollArea");
        if (scrollArea != null)
        {
            Transform content = scrollArea.Find("Content");
            if (content != null)
                selectUI.contentParent = content;
        }

        Transform backBtn = levelSelectPanel.Find("BackButton");
        if (backBtn != null)
            selectUI.backButton = backBtn.GetComponent<Button>();

        string prefabPath = "Assets/SoccerGame/Prefabs/LevelButton.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (prefab != null)
            selectUI.levelButtonPrefab = prefab;
        else
            Debug.LogWarning("LevelButton prefab not found at " + prefabPath);

        menuUI.levelSelectUI = selectUI;

        MainMenuAnimations anim = menuRoot.GetComponent<MainMenuAnimations>();
        if (anim != null)
        {
            Transform lb = canvasObj.transform.Find("LevelsButton");
            if (lb != null)
                anim.levelsButtonRect = lb.GetComponent<RectTransform>();
            EditorUtility.SetDirty(anim);
        }

        EditorUtility.SetDirty(menuUI);
        EditorUtility.SetDirty(selectUI);
    }
}
