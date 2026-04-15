using UnityEngine;
using UnityEngine.UI;

public class InGameMenuButton : MonoBehaviour
{
    private void OnEnable()
    {
        Button btn = GetComponent<Button>();
        Debug.Assert(btn != null, "InGameMenuButton: Button not found!");
        btn.onClick.RemoveListener(GoToMenu);
        btn.onClick.AddListener(GoToMenu);
    }

    private void OnDisable()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.RemoveListener(GoToMenu);
    }

    private void GoToMenu()
    {
        Time.timeScale = 1f;
        MatchEndUI.GoToMenu();
    }
}
