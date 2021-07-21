using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class UIDragHandler : EventTrigger
{

    private bool dragging;
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }


    public void Update()
    {
    }


    public void MouseDragPos(CallbackContext context)
    {
        Vector3 mousePos = context.action.ReadValue<Vector2>();
        Vector3 worldPos = mainCam.ScreenToViewportPoint(mousePos);
        
        

        print(worldPos);

        if (dragging)
            gameObject.GetComponent<RectTransform>().anchoredPosition = mousePos;
    }

    public void mouseClickTest(CallbackContext context)
    {
        if (context.started)
        {
            dragging = true;
            print("Click");
        }
        if (context.canceled)
        {
            dragging = false;
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        dragging = true;
        print("click" + eventData.clickCount);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        
        dragging = true;
        print("hit");
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
        print("hit");
    }
}