using UnityEngine;
using DG.Tweening;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }

    private Vector3 originalPos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        originalPos = transform.position;
    }

    public void ShakeLight()
    {
        transform.DOComplete();
        transform.position = originalPos;
        transform.DOShakePosition(0.15f, 0.15f, 20, 90f, false, true)
            .OnComplete(() => transform.position = originalPos);
    }

    public void ShakeHeavy()
    {
        transform.DOComplete();
        transform.position = originalPos;
        transform.DOShakePosition(0.3f, 0.35f, 30, 90f, false, true)
            .OnComplete(() => transform.position = originalPos);
    }
}
