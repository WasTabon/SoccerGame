using UnityEngine;
using UnityEditor;

public class SoccerGameSetup_Iteration16 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Ball Effects (Iteration 16)")]
    public static void Setup()
    {
        UpgradeTrail();
        CreateGlow();
        Debug.Log("Iteration 16 setup complete! Ball effects upgraded.");
    }

    private static void UpgradeTrail()
    {
        GameObject ball = GameObject.Find("Ball");
        Debug.Assert(ball != null, "Ball not found!");

        TrailRenderer trail = ball.GetComponent<TrailRenderer>();
        if (trail == null) trail = ball.AddComponent<TrailRenderer>();

        trail.time = 0.25f;
        trail.startWidth = 0.3f;
        trail.endWidth = 0.02f;
        trail.startColor = new Color(1f, 0.95f, 0.6f, 0.8f);
        trail.endColor = new Color(1f, 0.8f, 0.3f, 0f);
        trail.minVertexDistance = 0.03f;
        trail.sortingOrder = 9;
        trail.numCornerVertices = 3;
        trail.numCapVertices = 3;

        Material mat = trail.material;
        if (mat == null || mat.shader.name != "Sprites/Default")
        {
            mat = new Material(Shader.Find("Sprites/Default"));
            trail.material = mat;
        }

        EditorUtility.SetDirty(ball);
    }

    private static void CreateGlow()
    {
        GameObject ball = GameObject.Find("Ball");
        Debug.Assert(ball != null, "Ball not found!");

        Transform existingGlow = ball.transform.Find("Glow");
        GameObject glowObj;
        if (existingGlow != null)
            glowObj = existingGlow.gameObject;
        else
        {
            glowObj = new GameObject("Glow");
            glowObj.transform.SetParent(ball.transform, false);
        }

        glowObj.transform.localPosition = Vector3.zero;
        glowObj.transform.localScale = Vector3.one * 3f;

        SpriteRenderer sr = glowObj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = glowObj.AddComponent<SpriteRenderer>();
        sr.sprite = GetCircleSprite();
        sr.color = new Color(0.8f, 0.9f, 1f, 0.4f);
        sr.sortingOrder = 8;

        BallGlow glow = glowObj.GetComponent<BallGlow>();
        if (glow == null) glow = glowObj.AddComponent<BallGlow>();

        EditorUtility.SetDirty(glowObj);
    }

    private static Sprite GetCircleSprite()
    {
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/SoccerGame/GlowSprite.asset");
        if (sprite != null) return sprite;

        int size = 64;
        Texture2D tex = new Texture2D(size, size);
        float center = size / 2f;
        float radius = size / 2f;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), new Vector2(center, center));
                float t = Mathf.Clamp01(dist / radius);
                float alpha = Mathf.Pow(1f - t, 2f);
                tex.SetPixel(x, y, new Color(1, 1, 1, alpha));
            }
        }
        tex.Apply();
        tex.filterMode = FilterMode.Bilinear;

        sprite = Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);

        if (!AssetDatabase.IsValidFolder("Assets/SoccerGame"))
            AssetDatabase.CreateFolder("Assets", "SoccerGame");

        AssetDatabase.CreateAsset(tex, "Assets/SoccerGame/GlowSprite_tex.asset");
        AssetDatabase.CreateAsset(sprite, "Assets/SoccerGame/GlowSprite.asset");
        AssetDatabase.SaveAssets();

        return sprite;
    }
}
