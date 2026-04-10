using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector2 pointA;
    public Vector2 pointB;
    public float speed = 2f;

    private float t;
    private int direction = 1;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Assert(rb != null, "MovingObstacle: Rigidbody2D not found!");
    }

    private void FixedUpdate()
    {
        t += direction * speed * Time.fixedDeltaTime;
        if (t >= 1f)
        {
            t = 1f;
            direction = -1;
        }
        else if (t <= 0f)
        {
            t = 0f;
            direction = 1;
        }

        Vector2 pos = Vector2.Lerp(pointA, pointB, t);
        rb.MovePosition(pos);
    }
}
