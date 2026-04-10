using UnityEngine;
using UnityEditor;

public class SoccerGameSetup_Iteration4 : EditorWindow
{
    [MenuItem("SoccerGame/Setup AI Goalkeeper (Iteration 4)")]
    public static void Setup()
    {
        CreateGoalkeeper();
        Debug.Log("Iteration 4 setup complete! AI Goalkeeper created.");
    }

    private static void CreateGoalkeeper()
    {
        float goalY = 9.5f;
        float keeperWidth = 1.2f;
        float keeperHeight = 0.35f;

        GameObject obj = GameObject.Find("AIGoalkeeper");
        if (obj == null)
        {
            obj = new GameObject("AIGoalkeeper");
        }
        obj.transform.position = new Vector3(0, goalY, 0);

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = GetSquareSprite();
        sr.color = new Color(0.9f, 0.3f, 0.3f);
        sr.sortingOrder = 5;
        obj.transform.localScale = new Vector3(keeperWidth, keeperHeight, 1f);

        BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
        if (col == null) col = obj.AddComponent<BoxCollider2D>();
        col.size = Vector2.one;

        PhysicsMaterial2D mat = AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>("Assets/SoccerGame/BallBounce.physicsMaterial2D");
        if (mat != null) col.sharedMaterial = mat;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null) rb = obj.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        AIGoalkeeper keeper = obj.GetComponent<AIGoalkeeper>();
        if (keeper == null) keeper = obj.AddComponent<AIGoalkeeper>();

        GameObject ballObj = GameObject.Find("Ball");
        Debug.Assert(ballObj != null, "Ball not found on scene!");
        keeper.ball = ballObj.transform;

        EditorUtility.SetDirty(obj);
    }

    private static Sprite GetSquareSprite()
    {
        return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/SoccerGame/SquareSprite.asset");
    }
}
