using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SoccerGameSetup_Iteration8_Menu : EditorWindow
{
    [MenuItem("SoccerGame/Setup MainMenu Scene (Iteration 8)")]
    public static void Setup()
    {
        string scenePath = "Assets/SoccerGame/Scenes/MainMenu.unity";

        if (!AssetDatabase.IsValidFolder("Assets/SoccerGame/Scenes"))
            AssetDatabase.CreateFolder("Assets/SoccerGame", "Scenes");

        var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        if (currentScene.path != scenePath)
        {
            if (!System.IO.File.Exists(scenePath))
            {
                var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(newScene, scenePath);
            }
            EditorSceneManager.OpenScene(scenePath);
        }

        SetupCamera();
        CreateEventSystem();
        Canvas canvas = FindOrCreateCanvas();
        CreateMenuUI(canvas);

        EditorSceneManager.SaveOpenScenes();
        AddSceneToBuildSettings(scenePath);

        string gameScenePath = GetGameScenePath();
        if (gameScenePath != null)
            AddSceneToBuildSettings(gameScenePath);

        Debug.Log("Iteration 8 MainMenu setup complete! Don't forget to save the Game scene as 'Assets/SoccerGame/Scenes/Game.unity' if not done.");
    }

    private static void SetupCamera()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            GameObject camObj = new GameObject("Main Camera");
            cam = camObj.AddComponent<Camera>();
            camObj.tag = "MainCamera";
        }
        cam.orthographic = true;
        cam.orthographicSize = 10f;
        cam.transform.position = new Vector3(0, 0, -10);
        cam.backgroundColor = new Color(0.1f, 0.15f, 0.1f);
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
        GameObject canvasObj = GameObject.Find("MenuCanvas");
        if (canvasObj != null) return canvasObj.GetComponent<Canvas>();

        canvasObj = new GameObject("MenuCanvas");
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

    private static void CreateMenuUI(Canvas canvas)
    {
        Transform titleT = canvas.transform.Find("Title");
        GameObject titleObj;
        if (titleT != null)
            titleObj = titleT.gameObject;
        else
        {
            titleObj = new GameObject("Title");
            titleObj.transform.SetParent(canvas.transform, false);
        }

        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        if (titleRect == null) titleRect = titleObj.AddComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0, 0.65f);
        titleRect.anchorMax = new Vector2(1, 0.85f);
        titleRect.offsetMin = Vector2.zero;
        titleRect.offsetMax = Vector2.zero;

        TextMeshProUGUI titleTmp = titleObj.GetComponent<TextMeshProUGUI>();
        if (titleTmp == null) titleTmp = titleObj.AddComponent<TextMeshProUGUI>();
        titleTmp.text = "SOCCER\nGAME";
        titleTmp.fontSize = 100;
        titleTmp.alignment = TextAlignmentOptions.Center;
        titleTmp.color = Color.white;
        titleTmp.fontStyle = FontStyles.Bold;

        GameObject matchBtn = CreateButton(canvas.transform, "MatchButton",
            new Vector2(0.15f, 0.4f), new Vector2(0.85f, 0.5f),
            "MATCH MODE", new Color(0.2f, 0.6f, 0.3f));

        GameObject endlessBtn = CreateButton(canvas.transform, "EndlessButton",
            new Vector2(0.15f, 0.25f), new Vector2(0.85f, 0.35f),
            "ENDLESS MODE", new Color(0.3f, 0.3f, 0.7f));

        Transform menuRoot = canvas.transform.Find("MenuRoot");
        GameObject menuRootObj;
        if (menuRoot != null)
            menuRootObj = menuRoot.gameObject;
        else
        {
            menuRootObj = new GameObject("MenuRoot");
            menuRootObj.transform.SetParent(canvas.transform, false);
        }

        MainMenuUI menuUI = menuRootObj.GetComponent<MainMenuUI>();
        if (menuUI == null) menuUI = menuRootObj.AddComponent<MainMenuUI>();
        menuUI.matchButton = matchBtn.GetComponent<Button>();
        menuUI.endlessButton = endlessBtn.GetComponent<Button>();

        EditorUtility.SetDirty(menuRootObj);
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
        labelTmp.fontSize = 52;
        labelTmp.alignment = TextAlignmentOptions.Center;
        labelTmp.color = Color.white;
        labelTmp.fontStyle = FontStyles.Bold;

        return btnObj;
    }

    private static string GetGameScenePath()
    {
        string[] guids = AssetDatabase.FindAssets("t:Scene Game", new[] { "Assets/SoccerGame/Scenes" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (path.EndsWith("/Game.unity")) return path;
        }
        return null;
    }

    private static void AddSceneToBuildSettings(string scenePath)
    {
        var scenes = new System.Collections.Generic.List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
        foreach (var s in scenes)
        {
            if (s.path == scenePath) return;
        }
        scenes.Add(new EditorBuildSettingsScene(scenePath, true));
        EditorBuildSettings.scenes = scenes.ToArray();
    }
}
