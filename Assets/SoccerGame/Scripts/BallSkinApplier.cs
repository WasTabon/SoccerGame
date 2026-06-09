using UnityEngine;

public class BallSkinApplier : MonoBehaviour
{
    private SpriteRenderer sr;
    private Sprite defaultSprite;
    private BallGlow glow;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            defaultSprite = sr.sprite;
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
        {
            if (skin.ballSprite != null)
            {
                sr.sprite = skin.ballSprite;
                sr.color = Color.white;
            }
            else
            {
                sr.sprite = defaultSprite;
                sr.color = skin.color;
            }
        }

        glow = GetComponentInChildren<BallGlow>();
        if (glow != null)
            glow.SetGlowColor(skin.color);
    }
}
