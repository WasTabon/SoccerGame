using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SoccerGameSetup_Iteration9 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Game Feel (Iteration 9)")]
    public static void Setup()
    {
        SetupScreenShake();
        SetupBallEffects();
        SetupGoalEffect();
        SetupMatchEndContentRef();
        Debug.Log("Iteration 9 setup complete! Game feel effects added.");
    }

    private static void SetupScreenShake()
    {
        Camera cam = Camera.main;
        Debug.Assert(cam != null, "Main Camera not found!");

        ScreenShake shake = cam.GetComponent<ScreenShake>();
        if (shake == null) shake = cam.gameObject.AddComponent<ScreenShake>();

        EditorUtility.SetDirty(cam.gameObject);
    }

    private static void SetupBallEffects()
    {
        GameObject ball = GameObject.Find("Ball");
        Debug.Assert(ball != null, "Ball not found!");

        BallEffects be = ball.GetComponent<BallEffects>();
        if (be == null) be = ball.AddComponent<BallEffects>();

        TrailRenderer trail = ball.GetComponent<TrailRenderer>();
        if (trail == null) trail = ball.AddComponent<TrailRenderer>();
        trail.time = 0.15f;
        trail.startWidth = 0.25f;
        trail.endWidth = 0.02f;
        trail.startColor = new Color(1f, 1f, 1f, 0.6f);
        trail.endColor = new Color(1f, 1f, 1f, 0f);
        trail.material = GetDefaultSpriteMaterial();
        trail.sortingOrder = 9;
        trail.minVertexDistance = 0.05f;

        EditorUtility.SetDirty(ball);
    }

    private static void SetupGoalEffect()
    {
        GameObject obj = GameObject.Find("GoalEffect");
        if (obj == null)
            obj = new GameObject("GoalEffect");

        GoalEffect ge = obj.GetComponent<GoalEffect>();
        if (ge == null) ge = obj.AddComponent<GoalEffect>();

        GameObject canvasObj = GameObject.Find("GameCanvas");
        Debug.Assert(canvasObj != null, "GameCanvas not found!");

        Transform flashT = canvasObj.transform.Find("FlashImage");
        GameObject flashObj;
        if (flashT != null)
        {
            flashObj = flashT.gameObject;
        }
        else
        {
            flashObj = new GameObject("FlashImage");
            flashObj.transform.SetParent(canvasObj.transform, false);
        }

        RectTransform flashRect = flashObj.GetComponent<RectTransform>();
        if (flashRect == null) flashRect = flashObj.AddComponent<RectTransform>();
        flashRect.anchorMin = Vector2.zero;
        flashRect.anchorMax = Vector2.one;
        flashRect.offsetMin = Vector2.zero;
        flashRect.offsetMax = Vector2.zero;

        Image flashImg = flashObj.GetComponent<Image>();
        if (flashImg == null) flashImg = flashObj.AddComponent<Image>();
        flashImg.color = new Color(1, 1, 1, 0);
        flashImg.raycastTarget = false;

        flashObj.transform.SetAsLastSibling();

        ge.flashImage = flashImg;

        EditorUtility.SetDirty(obj);
        EditorUtility.SetDirty(flashObj);
    }

    private static void SetupMatchEndContentRef()
    {
        GameObject canvasObj = GameObject.Find("GameCanvas");
        if (canvasObj == null) return;

        Transform matchEndPanel = canvasObj.transform.Find("MatchEndPanel");
        if (matchEndPanel == null) return;

        MatchEndUI matchEndUI = matchEndPanel.GetComponent<MatchEndUI>();
        if (matchEndUI == null) return;

        Transform content = matchEndPanel.Find("Content");
        if (content != null)
        {
            matchEndUI.contentRect = content.GetComponent<RectTransform>();
            EditorUtility.SetDirty(matchEndUI);
        }
    }

    private static Material GetDefaultSpriteMaterial()
    {
        return new Material(Shader.Find("Sprites/Default"));
    }
}
