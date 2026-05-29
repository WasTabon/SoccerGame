using UnityEngine;
using DG.Tweening;

public class BallEffects : MonoBehaviour
{
    public GameObject hitParticlePrefab;
    public GameObject bounceParticlePrefab;
    public int hitParticleCount = 8;
    public int bounceParticleCount = 4;
    public float particleSpeed = 5f;
    public float particleLifetime = 0.4f;

    private Vector3 baseScale;
    private SpriteRenderer sr;

    private void Awake()
    {
        baseScale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();
    }

    public void PlayHitEffect(Vector2 hitDirection)
    {
        transform.DOComplete();
        transform.localScale = baseScale;

        Vector3 squashScale = new Vector3(baseScale.x * 1.4f, baseScale.y * 0.6f, 1f);
        transform.localScale = squashScale;
        transform.DOScale(baseScale, 0.2f).SetEase(Ease.OutElastic);

        if (sr != null)
        {
            sr.DOComplete();
            sr.DOColor(new Color(1f, 1f, 0.5f), 0.05f)
                .OnComplete(() => sr.DOColor(Color.white, 0.15f));
        }

        SpawnParticles(transform.position, hitDirection, hitParticleCount, new Color(1f, 0.9f, 0.3f));
    }

    public void PlayBounceEffect(Vector2 contactPoint, Vector2 normal)
    {
        SpawnParticles(contactPoint, normal, bounceParticleCount, new Color(0.8f, 0.8f, 1f, 0.8f));
    }

    public void PlayGoalEffect()
    {
        transform.DOComplete();
        transform.DOScale(baseScale * 1.5f, 0.15f).SetEase(Ease.OutQuad)
            .OnComplete(() => transform.localScale = baseScale);

        if (sr != null)
        {
            sr.DOComplete();
            sr.DOColor(Color.yellow, 0.1f)
                .OnComplete(() => sr.DOColor(Color.white, 0.1f));
        }

        SpawnParticles(transform.position, Vector2.up, 12, new Color(1f, 0.85f, 0f));
    }

    public void ResetEffects()
    {
        transform.DOComplete();
        transform.localScale = baseScale;
        if (sr != null)
        {
            sr.DOComplete();
            sr.color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = GetComponent<Ball>();
        if (ball == null) return;

        Flipper flipper = collision.gameObject.GetComponent<Flipper>();
        DefenseKeeper dk = collision.gameObject.GetComponent<DefenseKeeper>();
        AIGoalkeeper gk = collision.gameObject.GetComponent<AIGoalkeeper>();

        if (flipper != null || dk != null || gk != null) return;

        if (collision.contactCount > 0)
        {
            ContactPoint2D contact = collision.GetContact(0);
            PlayBounceEffect(contact.point, contact.normal);
        }
    }

    private void SpawnParticles(Vector2 position, Vector2 direction, int count, Color color)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject particle = new GameObject("Particle");
            particle.transform.position = position;

            SpriteRenderer psr = particle.AddComponent<SpriteRenderer>();
            psr.sprite = sr != null ? sr.sprite : null;
            psr.color = color;
            psr.sortingOrder = 15;
            particle.transform.localScale = Vector3.one * Random.Range(0.15f, 0.30f);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float spread = Random.Range(-60f, 60f);
            float rad = (angle + spread) * Mathf.Deg2Rad;
            Vector2 vel = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * particleSpeed * Random.Range(0.5f, 1.2f);

            Rigidbody2D prb = particle.AddComponent<Rigidbody2D>();
            prb.gravityScale = 2f;
            prb.velocity = vel;
            prb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;

            psr.DOFade(0f, particleLifetime).SetEase(Ease.InQuad);
            particle.transform.DOScale(Vector3.zero, particleLifetime).SetEase(Ease.InQuad)
                .OnComplete(() => Destroy(particle));
        }
    }
}
