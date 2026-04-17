using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class TutorialUI : MonoBehaviour
{
    public GameObject panel;
    public RectTransform contentRect;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;
    public TextMeshProUGUI pageIndicatorText;
    public Button nextButton;
    public Button skipButton;
    public TextMeshProUGUI nextButtonLabel;

    private int currentSlide;
    private List<TutorialSlide> slides;

    private struct TutorialSlide
    {
        public string title;
        public string body;

        public TutorialSlide(string t, string b)
        {
            title = t;
            body = b;
        }
    }

    private void Awake()
    {
        CreateSlides();
    }

    private void OnEnable()
    {
        if (nextButton != null)
        {
            nextButton.onClick.RemoveListener(OnNext);
            nextButton.onClick.AddListener(OnNext);
        }
        if (skipButton != null)
        {
            skipButton.onClick.RemoveListener(OnSkip);
            skipButton.onClick.AddListener(OnSkip);
        }
    }

    private void OnDisable()
    {
        if (nextButton != null)
            nextButton.onClick.RemoveListener(OnNext);
        if (skipButton != null)
            skipButton.onClick.RemoveListener(OnSkip);
    }

    private void CreateSlides()
    {
        slides = new List<TutorialSlide>
        {
            new TutorialSlide(
                "WELCOME!",
                "Welcome to Soccer Game!\n\nA fast-paced pinball-style\nsoccer experience.\n\nLet's learn how to play!"
            ),
            new TutorialSlide(
                "CONTROLS",
                "Tap the LEFT side of the screen\nto activate the left flipper.\n\nTap the RIGHT side of the screen\nto activate the right flipper.\n\nTime your taps to hit the ball!"
            ),
            new TutorialSlide(
                "SCORE GOALS",
                "Hit the ball into the\nTOP GOAL to score!\n\nThe AI goalkeeper will try\nto block your shots.\n\nUse obstacles and angles\nto outsmart the keeper!"
            ),
            new TutorialSlide(
                "DEFEND",
                "Don't let the ball get\npast your flippers!\n\nIf the ball falls below,\nthe opponent scores.\n\nStay sharp and react fast!"
            ),
            new TutorialSlide(
                "GAME MODES",
                "MATCH MODE\nFirst to 5 goals wins!\n\nENDLESS MODE\nScore as many as you can.\nBuild streaks for bonus points!\n\nLEVELS\n30 levels with unique obstacles.\nDon't concede a single goal!"
            )
        };
    }

    public bool ShouldShow()
    {
        return PlayerPrefs.GetInt("TutorialShown", 0) == 0;
    }

    public void Show()
    {
        panel.SetActive(true);
        currentSlide = 0;
        ShowSlide();

        if (contentRect != null)
        {
            contentRect.localScale = Vector3.zero;
            contentRect.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetUpdate(true);
        }
    }

    private void ShowSlide()
    {
        TutorialSlide slide = slides[currentSlide];

        titleText.text = slide.title;
        bodyText.text = slide.body;
        pageIndicatorText.text = (currentSlide + 1) + " / " + slides.Count;

        bool isLast = currentSlide >= slides.Count - 1;
        nextButtonLabel.text = isLast ? "GOT IT!" : "NEXT";
        skipButton.gameObject.SetActive(!isLast);

        titleText.transform.DOComplete();
        titleText.transform.localScale = Vector3.one * 0.5f;
        titleText.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);

        bodyText.DOComplete();
        Color c = bodyText.color;
        bodyText.color = new Color(c.r, c.g, c.b, 0f);
        bodyText.DOFade(1f, 0.3f).SetDelay(0.1f).SetUpdate(true);
    }

    private void OnNext()
    {
        if (currentSlide < slides.Count - 1)
        {
            currentSlide++;
            ShowSlide();
        }
        else
        {
            Close();
        }
    }

    private void OnSkip()
    {
        Close();
    }

    private void Close()
    {
        PlayerPrefs.SetInt("TutorialShown", 1);
        PlayerPrefs.Save();

        if (contentRect != null)
        {
            contentRect.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).SetUpdate(true)
                .OnComplete(() => panel.SetActive(false));
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
