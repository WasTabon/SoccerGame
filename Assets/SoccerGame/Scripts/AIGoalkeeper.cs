using UnityEngine;

public class AIGoalkeeper : MonoBehaviour
{
    public Transform ball;
    public float speed = 4f;
    public float reactionDelay = 0.3f;
    public float minX = -1.2f;
    public float maxX = 1.2f;
    public float deflectForce = 8f;

    private float targetX;
    private float reactionTimer;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Assert(rb != null, "AIGoalkeeper: Rigidbody2D not found!");
    }

    private void Update()
    {
        if (ball == null || !ball.gameObject.activeSelf) return;

        reactionTimer -= Time.deltaTime;
        if (reactionTimer <= 0f)
        {
            targetX = Mathf.Clamp(ball.position.x, minX, maxX);
            reactionTimer = reactionDelay;
        }
    }

    private void FixedUpdate()
    {
        float newX = Mathf.MoveTowards(rb.position.x, targetX, speed * Time.fixedDeltaTime);
        rb.MovePosition(new Vector2(newX, rb.position.y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ballComp = collision.gameObject.GetComponent<Ball>();
        if (ballComp == null) return;

        Rigidbody2D ballRb = collision.rigidbody;
        Vector2 dir = (collision.transform.position - transform.position).normalized;
        dir.y = -Mathf.Abs(dir.y);
        ballRb.AddForce(dir * deflectForce, ForceMode2D.Impulse);
    }
}
