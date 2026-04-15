using UnityEngine;
using DG.Tweening;

public class Flipper : MonoBehaviour
{
    public float motorSpeed = 2000f;
    public float restMotorSpeed = -1000f;
    public float hitForceMultiplier = 1.5f;

    private HingeJoint2D hinge;
    private bool isActivated;
    private SpriteRenderer sr;
    private Color baseColor;

    private void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
        Debug.Assert(hinge != null, "Flipper: HingeJoint2D not found!");
        sr = GetComponent<SpriteRenderer>();
        if (sr != null) baseColor = sr.color;
    }

    public void Activate()
    {
        isActivated = true;
        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = motorSpeed;
        hinge.motor = motor;
    }

    public void Deactivate()
    {
        isActivated = false;
        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = restMotorSpeed;
        hinge.motor = motor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActivated) return;

        Rigidbody2D ballRb = collision.rigidbody;
        if (ballRb == null) return;

        Ball ball = ballRb.GetComponent<Ball>();
        if (ball == null) return;

        Vector2 dir = (collision.transform.position - transform.position).normalized;
        dir.y = Mathf.Abs(dir.y);
        ballRb.velocity = Vector2.zero;
        ballRb.AddForce(dir * motorSpeed * hitForceMultiplier * 0.01f, ForceMode2D.Impulse);

        BallEffects be = ball.GetComponent<BallEffects>();
        if (be != null) be.PlayHitEffect(dir);

        if (ScreenShake.Instance != null)
            ScreenShake.Instance.ShakeLight();

        PlayFlipperHitEffect();
    }

    private void PlayFlipperHitEffect()
    {
        if (sr == null) return;
        sr.DOComplete();
        sr.DOColor(Color.white, 0.05f)
            .OnComplete(() => sr.DOColor(baseColor, 0.15f));
    }
}
