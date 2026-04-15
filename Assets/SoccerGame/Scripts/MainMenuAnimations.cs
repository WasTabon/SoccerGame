using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class MainMenuAnimations : MonoBehaviour
{
    public RectTransform titleRect;
    public RectTransform matchButtonRect;
    public RectTransform endlessButtonRect;

    private void OnEnable()
    {
        PlayEntrance();
    }

    private void PlayEntrance()
    {
        if (titleRect != null)
        {
            titleRect.anchoredPosition += new Vector2(0, 200f);
            Vector2 targetPos = titleRect.anchoredPosition - new Vector2(0, 200f);
            titleRect.DOAnchorPos(targetPos, 0.6f).SetEase(Ease.OutBack).SetUpdate(true);

            TextMeshProUGUI titleTmp = titleRect.GetComponent<TextMeshProUGUI>();
            if (titleTmp != null)
            {
                Color c = titleTmp.color;
                titleTmp.color = new Color(c.r, c.g, c.b, 0f);
                titleTmp.DOFade(1f, 0.4f).SetUpdate(true);
            }
        }

        AnimateButton(matchButtonRect, 0.3f);
        AnimateButton(endlessButtonRect, 0.45f);
    }

    private void AnimateButton(RectTransform rect, float delay)
    {
        if (rect == null) return;

        rect.localScale = Vector3.zero;
        rect.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetDelay(delay).SetUpdate(true);
    }
}
