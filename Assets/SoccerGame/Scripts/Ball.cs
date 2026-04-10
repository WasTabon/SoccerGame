using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialSpeed = 8f;
    public float minSpeed = 5f;
    public float maxSpeed = 20f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Assert(rb != null, "Ball: Rigidbody2D not found!");
    }

    private void Start()
    {
        Launch();
    }

    public void Launch()
    {
        float angle = Random.Range(30f, 150f) * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        if (Random.value > 0.5f) dir.y *= -1f;
        rb.velocity = dir * initialSpeed;
    }

    private void FixedUpdate()
    {
        ClampSpeed();
    }

    private void ClampSpeed()
    {
        float speed = rb.velocity.magnitude;
        if (speed < minSpeed)
            rb.velocity = rb.velocity.normalized * minSpeed;
        else if (speed > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
