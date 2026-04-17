using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration14_Menu : EditorWindow
{
    [MenuItem("SoccerGame/Setup Tutorial - Menu (Iteration 14)")]
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

        CreateTutorialPanel();
        LinkToMainMenu();
        EditorSceneManager.SaveOpenScenes();
        Debug.Log("Iteration 14 setup complete! Tutorial added to MainMenu.");
    }

    private static Transform GetUIParent()
    {
        GameObject canvasObj = GameObject.Find("MenuCanvas");
        Debug.Assert(canvasObj != null, "MenuCanvas not found!");

        Transform safeArea = canvasObj.transform.Find("SafeAreaPanel");
        return safeArea != null ? safeArea : canvasObj.transform;
    }

    private static void CreateTutorialPanel()
    {
        Transform uiParent = GetUIParent();

        Transform existing = uiParent.Find("TutorialPanel");
        GameObject panelObj;
        if (existing != null)
            panelObj = existing.gameObject;
        else
        {
            panelObj = new GameObject("TutorialPanel");
            panelObj.transform.SetParent(uiParent, false);
        }

        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        if (panelRect == null) panelRect = panelObj.AddComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        Image panelBg = panelObj.GetComponent<Image>();
        if (panelBg == null) panelBg = panelObj.AddComponent<Image>();
        panelBg.color = new Color(0, 0, 0, 0.92f);
        panelBg.raycastTarget = true;

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
        contentRect.anchorMin = new Vector2(0.08f, 0.15f);
        contentRect.anchorMax = new Vector2(0.92f, 0.85f);
        contentRect.offsetMin = Vector2.zero;
        contentRect.offsetMax = Vector2.zero;

        Image contentBg = contentObj.GetComponent<Image>();
        if (contentBg == null) contentBg = contentObj.AddComponent<Image>();
        contentBg.color = new Color(0.12f, 0.18f, 0.12f, 1f);

        TextMeshProUGUI titleText = FindOrCreateTMP(contentObj.transform, "TitleText",
            new Vector2(0.05f, 0.82f), new Vector2(0.95f, 0.95f),
            "WELCOME!", 56, FontStyles.Bold, Color.white);

        TextMeshProUGUI bodyText = FindOrCreateTMP(contentObj.transform, "BodyText",
            new Vector2(0.08f, 0.25f), new Vector2(0.92f, 0.8f),
            "Tutorial text here", 34, FontStyles.Normal, new Color(0.85f, 0.85f, 0.85f));
        bodyText.alignment = TextAlignmentOptions.Center;

        TextMeshProUGUI pageText = FindOrCreateTMP(contentObj.transform, "PageIndicator",
            new Vector2(0.3f, 0.15f), new Vector2(0.7f, 0.22f),
            "1 / 5", 28, FontStyles.Normal, new Color(0.6f, 0.6f, 0.6f));

        GameObject nextBtn = CreateButton(contentObj.transform, "NextButton",
            new Vector2(0.55f, 0.03f), new Vector2(0.92f, 0.13f),
            "NEXT", new Color(0.2f, 0.65f, 0.3f), 38);

        GameObject skipBtn = CreateButton(contentObj.transform, "SkipButton",
            new Vector2(0.08f, 0.03f), new Vector2(0.45f, 0.13f),
            "SKIP", new Color(0.45f, 0.45f, 0.45f), 38);

        TutorialUI tutUI = panelObj.GetComponent<TutorialUI>();
        if (tutUI == null) tutUI = panelObj.AddComponent<TutorialUI>();
        tutUI.panel = panelObj;
        tutUI.contentRect = contentRect;
        tutUI.titleText = titleText;
        tutUI.bodyText = bodyText;
        tutUI.pageIndicatorText = pageText;
        tutUI.nextButton = nextBtn.GetComponent<Button>();
        tutUI.skipButton = skipBtn.GetComponent<Button>();

        Transform nextLabelT = nextBtn.transform.Find("Label");
        if (nextLabelT != null)
            tutUI.nextButtonLabel = nextLabelT.GetComponent<TextMeshProUGUI>();

        Transform transitionFade = uiParent.Find("TransitionFade");
        if (transitionFade != null)
        {
            panelObj.transform.SetSiblingIndex(transitionFade.GetSiblingIndex());
            transitionFade.SetAsLastSibling();
        }
        else
        {
            panelObj.transform.SetAsLastSibling();
        }

        panelObj.SetActive(false);

        EditorUtility.SetDirty(panelObj);
    }

    private static void LinkToMainMenu()
    {
        Transform uiParent = GetUIParent();

        Transform menuRoot = uiParent.Find("MenuRoot");
        if (menuRoot == null)
        {
            Debug.LogWarning("MenuRoot not found!");
            return;
        }

        MainMenuUI menuUI = menuRoot.GetComponent<MainMenuUI>();
        if (menuUI == null)
        {
            Debug.LogWarning("MainMenuUI not found on MenuRoot!");
            return;
        }

        Transform tutPanel = uiParent.Find("TutorialPanel");
        if (tutPanel != null)
        {
            TutorialUI tutUI = tutPanel.GetComponent<TutorialUI>();
            menuUI.tutorialUI = tutUI;
        }

        EditorUtility.SetDirty(menuUI);
    }

    private static TextMeshProUGUI FindOrCreateTMP(Transform parent, string name,
        Vector2 anchorMin, Vector2 anchorMax, string defaultText, float fontSize, FontStyles style, Color color)
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
        tmp.color = color;
        tmp.fontStyle = style;

        return tmp;
    }

    private static GameObject CreateButton(Transform parent, string name,
        Vector2 anchorMin, Vector2 anchorMax, string label, Color color, float fontSize)
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
        labelTmp.fontSize = fontSize;
        labelTmp.alignment = TextAlignmentOptions.Center;
        labelTmp.color = Color.white;
        labelTmp.fontStyle = FontStyles.Bold;

        return btnObj;
    }
}
