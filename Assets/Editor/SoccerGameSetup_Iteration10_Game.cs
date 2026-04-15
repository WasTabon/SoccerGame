using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration10_Game : EditorWindow
{
    [MenuItem("SoccerGame/Setup Final Polish - Game (Iteration 10)")]
    public static void Setup()
    {
        CreateSceneTransition();
        CreateCountdownUI();
        CreateInGameMenuButton();
        Debug.Log("Iteration 10 Game setup complete!");
    }

    private static void CreateSceneTransition()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        Debug.Assert(canvasObj != null, "GameCanvas not found!");

        GameObject obj = GameObject.Find("SceneTransition");
        if (obj == null)
        {
            obj = new GameObject("SceneTransition");
        }

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

    private static void CreateCountdownUI()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        Debug.Assert(canvasObj != null, "GameCanvas not found!");

        GameObject obj = GameObject.Find("CountdownUI");
        if (obj == null)
        {
            obj = new GameObject("CountdownUI");
        }

        CountdownUI cu = obj.GetComponent<CountdownUI>();
        if (cu == null) cu = obj.AddComponent<CountdownUI>();

        Transform textT = canvasObj.transform.Find("CountdownText");
        GameObject textObj;
        if (textT != null)
            textObj = textT.gameObject;
        else
        {
            textObj = new GameObject("CountdownText");
            textObj.transform.SetParent(canvasObj.transform, false);
        }

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        if (textRect == null) textRect = textObj.AddComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.2f, 0.35f);
        textRect.anchorMax = new Vector2(0.8f, 0.65f);
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
        if (tmp == null) tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = "";
        tmp.fontSize = 150;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        tmp.fontStyle = FontStyles.Bold;

        textObj.SetActive(false);

        cu.countdownText = tmp;

        int siblingIndex = canvasObj.transform.Find("TransitionFade") != null
            ? canvasObj.transform.Find("TransitionFade").GetSiblingIndex()
            : canvasObj.transform.childCount;
        textObj.transform.SetSiblingIndex(siblingIndex);

        EditorUtility.SetDirty(obj);
        EditorUtility.SetDirty(textObj);
    }

    private static void CreateInGameMenuButton()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        Debug.Assert(canvasObj != null, "GameCanvas not found!");

        Transform existing = canvasObj.transform.Find("InGameMenuButton");
        GameObject btnObj;
        if (existing != null)
            btnObj = existing.gameObject;
        else
        {
            btnObj = new GameObject("InGameMenuButton");
            btnObj.transform.SetParent(canvasObj.transform, false);
        }

        RectTransform btnRect = btnObj.GetComponent<RectTransform>();
        if (btnRect == null) btnRect = btnObj.AddComponent<RectTransform>();
        btnRect.anchorMin = new Vector2(0, 1);
        btnRect.anchorMax = new Vector2(0, 1);
        btnRect.pivot = new Vector2(0, 1);
        btnRect.anchoredPosition = new Vector2(20, -160);
        btnRect.sizeDelta = new Vector2(140, 60);

        Image btnImg = btnObj.GetComponent<Image>();
        if (btnImg == null) btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.4f, 0.4f, 0.4f, 0.7f);

        Button btn = btnObj.GetComponent<Button>();
        if (btn == null) btn = btnObj.AddComponent<Button>();

        InGameMenuButton igmb = btnObj.GetComponent<InGameMenuButton>();
        if (igmb == null) igmb = btnObj.AddComponent<InGameMenuButton>();

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
        labelTmp.text = "MENU";
        labelTmp.fontSize = 28;
        labelTmp.alignment = TextAlignmentOptions.Center;
        labelTmp.color = Color.white;
        labelTmp.fontStyle = FontStyles.Bold;

        btnObj.transform.SetSiblingIndex(1);

        EditorUtility.SetDirty(btnObj);
    }
}
