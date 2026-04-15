using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }

    public Image fadeImage;
    public float fadeDuration = 0.4f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.color = Color.black;
            fadeImage.raycastTarget = true;
            fadeImage.DOFade(0f, fadeDuration).SetUpdate(true)
                .OnComplete(() => fadeImage.raycastTarget = false);
        }
    }

    public void LoadScene(string sceneName)
    {
        if (fadeImage == null)
        {
            SceneManager.LoadScene(sceneName);
            return;
        }

        fadeImage.raycastTarget = true;
        fadeImage.DOComplete();
        fadeImage.DOFade(1f, fadeDuration).SetUpdate(true)
            .OnComplete(() =>
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(sceneName);
            });
    }
}
