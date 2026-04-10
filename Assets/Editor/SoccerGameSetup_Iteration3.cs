using UnityEngine;
using UnityEditor;

public class SoccerGameSetup_Iteration3 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Goals (Iteration 3)")]
    public static void Setup()
    {
        RemoveOldWalls();
        CreateGoalTop();
        CreateGoalBottom();
        CreateMatchManager();
        Debug.Log("Iteration 3 setup complete! Goals and MatchManager created.");
    }

    private static void RemoveOldWalls()
    {
        Transform walls = GameObject.Find("Walls")?.transform;
        if (walls == null) return;

        Transform wallTop = walls.Find("WallTop");
        if (wallTop != null) Object.DestroyImmediate(wallTop.gameObject);

        Transform wallBottom = walls.Find("WallBottom");
        if (wallBottom != null) Object.DestroyImmediate(wallBottom.gameObject);
    }

    private static void CreateGoalTop()
    {
        float fieldWidth = 6f;
        float goalWidth = 3f;
        float goalY = 10f;
        float postHeight = 0.4f;
        float postWidth = 0.3f;
        float wallThickness = 1f;

        Transform parent = FindOrCreateParent("GoalTop");
        parent.position = Vector3.zero;

        CreateGoalTrigger(parent, "GoalTriggerTop", new Vector3(0, goalY + 0.5f, 0), new Vector2(goalWidth, 1.5f), false);

        CreatePost(parent, "PostTopLeft", new Vector3(-goalWidth / 2f - postWidth / 2f, goalY, 0), new Vector2(postWidth, postHeight));
        CreatePost(parent, "PostTopRight", new Vector3(goalWidth / 2f + postWidth / 2f, goalY, 0), new Vector2(postWidth, postHeight));

        float sideWallWidth = (fieldWidth - goalWidth) / 2f;
        CreatePost(parent, "WallTopLeft", new Vector3(-fieldWidth / 2f + sideWallWidth / 2f - 0.5f, goalY, 0), new Vector2(sideWallWidth + wallThickness, postHeight));
        CreatePost(parent, "WallTopRight", new Vector3(fieldWidth / 2f - sideWallWidth / 2f + 0.5f, goalY, 0), new Vector2(sideWallWidth + wallThickness, postHeight));

        CreatePost(parent, "GoalBack", new Vector3(0, goalY + 1.5f, 0), new Vector2(goalWidth + postWidth * 2f, postHeight));
    }

    private static void CreateGoalBottom()
    {
        float goalY = -10f;
        float goalWidth = 4f;

        Transform parent = FindOrCreateParent("GoalBottom");
        parent.position = Vector3.zero;

        CreateGoalTrigger(parent, "GoalTriggerBottom", new Vector3(0, goalY - 0.5f, 0), new Vector2(goalWidth + 2f, 1.5f), true);
    }

    private static void CreateGoalTrigger(Transform parent, string name, Vector3 pos, Vector2 size, bool isPlayerGoal)
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
            obj.transform.SetParent(parent);
        }
        obj.transform.position = pos;

        BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
        if (col == null) col = obj.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = size;

        GoalZone zone = obj.GetComponent<GoalZone>();
        if (zone == null) zone = obj.AddComponent<GoalZone>();
        zone.isPlayerGoal = isPlayerGoal;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = GetSquareSprite();
        sr.color = isPlayerGoal ? new Color(0.8f, 0.2f, 0.2f, 0.3f) : new Color(0.2f, 0.8f, 0.2f, 0.3f);
        obj.transform.localScale = new Vector3(size.x, size.y, 1f);
        col.size = Vector2.one;

        EditorUtility.SetDirty(obj);
    }

    private static void CreatePost(Transform parent, string name, Vector3 pos, Vector2 size)
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
            obj.transform.SetParent(parent);
        }
        obj.transform.position = pos;

        BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
        if (col == null) col = obj.AddComponent<BoxCollider2D>();
        col.size = Vector2.one;

        PhysicsMaterial2D mat = AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>("Assets/SoccerGame/BallBounce.physicsMaterial2D");
        if (mat != null) col.sharedMaterial = mat;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = GetSquareSprite();
        sr.color = Color.white;
        sr.sortingOrder = 3;
        obj.transform.localScale = new Vector3(size.x, size.y, 1f);

        EditorUtility.SetDirty(obj);
    }

    private static void CreateMatchManager()
    {
        GameObject obj = GameObject.Find("MatchManager");
        if (obj == null)
        {
            obj = new GameObject("MatchManager");
        }

        MatchManager mm = obj.GetComponent<MatchManager>();
        if (mm == null) mm = obj.AddComponent<MatchManager>();

        GameObject ballObj = GameObject.Find("Ball");
        Debug.Assert(ballObj != null, "Ball not found on scene!");
        mm.ball = ballObj.GetComponent<Ball>();

        EditorUtility.SetDirty(obj);
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
