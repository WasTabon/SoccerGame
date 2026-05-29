using UnityEngine;

public class BallSkinApplier : MonoBehaviour
{
    private SpriteRenderer sr;
    private BallGlow glow;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ApplyActiveSkin();
    }

    private void OnEnable()
    {
        SkinManager.OnSkinChanged -= OnSkinChanged;
        SkinManager.OnSkinChanged += OnSkinChanged;
    }

    private void OnDisable()
    {
        SkinManager.OnSkinChanged -= OnSkinChanged;
    }

    private void OnSkinChanged(string skinId)
    {
        ApplyActiveSkin();
    }

    public void ApplyActiveSkin()
    {
        if (SkinManager.Instance == null) return;

        SkinData skin = SkinManager.Instance.GetActiveSkin();
        if (skin == null) return;

        if (sr != null)
            sr.color = skin.color;

        glow = GetComponentInChildren<BallGlow>();
        if (glow != null)
            glow.SetGlowColor(skin.color);
    }
}
