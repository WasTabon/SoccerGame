using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SoccerGameSetup_Iteration17_Menu : EditorWindow
{
    [MenuItem("SoccerGame/Setup Shop - Menu (Iteration 17)")]
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

        CreateManagers();
        CreateSkinItemPrefab();
        AddShopButton();
        CreateShopPanel();
        LinkReferences();
        EditorSceneManager.SaveOpenScenes();
        Debug.Log("Iteration 17 Menu setup complete!");
    }

    private static Transform GetUIParent()
    {
        GameObject canvasObj = GameObject.Find("MenuCanvas");
        Debug.Assert(canvasObj != null, "MenuCanvas not found!");
        Transform safeArea = canvasObj.transform.Find("SafeAreaPanel");
        return safeArea != null ? safeArea : canvasObj.transform;
    }

    private static void CreateManagers()
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

    private static void CreateSkinItemPrefab()
    {
        string prefabPath = "Assets/SoccerGame/Prefabs/SkinItem.prefab";

        if (!AssetDatabase.IsValidFolder("Assets/SoccerGame/Prefabs"))
            AssetDatabase.CreateFolder("Assets/SoccerGame", "Prefabs");

        GameObject itemObj = new GameObject("SkinItem");

        RectTransform itemRect = itemObj.AddComponent<RectTransform>();
        itemRect.sizeDelta = new Vector2(0, 120);

        Image itemBg = itemObj.AddComponent<Image>();
        itemBg.color = new Color(0.15f, 0.2f, 0.15f);

        GameObject previewObj = new GameObject("Preview");
        previewObj.transform.SetParent(itemObj.transform, false);
        RectTransform prevRect = previewObj.AddComponent<RectTransform>();
        prevRect.anchorMin = new Vector2(0.02f, 0.15f);
        prevRect.anchorMax = new Vector2(0.15f, 0.85f);
        prevRect.offsetMin = Vector2.zero;
        prevRect.offsetMax = Vector2.zero;
        Image prevImg = previewObj.AddComponent<Image>();
        prevImg.color = Color.white;

        Sprite circleSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/SoccerGame/CircleSprite.asset");
        if (circleSprite != null) prevImg.sprite = circleSprite;

        TextMeshProUGUI nameTmp = CreateTMP(itemObj.transform, "NameText",
            new Vector2(0.18f, 0.5f), new Vector2(0.55f, 0.9f), "Skin Name", 32, FontStyles.Bold);

        TextMeshProUGUI priceTmp = CreateTMP(itemObj.transform, "PriceText",
            new Vector2(0.18f, 0.1f), new Vector2(0.55f, 0.5f), "10", 26, FontStyles.Normal);
        priceTmp.color = new Color(1f, 0.85f, 0f);

        GameObject btnObj = new GameObject("ActionButton");
        btnObj.transform.SetParent(itemObj.transform, false);
        RectTransform btnRect = btnObj.AddComponent<RectTransform>();
        btnRect.anchorMin = new Vector2(0.6f, 0.2f);
        btnRect.anchorMax = new Vector2(0.95f, 0.8f);
        btnRect.offsetMin = Vector2.zero;
        btnRect.offsetMax = Vector2.zero;
        Image btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.2f, 0.5f, 0.7f);
        Button btn = btnObj.AddComponent<Button>();

        TextMeshProUGUI btnLabel = CreateTMP(btnObj.transform, "Label",
            Vector2.zero, Vector2.one, "BUY", 28, FontStyles.Bold);

        SkinShopItem shopItem = itemObj.AddComponent<SkinShopItem>();
        shopItem.previewImage = prevImg;
        shopItem.nameText = nameTmp;
        shopItem.priceText = priceTmp;
        shopItem.actionButton = btn;
        shopItem.actionButtonImage = btnImg;
        shopItem.actionButtonLabel = btnLabel;

        PrefabUtility.SaveAsPrefabAsset(itemObj, prefabPath);
        Object.DestroyImmediate(itemObj);
        Debug.Log("SkinItem prefab saved to " + prefabPath);
    }

    private static void AddShopButton()
    {
        Transform uiParent = GetUIParent();

        Transform matchBtn = uiParent.Find("MatchButton");
        Transform endlessBtn = uiParent.Find("EndlessButton");
        Transform levelsBtn = uiParent.Find("LevelsButton");

        if (matchBtn != null)
        {
            RectTransform r = matchBtn.GetComponent<RectTransform>();
            r.anchorMin = new Vector2(0.15f, 0.55f);
            r.anchorMax = new Vector2(0.85f, 0.63f);
            r.offsetMin = Vector2.zero;
            r.offsetMax = Vector2.zero;
        }
        if (endlessBtn != null)
        {
            RectTransform r = endlessBtn.GetComponent<RectTransform>();
            r.anchorMin = new Vector2(0.15f, 0.44f);
            r.anchorMax = new Vector2(0.85f, 0.52f);
            r.offsetMin = Vector2.zero;
            r.offsetMax = Vector2.zero;
        }
        if (levelsBtn != null)
        {
            RectTransform r = levelsBtn.GetComponent<RectTransform>();
            r.anchorMin = new Vector2(0.15f, 0.33f);
            r.anchorMax = new Vector2(0.85f, 0.41f);
            r.offsetMin = Vector2.zero;
            r.offsetMax = Vector2.zero;
        }

        Transform existing = uiParent.Find("ShopButton");
        GameObject shopObj;
        if (existing != null)
            shopObj = existing.gameObject;
        else
        {
            shopObj = new GameObject("ShopButton");
            shopObj.transform.SetParent(uiParent, false);
        }

        RectTransform shopRect = shopObj.GetComponent<RectTransform>();
        if (shopRect == null) shopRect = shopObj.AddComponent<RectTransform>();
        shopRect.anchorMin = new Vector2(0.15f, 0.22f);
        shopRect.anchorMax = new Vector2(0.85f, 0.30f);
        shopRect.offsetMin = Vector2.zero;
        shopRect.offsetMax = Vector2.zero;

        Image shopImg = shopObj.GetComponent<Image>();
        if (shopImg == null) shopImg = shopObj.AddComponent<Image>();
        shopImg.color = new Color(0.6f, 0.3f, 0.6f);

        Button shopBtn = shopObj.GetComponent<Button>();
        if (shopBtn == null) shopBtn = shopObj.AddComponent<Button>();

        Transform labelT = shopObj.transform.Find("Label");
        GameObject labelObj;
        if (labelT != null) labelObj = labelT.gameObject;
        else
        {
            labelObj = new GameObject("Label");
            labelObj.transform.SetParent(shopObj.transform, false);
        }

        RectTransform labelRect = labelObj.GetComponent<RectTransform>();
        if (labelRect == null) labelRect = labelObj.AddComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;

        TextMeshProUGUI labelTmp = labelObj.GetComponent<TextMeshProUGUI>();
        if (labelTmp == null) labelTmp = labelObj.AddComponent<TextMeshProUGUI>();
        labelTmp.text = "SHOP";
        labelTmp.fontSize = 52;
        labelTmp.alignment = TextAlignmentOptions.Center;
        labelTmp.color = Color.white;
        labelTmp.fontStyle = FontStyles.Bold;

        EditorUtility.SetDirty(shopObj);
    }

    private static void CreateShopPanel()
    {
        Transform uiParent = GetUIParent();

        Transform existing = uiParent.Find("ShopPanel");
        GameObject panelObj;
        if (existing != null)
            panelObj = existing.gameObject;
        else
        {
            panelObj = new GameObject("ShopPanel");
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
        panelBg.color = new Color(0.08f, 0.12f, 0.08f);

        CreateTMP(panelObj.transform, "Header", new Vector2(0, 0.92f), new Vector2(0.7f, 1f), "SHOP", 52, FontStyles.Bold);

        TextMeshProUGUI coinText = CreateTMP(panelObj.transform, "CoinDisplay",
            new Vector2(0.7f, 0.92f), new Vector2(1f, 1f), "0", 40, FontStyles.Bold);
        coinText.color = new Color(1f, 0.85f, 0f);

        Transform scrollT = panelObj.transform.Find("ScrollArea");
        GameObject scrollObj;
        if (scrollT != null)
            scrollObj = scrollT.gameObject;
        else
        {
            scrollObj = new GameObject("ScrollArea");
            scrollObj.transform.SetParent(panelObj.transform, false);
        }

        RectTransform scrollRect = scrollObj.GetComponent<RectTransform>();
        if (scrollRect == null) scrollRect = scrollObj.AddComponent<RectTransform>();
        scrollRect.anchorMin = new Vector2(0.03f, 0.1f);
        scrollRect.anchorMax = new Vector2(0.97f, 0.9f);
        scrollRect.offsetMin = Vector2.zero;
        scrollRect.offsetMax = Vector2.zero;

        Image scrollBg = scrollObj.GetComponent<Image>();
        if (scrollBg == null) scrollBg = scrollObj.AddComponent<Image>();
        scrollBg.color = new Color(0, 0, 0, 0.01f);

        ScrollRect scroll = scrollObj.GetComponent<ScrollRect>();
        if (scroll == null) scroll = scrollObj.AddComponent<ScrollRect>();
        scroll.horizontal = false;
        scroll.vertical = true;

        Mask mask = scrollObj.GetComponent<Mask>();
        if (mask == null) mask = scrollObj.AddComponent<Mask>();
        mask.showMaskGraphic = false;

        Transform contentT = scrollObj.transform.Find("Content");
        GameObject contentObj;
        if (contentT != null)
            contentObj = contentT.gameObject;
        else
        {
            contentObj = new GameObject("Content");
            contentObj.transform.SetParent(scrollObj.transform, false);
        }

        RectTransform contentRect = contentObj.GetComponent<RectTransform>();
        if (contentRect == null) contentRect = contentObj.AddComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0, 1);
        contentRect.anchorMax = new Vector2(1, 1);
        contentRect.pivot = new Vector2(0.5f, 1);
        contentRect.anchoredPosition = Vector2.zero;

        VerticalLayoutGroup vlg = contentObj.GetComponent<VerticalLayoutGroup>();
        if (vlg == null) vlg = contentObj.AddComponent<VerticalLayoutGroup>();
        vlg.spacing = 10;
        vlg.padding = new RectOffset(10, 10, 10, 10);
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;
        vlg.childControlHeight = false;
        vlg.childControlWidth = true;

        ContentSizeFitter fitter = contentObj.GetComponent<ContentSizeFitter>();
        if (fitter == null) fitter = contentObj.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        scroll.content = contentRect;
        scroll.viewport = scrollRect;

        GameObject backBtn = CreateButton(panelObj.transform, "BackButton",
            new Vector2(0.25f, 0.02f), new Vector2(0.75f, 0.08f), "BACK", new Color(0.5f, 0.5f, 0.5f));

        Transform transitionFade = uiParent.Find("TransitionFade");
        if (transitionFade != null)
        {
            panelObj.transform.SetSiblingIndex(transitionFade.GetSiblingIndex());
            transitionFade.SetAsLastSibling();
        }

        panelObj.SetActive(false);
        EditorUtility.SetDirty(panelObj);
    }

    private static void LinkReferences()
    {
        Transform uiParent = GetUIParent();

        Transform menuRoot = uiParent.Find("MenuRoot");
        Debug.Assert(menuRoot != null, "MenuRoot not found!");
        MainMenuUI menuUI = menuRoot.GetComponent<MainMenuUI>();
        Debug.Assert(menuUI != null, "MainMenuUI not found!");

        Transform shopBtn = uiParent.Find("ShopButton");
        if (shopBtn != null)
            menuUI.shopButton = shopBtn.GetComponent<Button>();

        Transform shopPanel = uiParent.Find("ShopPanel");
        if (shopPanel != null)
        {
            SkinShopUI shopUI = shopPanel.GetComponent<SkinShopUI>();
            if (shopUI == null) shopUI = shopPanel.gameObject.AddComponent<SkinShopUI>();

            shopUI.panel = shopPanel.gameObject;

            Transform scrollArea = shopPanel.Find("ScrollArea");
            if (scrollArea != null)
            {
                Transform content = scrollArea.Find("Content");
                if (content != null) shopUI.contentParent = content;
            }

            Transform backBtn = shopPanel.Find("BackButton");
            if (backBtn != null) shopUI.backButton = backBtn.GetComponent<Button>();

            Transform coinDisplay = shopPanel.Find("CoinDisplay");
            if (coinDisplay != null)
                shopUI.coinDisplayText = coinDisplay.GetComponent<TextMeshProUGUI>();

            string prefabPath = "Assets/SoccerGame/Prefabs/SkinItem.prefab";
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab != null) shopUI.skinItemPrefab = prefab;

            menuUI.skinShopUI = shopUI;
            EditorUtility.SetDirty(shopUI);
        }

        MainMenuAnimations anim = menuRoot.GetComponent<MainMenuAnimations>();
        if (anim != null)
            EditorUtility.SetDirty(anim);

        EditorUtility.SetDirty(menuUI);
    }

    private static TextMeshProUGUI CreateTMP(Transform parent, string name,
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

    private static GameObject CreateButton(Transform parent, string name,
        Vector2 anchorMin, Vector2 anchorMax, string label, Color color)
    {
        Transform existing = parent.Find(name);
        GameObject btnObj;
        if (existing != null) btnObj = existing.gameObject;
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
        if (labelT != null) labelObj = labelT.gameObject;
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

        TextMeshProUGUI tmp = labelObj.GetComponent<TextMeshProUGUI>();
        if (tmp == null) tmp = labelObj.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.fontSize = 40;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        tmp.fontStyle = FontStyles.Bold;

        return btnObj;
    }
}
