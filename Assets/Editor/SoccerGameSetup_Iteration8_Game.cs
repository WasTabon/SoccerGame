using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration8_Game : EditorWindow
{
    [MenuItem("SoccerGame/Setup Game Scene (Iteration 8)")]
    public static void Setup()
    {
        SaveCurrentAsGameScene();
        AddMenuButtonToMatchEnd();
        AddMenuButtonToEndlessPanel();
        EditorSceneManager.SaveOpenScenes();
        Debug.Log("Iteration 8 Game scene setup complete!");
    }

    private static void SaveCurrentAsGameScene()
    {
        if (!AssetDatabase.IsValidFolder("Assets/SoccerGame/Scenes"))
            AssetDatabase.CreateFolder("Assets/SoccerGame", "Scenes");

        string scenePath = "Assets/SoccerGame/Scenes/Game.unity";
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        if (scene.path != scenePath)
        {
            EditorSceneManager.SaveScene(scene, scenePath);
        }

        AddSceneToBuildSettings(scenePath);
    }

    private static void AddMenuButtonToMatchEnd()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        if (canvasObj == null)
        {
            Debug.LogWarning("GameCanvas not found!");
            return;
        }

        Transform matchEndPanel = canvasObj.transform.Find("MatchEndPanel");
        if (matchEndPanel == null)
        {
            Debug.LogWarning("MatchEndPanel not found!");
            return;
        }

        Transform content = matchEndPanel.Find("Content");
        if (content == null)
        {
            Debug.LogWarning("Content not found in MatchEndPanel!");
            return;
        }

        Transform restartBtnT = content.Find("RestartButton");
        if (restartBtnT != null)
        {
            RectTransform restartRect = restartBtnT.GetComponent<RectTransform>();
            restartRect.anchorMin = new Vector2(0.15f, 0.15f);
            restartRect.anchorMax = new Vector2(0.85f, 0.35f);
            restartRect.offsetMin = Vector2.zero;
            restartRect.offsetMax = Vector2.zero;
        }

        GameObject menuBtn = CreateButton(content, "MenuButton",
            new Vector2(0.15f, -0.05f), new Vector2(0.85f, 0.12f),
            "MENU", new Color(0.5f, 0.5f, 0.5f));

        MatchEndUI matchEndUI = matchEndPanel.GetComponent<MatchEndUI>();
        if (matchEndUI != null)
        {
            matchEndUI.menuButton = menuBtn.GetComponent<Button>();
            EditorUtility.SetDirty(matchEndUI);
        }
    }

    private static void AddMenuButtonToEndlessPanel()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        if (canvasObj == null) return;

        Transform endlessPanel = canvasObj.transform.Find("EndlessPanel");
        if (endlessPanel == null) return;

        GameObject menuBtn = CreateButton(endlessPanel, "EndlessMenuButton",
            new Vector2(0.7f, 0f), new Vector2(1f, 0.2f),
            "MENU", new Color(0.5f, 0.5f, 0.5f));

        RectTransform rect = menuBtn.GetComponent<RectTransform>();
        rect.offsetMin = new Vector2(-10, 5);
        rect.offsetMax = new Vector2(-10, -5);

        Button btn = menuBtn.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();

        EndlessMenuButton emb = menuBtn.GetComponent<EndlessMenuButton>();
        if (emb == null) emb = menuBtn.AddComponent<EndlessMenuButton>();

        EditorUtility.SetDirty(menuBtn);
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
