using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration7 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Difficulty and Endless (Iteration 7)")]
    public static void Setup()
    {
        CreateDifficultyManager();
        CreateEndlessUI();
        Debug.Log("Iteration 7 setup complete! DifficultyManager and Endless UI created.");
    }

    private static void CreateDifficultyManager()
    {
        GameObject obj = GameObject.Find("DifficultyManager");
        if (obj == null)
            obj = new GameObject("DifficultyManager");

        DifficultyManager dm = obj.GetComponent<DifficultyManager>();
        if (dm == null) dm = obj.AddComponent<DifficultyManager>();

        GameObject ballObj = GameObject.Find("Ball");
        Debug.Assert(ballObj != null, "Ball not found!");
        dm.ball = ballObj.GetComponent<Ball>();

        GameObject keeperObj = GameObject.Find("AIGoalkeeper");
        if (keeperObj != null)
            dm.goalkeeper = keeperObj.GetComponent<AIGoalkeeper>();

        EditorUtility.SetDirty(obj);
    }

    private static void CreateEndlessUI()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        Debug.Assert(canvasObj != null, "GameCanvas not found! Run Iteration 5 setup first.");
        Canvas canvas = canvasObj.GetComponent<Canvas>();

        Transform existing = canvas.transform.Find("EndlessPanel");
        GameObject panelObj;
        if (existing != null)
        {
            panelObj = existing.gameObject;
        }
        else
        {
            panelObj = new GameObject("EndlessPanel");
            panelObj.transform.SetParent(canvas.transform, false);
        }

        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        if (panelRect == null) panelRect = panelObj.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0, 1);
        panelRect.anchorMax = new Vector2(1, 1);
        panelRect.pivot = new Vector2(0.5f, 1);
        panelRect.anchoredPosition = new Vector2(0, -20);
        panelRect.sizeDelta = new Vector2(0, 200);

        Image bg = panelObj.GetComponent<Image>();
        if (bg == null) bg = panelObj.AddComponent<Image>();
        bg.color = new Color(0, 0, 0, 0.5f);

        TextMeshProUGUI scoreText = FindOrCreateTMP(panelObj.transform, "EndlessScoreText",
            new Vector2(0, 0.5f), new Vector2(1, 1), "0", 80, FontStyles.Bold);

        TextMeshProUGUI streakText = FindOrCreateTMP(panelObj.transform, "StreakText",
            new Vector2(0, 0.2f), new Vector2(1, 0.5f), "", 40, FontStyles.Italic);
        streakText.color = new Color(1f, 0.8f, 0.2f);

        TextMeshProUGUI bestText = FindOrCreateTMP(panelObj.transform, "BestScoreText",
            new Vector2(0, 0f), new Vector2(1, 0.2f), "BEST: 0", 28, FontStyles.Normal);
        bestText.color = new Color(0.7f, 0.7f, 0.7f);

        EndlessUI endlessUI = panelObj.GetComponent<EndlessUI>();
        if (endlessUI == null) endlessUI = panelObj.AddComponent<EndlessUI>();
        endlessUI.panel = panelObj;
        endlessUI.scoreText = scoreText;
        endlessUI.streakText = streakText;
        endlessUI.bestScoreText = bestText;

        panelObj.SetActive(false);

        EditorUtility.SetDirty(panelObj);
    }

    private static TextMeshProUGUI FindOrCreateTMP(Transform parent, string name,
        Vector2 anchorMin, Vector2 anchorMax, string defaultText, float fontSize, FontStyles style)
    {
        Transform existing = parent.Find(name);
        GameObject obj;
        if (existing != null)
        {
            obj = existing.gameObject;
        }
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
}
