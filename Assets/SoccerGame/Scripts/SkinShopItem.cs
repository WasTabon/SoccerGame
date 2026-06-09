using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SkinShopItem : MonoBehaviour
{
    public Image previewImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Button actionButton;
    public Image actionButtonImage;
    public TextMeshProUGUI actionButtonLabel;

    [HideInInspector]
    public SkinShopUI shopUI;

    private SkinData skinData;

    public void Setup(SkinData skin, bool owned, bool equipped)
    {
        skinData = skin;

        if (skin.ballSprite != null)
        {
            previewImage.sprite = skin.ballSprite;
            previewImage.color = Color.white;
        }
        else
        {
            previewImage.sprite = null;
            previewImage.color = skin.color;
        }

        nameText.text = skin.displayName;

        if (equipped)
        {
            priceText.text = "";
            actionButtonLabel.text = "EQUIPPED";
            actionButtonImage.color = new Color(0.3f, 0.3f, 0.3f);
            actionButton.interactable = false;
        }
        else if (owned)
        {
            priceText.text = "";
            actionButtonLabel.text = "EQUIP";
            actionButtonImage.color = new Color(0.2f, 0.6f, 0.3f);
            actionButton.interactable = true;
        }
        else
        {
            priceText.text = skin.price.ToString();
            actionButtonLabel.text = "BUY";
            int coins = CoinManager.Instance != null ? CoinManager.Instance.coins : PlayerPrefs.GetInt("Coins", 0);
            bool canAfford = coins >= skin.price;
            actionButtonImage.color = canAfford ? new Color(0.2f, 0.5f, 0.7f) : new Color(0.5f, 0.3f, 0.3f);
            actionButton.interactable = canAfford;
        }

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(OnAction);
    }

    private void OnAction()
    {
        transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 5).SetUpdate(true);
        shopUI.OnSkinAction(skinData);
    }
}
