using UnityEngine;
using UnityEngine.EventSystems;

public class FlipperInput : MonoBehaviour
{
    public Flipper leftFlipper;
    public Flipper rightFlipper;

    private void Update()
    {
        HandleTouchInput();
        HandleMouseInput();
    }

    private void HandleTouchInput()
    {
        bool leftActive = false;
        bool rightActive = false;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                continue;

            if (touch.position.x < Screen.width / 2f)
                leftActive = true;
            else
                rightActive = true;
        }

        if (Input.touchCount > 0)
        {
            if (leftActive) leftFlipper.Activate();
            else leftFlipper.Deactivate();

            if (rightActive) rightFlipper.Activate();
            else rightFlipper.Deactivate();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.touchCount > 0) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2f)
                leftFlipper.Activate();
            else
                rightFlipper.Activate();
        }

        if (Input.GetMouseButtonUp(0))
        {
            leftFlipper.Deactivate();
            rightFlipper.Deactivate();
        }

        if (!Input.GetMouseButton(0))
        {
            leftFlipper.Deactivate();
            rightFlipper.Deactivate();
        }
    }
}
