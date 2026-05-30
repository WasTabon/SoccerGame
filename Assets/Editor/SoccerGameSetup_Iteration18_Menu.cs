using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration18_Menu : EditorWindow
{
    [MenuItem("SoccerGame/Setup Lives + Best Score - Menu (Iteration 18)")]
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
                Debug.LogWarning("MainMenu scene not found!");
                return;
            }
        }

        CreateLivesManager();
        CreateLivesDisplay();
        CreateBestScoreDisplay();
        CreateNoLivesPopup();
        LinkReferences();
        EditorSceneManager.SaveOpenScenes();
        Debug.Log("Iteration 18 Menu setup complete!");
    }

    private static Transform GetUIParent()
    {
        GameObject canvasObj = GameObject.Find("MenuCanvas");
        Debug.Assert(canvasObj != null, "MenuCanvas not found!");
        Transform safeArea = canvasObj.transform.Find("SafeAreaPanel");
        return safeArea != null ? safeArea : canvasObj.transform;
    }

    private static void CreateLivesManager()
    {
        if (GameObject.Find("LivesManager") != null) return;

        GameObject obj = new GameObject("LivesManager");
        obj.AddComponent<LivesManager>();
        EditorUtility.SetDirty(obj);
    }

    private static void CreateLivesDisplay()
    {
        Transform uiParent = GetUIParent();

        Transform existing = uiParent.Find("LivesPanel");
        GameObject panelObj;
        if (existing != null)
            panelObj = existing.gameObject;
        else
        {
            panelObj = new GameObject("LivesPanel");
            panelObj.transform.SetParent(uiParent, false);
        }

        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        if (panelRect == null) panelRect = panelObj.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.3f, 0.88f);
        panelRect.anchorMax = new Vector2(0.7f, 0.94f);
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        Image bg = panelObj.GetComponent<Image>();
        if (bg == null) bg = panelObj.AddComponent<Image>();
        bg.color = new Color(0, 0, 0, 0.5f);

        TextMeshProUGUI heartTmp = FindOrCreateTMP(panelObj.transform, "HeartIcon",
            new Vector2(0, 0), new Vector2(0.25f, 1), "<color=#FF4444>♥</color>", 36, FontStyles.Normal);

        TextMeshProUGUI livesTmp = FindOrCreateTMP(panelObj.transform, "LivesCount",
            new Vector2(0.25f, 0), new Vector2(0.5f, 1), "5", 36, FontStyles.Bold);

        TextMeshProUGUI timerTmp = FindOrCreateTMP(panelObj.transform, "TimerText",
            new Vector2(0.5f, 0), new Vector2(1f, 1), "", 28, FontStyles.Normal);
        timerTmp.color = new Color(0.7f, 0.7f, 0.7f);

        LivesUI livesUI = panelObj.GetComponent<LivesUI>();
        if (livesUI == null) livesUI = panelObj.AddComponent<LivesUI>();
        livesUI.livesText = livesTmp;
        livesUI.timerText = timerTmp;

        EditorUtility.SetDirty(panelObj);
    }

    private static void CreateBestScoreDisplay()
    {
        Transform uiParent = GetUIParent();

        Transform endlessBtn = uiParent.Find("EndlessButton");
        if (endlessBtn == null) return;

        Transform existing = uiParent.Find("BestScoreLabel");
        GameObject obj;
        if (existing != null)
            obj = existing.gameObject;
        else
        {
            obj = new GameObject("BestScoreLabel");
            obj.transform.SetParent(uiParent, false);
        }

        RectTransform rect = obj.GetComponent<RectTransform>();
        if (rect == null) rect = obj.AddComponent<RectTransform>();

        RectTransform endlessRect = endlessBtn.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(endlessRect.anchorMin.x, endlessRect.anchorMin.y - 0.03f);
        rect.anchorMax = new Vector2(endlessRect.anchorMax.x, endlessRect.anchorMin.y);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
        if (tmp == null) tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = "BEST: 0";
        tmp.fontSize = 22;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = new Color(0.6f, 0.6f, 0.6f);
        tmp.fontStyle = FontStyles.Italic;

        EditorUtility.SetDirty(obj);
    }

    private static void CreateNoLivesPopup()
    {
        Transform uiParent = GetUIParent();

        Transform existing = uiParent.Find("NoLivesPopup");
        GameObject popupObj;
        if (existing != null)
            popupObj = existing.gameObject;
        else
        {
            popupObj = new GameObject("NoLivesPopup");
            popupObj.transform.SetParent(uiParent, false);
        }

        RectTransform popupRect = popupObj.GetComponent<RectTransform>();
        if (popupRect == null) popupRect = popupObj.AddComponent<RectTransform>();
        popupRect.anchorMin = Vector2.zero;
        popupRect.anchorMax = Vector2.one;
        popupRect.offsetMin = Vector2.zero;
        popupRect.offsetMax = Vector2.zero;

        Image popupBg = popupObj.GetComponent<Image>();
        if (popupBg == null) popupBg = popupObj.AddComponent<Image>();
        popupBg.color = new Color(0, 0, 0, 0.85f);
        popupBg.raycastTarget = true;

        Transform contentT = popupObj.transform.Find("Content");
        GameObject contentObj;
        if (contentT != null)
            contentObj = contentT.gameObject;
        else
        {
            contentObj = new GameObject("Content");
            contentObj.transform.SetParent(popupObj.transform, false);
        }

        RectTransform contentRect = contentObj.GetComponent<RectTransform>();
        if (contentRect == null) contentRect = contentObj.AddComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0.1f, 0.35f);
        contentRect.anchorMax = new Vector2(0.9f, 0.65f);
        contentRect.offsetMin = Vector2.zero;
        contentRect.offsetMax = Vector2.zero;

        Image contentBg = contentObj.GetComponent<Image>();
        if (contentBg == null) contentBg = contentObj.AddComponent<Image>();
        contentBg.color = new Color(0.15f, 0.2f, 0.15f);

        FindOrCreateTMP(contentObj.transform, "Title",
            new Vector2(0.05f, 0.6f), new Vector2(0.95f, 0.9f), "NO LIVES!", 48, FontStyles.Bold);

        FindOrCreateTMP(contentObj.transform, "Message",
            new Vector2(0.05f, 0.3f), new Vector2(0.95f, 0.6f),
            "Wait for lives to regenerate\nor come back later!", 28, FontStyles.Normal);

        Transform okBtnT = contentObj.transform.Find("OKButton");
        GameObject okBtnObj;
        if (okBtnT != null)
            okBtnObj = okBtnT.gameObject;
        else
        {
            okBtnObj = new GameObject("OKButton");
            okBtnObj.transform.SetParent(contentObj.transform, false);
        }

        RectTransform okRect = okBtnObj.GetComponent<RectTransform>();
        if (okRect == null) okRect = okBtnObj.AddComponent<RectTransform>();
        okRect.anchorMin = new Vector2(0.25f, 0.05f);
        okRect.anchorMax = new Vector2(0.75f, 0.25f);
        okRect.offsetMin = Vector2.zero;
        okRect.offsetMax = Vector2.zero;

        Image okImg = okBtnObj.GetComponent<Image>();
        if (okImg == null) okImg = okBtnObj.AddComponent<Image>();
        okImg.color = new Color(0.5f, 0.5f, 0.5f);

        Button okBtn = okBtnObj.GetComponent<Button>();
        if (okBtn == null) okBtn = okBtnObj.AddComponent<Button>();
        okBtn.onClick.RemoveAllListeners();

        NoLivesPopupClose closer = okBtnObj.GetComponent<NoLivesPopupClose>();
        if (closer == null) closer = okBtnObj.AddComponent<NoLivesPopupClose>();

        FindOrCreateTMP(okBtnObj.transform, "Label",
            Vector2.zero, Vector2.one, "OK", 36, FontStyles.Bold);

        Transform transitionFade = uiParent.Find("TransitionFade");
        if (transitionFade != null)
        {
            popupObj.transform.SetSiblingIndex(transitionFade.GetSiblingIndex());
            transitionFade.SetAsLastSibling();
        }

        popupObj.SetActive(false);
        EditorUtility.SetDirty(popupObj);
    }

    private static void LinkReferences()
    {
        Transform uiParent = GetUIParent();

        Transform menuRoot = uiParent.Find("MenuRoot");
        if (menuRoot == null) return;

        MainMenuUI menuUI = menuRoot.GetComponent<MainMenuUI>();
        if (menuUI == null) return;

        Transform noLivesPopup = uiParent.Find("NoLivesPopup");
        if (noLivesPopup != null)
            menuUI.noLivesPopup = noLivesPopup.gameObject;

        Transform bestScore = uiParent.Find("BestScoreLabel");
        if (bestScore != null)
            menuUI.bestScoreText = bestScore.GetComponent<TextMeshProUGUI>();

        EditorUtility.SetDirty(menuUI);
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
