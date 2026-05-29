using UnityEngine;
using UnityEditor;

public class SoccerGameSetup_Iteration15 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Fast Flippers + Defense Keeper (Iteration 15)")]
    public static void Setup()
    {
        UpgradeFlippers();
        CreateDefenseKeeper();
        Debug.Log("Iteration 15 setup complete! Flippers upgraded, Defense Keeper created.");
    }

    private static void UpgradeFlippers()
    {
        UpgradeFlipper("FlipperLeft", false);
        UpgradeFlipper("FlipperRight", true);
    }

    private static void UpgradeFlipper(string name, bool isRight)
    {
        GameObject obj = GameObject.Find(name);
        if (obj == null)
        {
            Debug.LogWarning(name + " not found!");
            return;
        }

        HingeJoint2D hinge = obj.GetComponent<HingeJoint2D>();
        if (hinge != null)
        {
            JointMotor2D motor = hinge.motor;
            motor.maxMotorTorque = 15000f;
            hinge.motor = motor;
        }

        Flipper flipper = obj.GetComponent<Flipper>();
        if (flipper != null)
        {
            flipper.motorSpeed = isRight ? -3000f : 3000f;
            flipper.restMotorSpeed = isRight ? 1500f : -1500f;
        }

        EditorUtility.SetDirty(obj);
    }

    private static void CreateDefenseKeeper()
    {
        float keeperY = -9.2f;
        float keeperWidth = 0.8f;
        float keeperHeight = 0.25f;

        GameObject obj = GameObject.Find("DefenseKeeper");
        if (obj == null)
            obj = new GameObject("DefenseKeeper");

        obj.transform.position = new Vector3(0, keeperY, 0);

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = GetSquareSprite();
        sr.color = new Color(0.3f, 0.8f, 0.9f);
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

        DefenseKeeper keeper = obj.GetComponent<DefenseKeeper>();
        if (keeper == null) keeper = obj.AddComponent<DefenseKeeper>();

        GameObject ballObj = GameObject.Find("Ball");
        Debug.Assert(ballObj != null, "Ball not found!");
        keeper.ball = ballObj.transform;

        EditorUtility.SetDirty(obj);
    }

    private static Sprite GetSquareSprite()
    {
        return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/SoccerGame/SquareSprite.asset");
    }
}
