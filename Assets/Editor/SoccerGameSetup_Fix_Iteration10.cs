using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Fix_Iteration10 : EditorWindow
{
    [MenuItem("SoccerGame/Fix Match End and Endless Menu (Iteration 10 Fix)")]
    public static void Fix()
    {
        FixMatchEndUI();
        RemoveDuplicateEndlessMenuButton();
        Debug.Log("Iteration 10 Fix complete! MatchEndUI restructured, duplicate menu button removed.");
    }

    private static void FixMatchEndUI()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        Debug.Assert(canvasObj != null, "GameCanvas not found!");

        Transform matchEndPanel = canvasObj.transform.Find("MatchEndPanel");
        if (matchEndPanel == null)
        {
            Debug.LogWarning("MatchEndPanel not found!");
            return;
        }

        MatchEndUI oldUI = matchEndPanel.GetComponent<MatchEndUI>();

        GameObject handlerObj;
        Transform handlerT = canvasObj.transform.Find("MatchEndHandler");
        if (handlerT != null)
        {
            handlerObj = handlerT.gameObject;
        }
        else
        {
            handlerObj = new GameObject("MatchEndHandler");
            handlerObj.transform.SetParent(canvasObj.transform, false);

            RectTransform rect = handlerObj.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        int panelIndex = matchEndPanel.GetSiblingIndex();
        handlerObj.transform.SetSiblingIndex(panelIndex);
        matchEndPanel.SetParent(handlerObj.transform, false);

        RectTransform panelRect = matchEndPanel.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        if (oldUI != null)
            Object.DestroyImmediate(oldUI);

        MatchEndUI newUI = handlerObj.GetComponent<MatchEndUI>();
        if (newUI == null) newUI = handlerObj.AddComponent<MatchEndUI>();

        newUI.panel = matchEndPanel.gameObject;

        Transform content = matchEndPanel.Find("Content");
        if (content != null)
        {
            newUI.contentRect = content.GetComponent<RectTransform>();

            Transform resultT = content.Find("ResultText");
            if (resultT != null)
                newUI.resultText = resultT.GetComponent<TextMeshProUGUI>();

            Transform scoreT = content.Find("FinalScoreText");
            if (scoreT != null)
                newUI.finalScoreText = scoreT.GetComponent<TextMeshProUGUI>();

            Transform restartT = content.Find("RestartButton");
            if (restartT != null)
                newUI.restartButton = restartT.GetComponent<Button>();

            Transform menuT = content.Find("MenuButton");
            if (menuT != null)
                newUI.menuButton = menuT.GetComponent<Button>();
        }

        matchEndPanel.gameObject.SetActive(false);

        EditorUtility.SetDirty(handlerObj);
        EditorUtility.SetDirty(matchEndPanel.gameObject);
    }

    private static void RemoveDuplicateEndlessMenuButton()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        if (canvasObj == null) return;

        Transform endlessPanel = canvasObj.transform.Find("EndlessPanel");
        if (endlessPanel == null) return;

        Transform oldBtn = endlessPanel.Find("EndlessMenuButton");
        if (oldBtn != null)
        {
            Object.DestroyImmediate(oldBtn.gameObject);
            Debug.Log("Removed duplicate EndlessMenuButton from EndlessPanel.");
        }
    }
}
