using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    public float rotationSpeed = 90f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Assert(rb != null, "RotatingObstacle: Rigidbody2D not found!");
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation + rotationSpeed * Time.fixedDeltaTime);
    }
}
