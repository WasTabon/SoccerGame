using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndlessMenuButton : MonoBehaviour
{
    private void OnEnable()
    {
        Button btn = GetComponent<Button>();
        Debug.Assert(btn != null, "EndlessMenuButton: Button not found!");
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
        if (GameStarter.Instance != null)
            Destroy(GameStarter.Instance.gameObject);

        SceneManager.LoadScene("MainMenu");
    }
}
