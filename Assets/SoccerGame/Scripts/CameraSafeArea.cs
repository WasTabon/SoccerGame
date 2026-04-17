using UnityEngine;

public class CameraSafeArea : MonoBehaviour
{
    private Camera cam;
    private Rect lastSafeArea;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        Debug.Assert(cam != null, "CameraSafeArea: Camera not found!");
        ApplySafeArea();
    }

    private void Update()
    {
        if (Screen.safeArea != lastSafeArea)
            ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        lastSafeArea = safeArea;

        float x = safeArea.x / Screen.width;
        float y = safeArea.y / Screen.height;
        float w = safeArea.width / Screen.width;
        float h = safeArea.height / Screen.height;

        cam.rect = new Rect(x, y, w, h);
    }
}
