using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration10_Menu : EditorWindow
{
    [MenuItem("SoccerGame/Setup Final Polish - Menu (Iteration 10)")]
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

        CreateSceneTransition();
        CreateMenuAnimations();
        EditorSceneManager.SaveOpenScenes();
        Debug.Log("Iteration 10 Menu setup complete!");
    }

    private static void CreateSceneTransition()
    {
        GameObject canvasObj = GameObject.Find("MenuCanvas");
        Debug.Assert(canvasObj != null, "MenuCanvas not found!");

        GameObject obj = GameObject.Find("SceneTransition");
        if (obj == null)
            obj = new GameObject("SceneTransition");

        SceneTransition st = obj.GetComponent<SceneTransition>();
        if (st == null) st = obj.AddComponent<SceneTransition>();

        Transform fadeT = canvasObj.transform.Find("TransitionFade");
        GameObject fadeObj;
        if (fadeT != null)
            fadeObj = fadeT.gameObject;
        else
        {
            fadeObj = new GameObject("TransitionFade");
            fadeObj.transform.SetParent(canvasObj.transform, false);
        }

        fadeObj.transform.SetAsLastSibling();

        RectTransform fadeRect = fadeObj.GetComponent<RectTransform>();
        if (fadeRect == null) fadeRect = fadeObj.AddComponent<RectTransform>();
        fadeRect.anchorMin = Vector2.zero;
        fadeRect.anchorMax = Vector2.one;
        fadeRect.offsetMin = Vector2.zero;
        fadeRect.offsetMax = Vector2.zero;

        Image fadeImg = fadeObj.GetComponent<Image>();
        if (fadeImg == null) fadeImg = fadeObj.AddComponent<Image>();
        fadeImg.color = Color.black;
        fadeImg.raycastTarget = true;

        st.fadeImage = fadeImg;

        EditorUtility.SetDirty(obj);
        EditorUtility.SetDirty(fadeObj);
    }

    private static void CreateMenuAnimations()
    {
        GameObject canvasObj = GameObject.Find("MenuCanvas");
        Debug.Assert(canvasObj != null, "MenuCanvas not found!");

        Transform menuRoot = canvasObj.transform.Find("MenuRoot");
        if (menuRoot == null)
        {
            Debug.LogWarning("MenuRoot not found!");
            return;
        }

        MainMenuAnimations anim = menuRoot.GetComponent<MainMenuAnimations>();
        if (anim == null) anim = menuRoot.gameObject.AddComponent<MainMenuAnimations>();

        Transform title = canvasObj.transform.Find("Title");
        Transform matchBtn = canvasObj.transform.Find("MatchButton");
        Transform endlessBtn = canvasObj.transform.Find("EndlessButton");

        if (title != null) anim.titleRect = title.GetComponent<RectTransform>();
        if (matchBtn != null) anim.matchButtonRect = matchBtn.GetComponent<RectTransform>();
        if (endlessBtn != null) anim.endlessButtonRect = endlessBtn.GetComponent<RectTransform>();

        EditorUtility.SetDirty(menuRoot.gameObject);
    }
}
