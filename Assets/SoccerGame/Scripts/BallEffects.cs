using UnityEngine;
using DG.Tweening;

public class BallEffects : MonoBehaviour
{
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

        float angle = Mathf.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg;
        Vector3 squashScale = new Vector3(baseScale.x * 1.4f, baseScale.y * 0.6f, 1f);

        transform.localScale = squashScale;
        transform.DOScale(baseScale, 0.2f).SetEase(Ease.OutElastic);

        if (sr != null)
        {
            sr.DOComplete();
            sr.DOColor(new Color(1f, 1f, 0.5f), 0.05f)
                .OnComplete(() => sr.DOColor(Color.white, 0.15f));
        }
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
}
