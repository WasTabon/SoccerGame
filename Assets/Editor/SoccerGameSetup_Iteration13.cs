using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class SoccerGameSetup_Iteration13 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Safe Area - Game (Iteration 13)")]
    public static void SetupGame()
    {
        AddCameraSafeArea();
        WrapCanvasInSafeArea("GameCanvas");
        EditorSceneManager.SaveOpenScenes();
        Debug.Log("Iteration 13 Game setup complete! Safe area applied.");
    }

    [MenuItem("SoccerGame/Setup Safe Area - Menu (Iteration 13)")]
    public static void SetupMenu()
    {
        string scenePath = "Assets/SoccerGame/Scenes/MainMenu.unity";
        var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (currentScene.path != scenePath)
        {
            if (System.IO.File.Exists(scenePath))
                EditorSceneManager.OpenScene(scenePath);
            else
            {
                Debug.LogWarning("MainMenu scene not found!");
                return;
            }
        }

        WrapCanvasInSafeArea("MenuCanvas");
        EditorSceneManager.SaveOpenScenes();
        Debug.Log("Iteration 13 Menu setup complete! Safe area applied.");
    }

    private static void AddCameraSafeArea()
    {
        Camera cam = Camera.main;
        Debug.Assert(cam != null, "Main Camera not found!");

        CameraSafeArea csa = cam.GetComponent<CameraSafeArea>();
        if (csa == null)
            csa = cam.gameObject.AddComponent<CameraSafeArea>();

        EditorUtility.SetDirty(cam.gameObject);
    }

    private static void WrapCanvasInSafeArea(string canvasName)
    {
        GameObject canvasObj = GameObject.Find(canvasName);
        Debug.Assert(canvasObj != null, canvasName + " not found!");

        Transform safeAreaT = canvasObj.transform.Find("SafeAreaPanel");
        if (safeAreaT != null)
        {
            Debug.Log("SafeAreaPanel already exists on " + canvasName + ", skipping.");
            return;
        }

        List<Transform> children = new List<Transform>();
        for (int i = 0; i < canvasObj.transform.childCount; i++)
            children.Add(canvasObj.transform.GetChild(i));

        GameObject safePanel = new GameObject("SafeAreaPanel");
        safePanel.transform.SetParent(canvasObj.transform, false);

        RectTransform safeRect = safePanel.AddComponent<RectTransform>();
        safeRect.anchorMin = Vector2.zero;
        safeRect.anchorMax = Vector2.one;
        safeRect.offsetMin = Vector2.zero;
        safeRect.offsetMax = Vector2.zero;

        safePanel.AddComponent<SafeArea>();

        safePanel.transform.SetAsFirstSibling();

        foreach (Transform child in children)
        {
            child.SetParent(safePanel.transform, true);
        }

        EditorUtility.SetDirty(canvasObj);
        Debug.Log("Wrapped " + children.Count + " children of " + canvasName + " in SafeAreaPanel.");
    }
}
