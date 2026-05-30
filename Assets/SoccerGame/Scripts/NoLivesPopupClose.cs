using UnityEngine;
using UnityEngine.UI;

public class NoLivesPopupClose : MonoBehaviour
{
    private void OnEnable()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.RemoveListener(Close);
            btn.onClick.AddListener(Close);
        }
    }

    private void OnDisable()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.RemoveListener(Close);
    }

    private void Close()
    {
        transform.parent.parent.gameObject.SetActive(false);
    }
}
