using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelSelectButton : MonoBehaviour
{
    public TextMeshProUGUI levelNumberText;
    public TextMeshProUGUI goalsInfoText;
    public GameObject lockIcon;
    public Button button;
    public Image background;

    private int levelNumber;
    private bool isUnlocked;

    public void Setup(int level, bool unlocked)
    {
        levelNumber = level;
        isUnlocked = unlocked;

        levelNumberText.text = level.ToString();

        LevelConfig config = LevelDatabase.GetLevel(level);
        goalsInfoText.text = config.goalsToWin + " goals";

        lockIcon.SetActive(!unlocked);
        levelNumberText.gameObject.SetActive(unlocked);
        goalsInfoText.gameObject.SetActive(unlocked);

        button.interactable = unlocked;
        background.color = unlocked
            ? new Color(0.2f, 0.55f, 0.3f)
            : new Color(0.3f, 0.3f, 0.3f);

        button.onClick.RemoveAllListeners();
        if (unlocked)
            button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        transform.DOComplete();
        transform.DOPunchScale(Vector3.one * 0.15f, 0.2f, 5).SetUpdate(true)
            .OnComplete(() =>
            {
                GameStarter starter = GameStarter.Instance;
                if (starter == null)
                {
                    GameObject obj = new GameObject("GameStarter");
                    starter = obj.AddComponent<GameStarter>();
                }
                starter.StartLevel(levelNumber);
            });
    }
}
