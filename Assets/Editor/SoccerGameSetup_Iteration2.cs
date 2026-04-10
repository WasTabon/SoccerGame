using UnityEngine;
using UnityEditor;

public class SoccerGameSetup_Iteration2 : EditorWindow
{
    [MenuItem("SoccerGame/Setup Flippers (Iteration 2)")]
    public static void SetupFlippers()
    {
        CreateFlipperPivots();
        CreateFlipperInput();
        Debug.Log("Iteration 2 setup complete! Flippers created.");
    }

    private static void CreateFlipperPivots()
    {
        float flipperY = -8.5f;
        float pivotOffsetX = 2.2f;
        float flipperLength = 2f;
        float flipperHeight = 0.35f;
        float flipperAngleMin = -30f;
        float flipperAngleMax = 30f;

        CreateFlipper("FlipperLeft", new Vector3(-pivotOffsetX, flipperY, 0), flipperLength, flipperHeight, flipperAngleMin, flipperAngleMax, false);
        CreateFlipper("FlipperRight", new Vector3(pivotOffsetX, flipperY, 0), flipperLength, flipperHeight, -flipperAngleMax, -flipperAngleMin, true);
    }

    private static void CreateFlipper(string name, Vector3 pivotPos, float length, float height, float minAngle, float maxAngle, bool isRight)
    {
        GameObject flipper = GameObject.Find(name);
        if (flipper == null)
        {
            flipper = new GameObject(name);
        }
        flipper.transform.position = pivotPos;
        flipper.transform.rotation = Quaternion.Euler(0, 0, isRight ? -minAngle : -maxAngle);

        SpriteRenderer sr = flipper.GetComponent<SpriteRenderer>();
        if (sr == null) sr = flipper.AddComponent<SpriteRenderer>();
        sr.sprite = GetSquareSprite();
        sr.color = new Color(0.8f, 0.8f, 0.2f);
        sr.sortingOrder = 5;

        float offsetX = isRight ? -length / 2f : length / 2f;
        flipper.transform.localScale = new Vector3(length, height, 1f);

        BoxCollider2D col = flipper.GetComponent<BoxCollider2D>();
        if (col == null) col = flipper.AddComponent<BoxCollider2D>();
        col.size = Vector2.one;

        Rigidbody2D rb = flipper.GetComponent<Rigidbody2D>();
        if (rb == null) rb = flipper.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        rb.mass = 100f;

        HingeJoint2D hinge = flipper.GetComponent<HingeJoint2D>();
        if (hinge == null) hinge = flipper.AddComponent<HingeJoint2D>();

        hinge.anchor = isRight ? new Vector2(0.5f, 0f) : new Vector2(-0.5f, 0f);
        hinge.connectedAnchor = pivotPos;
        hinge.useLimits = true;

        JointAngleLimits2D limits = new JointAngleLimits2D();
        limits.min = minAngle;
        limits.max = maxAngle;
        hinge.limits = limits;

        hinge.useMotor = true;
        JointMotor2D motor = new JointMotor2D();
        motor.motorSpeed = -1000f;
        motor.maxMotorTorque = 10000f;
        hinge.motor = motor;

        Flipper flipperScript = flipper.GetComponent<Flipper>();
        if (flipperScript == null) flipperScript = flipper.AddComponent<Flipper>();
        flipperScript.motorSpeed = isRight ? -2000f : 2000f;
        flipperScript.restMotorSpeed = isRight ? 1000f : -1000f;

        EditorUtility.SetDirty(flipper);
    }

    private static void CreateFlipperInput()
    {
        GameObject inputObj = GameObject.Find("FlipperInput");
        if (inputObj == null)
        {
            inputObj = new GameObject("FlipperInput");
        }

        FlipperInput input = inputObj.GetComponent<FlipperInput>();
        if (input == null) input = inputObj.AddComponent<FlipperInput>();

        GameObject leftObj = GameObject.Find("FlipperLeft");
        GameObject rightObj = GameObject.Find("FlipperRight");

        Debug.Assert(leftObj != null, "FlipperLeft not found!");
        Debug.Assert(rightObj != null, "FlipperRight not found!");

        input.leftFlipper = leftObj.GetComponent<Flipper>();
        input.rightFlipper = rightObj.GetComponent<Flipper>();

        EditorUtility.SetDirty(inputObj);
    }

    private static Sprite GetSquareSprite()
    {
        return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/SoccerGame/SquareSprite.asset");
    }
}
