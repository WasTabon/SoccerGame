using UnityEngine;
using UnityEditor;

public class SoccerGameSetup_Iteration6 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Obstacles (Iteration 6)")]
    public static void Setup()
    {
        Transform parent = FindOrCreateParent("Obstacles");
        PhysicsMaterial2D mat = AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>("Assets/SoccerGame/BallBounce.physicsMaterial2D");

        CreateStaticObstacle(parent, "StaticBlock_Left", new Vector3(-1.8f, 2f, 0), new Vector2(0.8f, 0.8f), mat);
        CreateStaticObstacle(parent, "StaticBlock_Right", new Vector3(1.8f, 2f, 0), new Vector2(0.8f, 0.8f), mat);
        CreateStaticObstacle(parent, "StaticBlock_Center", new Vector3(0f, 5f, 0), new Vector2(1f, 0.5f), mat);

        CreateMovingObstacle(parent, "MovingBlock_1",
            new Vector2(-1.5f, -2f), new Vector2(1.5f, -2f), 1.5f, new Vector2(1.2f, 0.4f), mat);
        CreateMovingObstacle(parent, "MovingBlock_2",
            new Vector2(-1.8f, 6.5f), new Vector2(1.8f, 6.5f), 2f, new Vector2(0.8f, 0.4f), mat);

        CreateRotatingObstacle(parent, "RotatingBlock", new Vector3(0f, 0f, 0), new Vector2(2f, 0.3f), 60f, mat);

        Debug.Log("Iteration 6 setup complete! Obstacles created.");
    }

    private static void CreateStaticObstacle(Transform parent, string name, Vector3 pos, Vector2 size, PhysicsMaterial2D mat)
    {
        GameObject obj = FindOrCreateChild(parent, name);
        obj.transform.position = pos;
        obj.transform.localScale = new Vector3(size.x, size.y, 1f);

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = GetSquareSprite();
        sr.color = new Color(0.5f, 0.4f, 0.6f);
        sr.sortingOrder = 2;

        BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
        if (col == null) col = obj.AddComponent<BoxCollider2D>();
        col.size = Vector2.one;
        if (mat != null) col.sharedMaterial = mat;

        EditorUtility.SetDirty(obj);
    }

    private static void CreateMovingObstacle(Transform parent, string name, Vector2 pointA, Vector2 pointB, float speed, Vector2 size, PhysicsMaterial2D mat)
    {
        GameObject obj = FindOrCreateChild(parent, name);
        obj.transform.position = new Vector3(pointA.x, pointA.y, 0);
        obj.transform.localScale = new Vector3(size.x, size.y, 1f);

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = GetSquareSprite();
        sr.color = new Color(0.9f, 0.6f, 0.2f);
        sr.sortingOrder = 2;

        BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
        if (col == null) col = obj.AddComponent<BoxCollider2D>();
        col.size = Vector2.one;
        if (mat != null) col.sharedMaterial = mat;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null) rb = obj.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        MovingObstacle mo = obj.GetComponent<MovingObstacle>();
        if (mo == null) mo = obj.AddComponent<MovingObstacle>();
        mo.pointA = pointA;
        mo.pointB = pointB;
        mo.speed = speed;

        EditorUtility.SetDirty(obj);
    }

    private static void CreateRotatingObstacle(Transform parent, string name, Vector3 pos, Vector2 size, float rotSpeed, PhysicsMaterial2D mat)
    {
        GameObject obj = FindOrCreateChild(parent, name);
        obj.transform.position = pos;
        obj.transform.localScale = new Vector3(size.x, size.y, 1f);

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = GetSquareSprite();
        sr.color = new Color(0.2f, 0.7f, 0.8f);
        sr.sortingOrder = 2;

        BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
        if (col == null) col = obj.AddComponent<BoxCollider2D>();
        col.size = Vector2.one;
        if (mat != null) col.sharedMaterial = mat;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null) rb = obj.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        RotatingObstacle ro = obj.GetComponent<RotatingObstacle>();
        if (ro == null) ro = obj.AddComponent<RotatingObstacle>();
        ro.rotationSpeed = rotSpeed;

        EditorUtility.SetDirty(obj);
    }

    private static GameObject FindOrCreateChild(Transform parent, string name)
    {
        Transform existing = parent.Find(name);
        if (existing != null) return existing.gameObject;
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent);
        return obj;
    }

    private static Transform FindOrCreateParent(string name)
    {
        GameObject obj = GameObject.Find(name);
        if (obj == null) obj = new GameObject(name);
        return obj.transform;
    }

    private static Sprite GetSquareSprite()
    {
        return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/SoccerGame/SquareSprite.asset");
    }
}
