using UnityEngine;
using DG.Tweening;

public class BallGlow : MonoBehaviour
{
    public float pulseMin = 0.3f;
    public float pulseMax = 0.6f;
    public float pulseDuration = 0.8f;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Debug.Assert(sr != null, "BallGlow: SpriteRenderer not found!");
    }

    private void Start()
    {
        sr.DOFade(pulseMax, pulseDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .From(pulseMin);
    }

    public void SetGlowColor(Color color)
    {
        color.a = sr.color.a;
        sr.color = color;
    }
}
