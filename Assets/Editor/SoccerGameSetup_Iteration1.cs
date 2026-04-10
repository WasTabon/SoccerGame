using UnityEngine;
using UnityEditor;

public class SoccerGameSetup_Iteration1 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Scene (Iteration 1)")]
    public static void ShowWindow()
    {
        SetupScene();
    }

    private static void SetupScene()
    {
        SetupCamera();
        CreatePhysicsMaterial();
        CreateWalls();
        CreateBall();
        Debug.Log("Iteration 1 setup complete!");
    }

    private static void SetupCamera()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            GameObject camObj = new GameObject("Main Camera");
            cam = camObj.AddComponent<Camera>();
            camObj.tag = "MainCamera";
        }
        cam.orthographic = true;
        cam.orthographicSize = 10f;
        cam.transform.position = new Vector3(0, 0, -10);
        cam.backgroundColor = new Color(0.15f, 0.22f, 0.15f);
    }

    private static void CreatePhysicsMaterial()
    {
        string path = "Assets/SoccerGame/BallBounce.physicsMaterial2D";
        if (AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>(path) != null) return;

        if (!AssetDatabase.IsValidFolder("Assets/SoccerGame"))
            AssetDatabase.CreateFolder("Assets", "SoccerGame");

        PhysicsMaterial2D mat = new PhysicsMaterial2D("BallBounce");
        mat.bounciness = 1f;
        mat.friction = 0f;
        AssetDatabase.CreateAsset(mat, path);
        AssetDatabase.SaveAssets();
    }

    private static PhysicsMaterial2D GetBounceMaterial()
    {
        return AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>("Assets/SoccerGame/BallBounce.physicsMaterial2D");
    }

    private static void CreateWalls()
    {
        Transform parent = FindOrCreateParent("Walls");

        float fieldWidth = 6f;
        float fieldHeight = 10f;
        float wallThickness = 1f;

        CreateWall(parent, "WallLeft", new Vector3(-fieldWidth / 2f - wallThickness / 2f, 0, 0), new Vector2(wallThickness, fieldHeight * 2f));
        CreateWall(parent, "WallRight", new Vector3(fieldWidth / 2f + wallThickness / 2f, 0, 0), new Vector2(wallThickness, fieldHeight * 2f));
        CreateWall(parent, "WallTop", new Vector3(0, fieldHeight + wallThickness / 2f, 0), new Vector2(fieldWidth + wallThickness * 2f, wallThickness));
        CreateWall(parent, "WallBottom", new Vector3(0, -fieldHeight - wallThickness / 2f, 0), new Vector2(fieldWidth + wallThickness * 2f, wallThickness));
    }

    private static void CreateWall(Transform parent, string name, Vector3 pos, Vector2 size)
    {
        Transform existing = parent.Find(name);
        GameObject wall;
        if (existing != null)
        {
            wall = existing.gameObject;
        }
        else
        {
            wall = new GameObject(name);
            wall.transform.SetParent(parent);
        }
        wall.transform.position = pos;
        wall.layer = LayerMask.NameToLayer("Default");

        BoxCollider2D col = wall.GetComponent<BoxCollider2D>();
        if (col == null) col = wall.AddComponent<BoxCollider2D>();
        col.size = size;

        PhysicsMaterial2D mat = GetBounceMaterial();
        if (mat != null) col.sharedMaterial = mat;

        SpriteRenderer sr = wall.GetComponent<SpriteRenderer>();
        if (sr == null) sr = wall.AddComponent<SpriteRenderer>();
        sr.sprite = CreateSquareSprite();
        sr.color = new Color(0.3f, 0.5f, 0.3f);
        wall.transform.localScale = new Vector3(size.x, size.y, 1f);
        col.size = Vector2.one;
    }

    private static void CreateBall()
    {
        GameObject ball = GameObject.Find("Ball");
        if (ball == null)
        {
            ball = new GameObject("Ball");
        }
        ball.transform.position = Vector3.zero;

        SpriteRenderer sr = ball.GetComponent<SpriteRenderer>();
        if (sr == null) sr = ball.AddComponent<SpriteRenderer>();
        sr.sprite = CreateCircleSprite();
        sr.color = Color.white;
        sr.sortingOrder = 10;
        ball.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        CircleCollider2D col = ball.GetComponent<CircleCollider2D>();
        if (col == null) col = ball.AddComponent<CircleCollider2D>();
        col.radius = 0.5f;

        PhysicsMaterial2D mat = GetBounceMaterial();
        if (mat != null) col.sharedMaterial = mat;

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb == null) rb = ball.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.drag = 0f;
        rb.angularDrag = 0f;

        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript == null) ball.AddComponent<Ball>();

        EditorUtility.SetDirty(ball);
    }

    private static Transform FindOrCreateParent(string name)
    {
        GameObject obj = GameObject.Find(name);
        if (obj == null) obj = new GameObject(name);
        return obj.transform;
    }

    private static Sprite CreateSquareSprite()
    {
        string path = "Assets/SoccerGame/SquareSprite.asset";
        Sprite existing = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        if (existing != null) return existing;

        Texture2D tex = new Texture2D(4, 4);
        Color[] colors = new Color[16];
        for (int i = 0; i < 16; i++) colors[i] = Color.white;
        tex.SetPixels(colors);
        tex.Apply();
        tex.filterMode = FilterMode.Point;

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, 4, 4), new Vector2(0.5f, 0.5f), 4f);
        AssetDatabase.CreateAsset(tex, path.Replace(".asset", "_tex.asset"));
        AssetDatabase.CreateAsset(sprite, path);
        AssetDatabase.SaveAssets();
        return sprite;
    }

    private static Sprite CreateCircleSprite()
    {
        string path = "Assets/SoccerGame/CircleSprite.asset";
        Sprite existing = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        if (existing != null) return existing;

        int size = 64;
        Texture2D tex = new Texture2D(size, size);
        float center = size / 2f;
        float radius = size / 2f;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), new Vector2(center, center));
                tex.SetPixel(x, y, dist <= radius ? Color.white : Color.clear);
            }
        }
        tex.Apply();
        tex.filterMode = FilterMode.Bilinear;

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        AssetDatabase.CreateAsset(tex, path.Replace(".asset", "_tex.asset"));
        AssetDatabase.CreateAsset(sprite, path);
        AssetDatabase.SaveAssets();
        return sprite;
    }
}
