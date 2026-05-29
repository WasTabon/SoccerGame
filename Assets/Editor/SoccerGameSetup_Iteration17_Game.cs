using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration17_Game : EditorWindow
{
    [MenuItem("SoccerGame/Setup Coins + Skins - Game (Iteration 17)")]
    public static void Setup()
    {
        CreateCoinUI();
        AddBallSkinApplier();
        EnsureManagers();
        Debug.Log("Iteration 17 Game setup complete!");
    }

    private static Transform GetUIParent()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        Debug.Assert(canvasObj != null, "GameCanvas not found!");
        Transform safeArea = canvasObj.transform.Find("SafeAreaPanel");
        return safeArea != null ? safeArea : canvasObj.transform;
    }

    private static void CreateCoinUI()
    {
        Transform uiParent = GetUIParent();

        Transform existing = uiParent.Find("CoinPanel");
        GameObject panelObj;
        if (existing != null)
            panelObj = existing.gameObject;
        else
        {
            panelObj = new GameObject("CoinPanel");
            panelObj.transform.SetParent(uiParent, false);
        }

        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        if (panelRect == null) panelRect = panelObj.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.65f, 1);
        panelRect.anchorMax = new Vector2(1, 1);
        panelRect.pivot = new Vector2(1, 1);
        panelRect.anchoredPosition = new Vector2(-10, -160);
        panelRect.sizeDelta = new Vector2(200, 50);

        Image bg = panelObj.GetComponent<Image>();
        if (bg == null) bg = panelObj.AddComponent<Image>();
        bg.color = new Color(0, 0, 0, 0.5f);

        Transform coinLabelT = panelObj.transform.Find("CoinLabel");
        GameObject coinLabelObj;
        if (coinLabelT != null)
            coinLabelObj = coinLabelT.gameObject;
        else
        {
            coinLabelObj = new GameObject("CoinLabel");
            coinLabelObj.transform.SetParent(panelObj.transform, false);
        }

        RectTransform labelRect = coinLabelObj.GetComponent<RectTransform>();
        if (labelRect == null) labelRect = coinLabelObj.AddComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = new Vector2(0.4f, 1);
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;

        TextMeshProUGUI labelTmp = coinLabelObj.GetComponent<TextMeshProUGUI>();
        if (labelTmp == null) labelTmp = coinLabelObj.AddComponent<TextMeshProUGUI>();
        labelTmp.text = "$";
        labelTmp.fontSize = 28;
        labelTmp.alignment = TextAlignmentOptions.Center;
        labelTmp.color = new Color(1f, 0.85f, 0f);
        labelTmp.fontStyle = FontStyles.Bold;

        Transform coinTextT = panelObj.transform.Find("CoinText");
        GameObject coinTextObj;
        if (coinTextT != null)
            coinTextObj = coinTextT.gameObject;
        else
        {
            coinTextObj = new GameObject("CoinText");
            coinTextObj.transform.SetParent(panelObj.transform, false);
        }

        RectTransform textRect = coinTextObj.GetComponent<RectTransform>();
        if (textRect == null) textRect = coinTextObj.AddComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.4f, 0);
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        TextMeshProUGUI coinTmp = coinTextObj.GetComponent<TextMeshProUGUI>();
        if (coinTmp == null) coinTmp = coinTextObj.AddComponent<TextMeshProUGUI>();
        coinTmp.text = "0";
        coinTmp.fontSize = 28;
        coinTmp.alignment = TextAlignmentOptions.Center;
        coinTmp.color = Color.white;
        coinTmp.fontStyle = FontStyles.Bold;

        CoinUI coinUI = panelObj.GetComponent<CoinUI>();
        if (coinUI == null) coinUI = panelObj.AddComponent<CoinUI>();
        coinUI.coinText = coinTmp;

        EditorUtility.SetDirty(panelObj);
    }

    private static void AddBallSkinApplier()
    {
        GameObject ball = GameObject.Find("Ball");
        Debug.Assert(ball != null, "Ball not found!");

        BallSkinApplier bsa = ball.GetComponent<BallSkinApplier>();
        if (bsa == null) bsa = ball.AddComponent<BallSkinApplier>();

        EditorUtility.SetDirty(ball);
    }

    private static void EnsureManagers()
    {
        if (GameObject.Find("CoinManager") == null)
        {
            GameObject obj = new GameObject("CoinManager");
            obj.AddComponent<CoinManager>();
            EditorUtility.SetDirty(obj);
        }

        if (GameObject.Find("SkinManager") == null)
        {
            GameObject obj = new GameObject("SkinManager");
            obj.AddComponent<SkinManager>();
            EditorUtility.SetDirty(obj);
        }
    }
}
