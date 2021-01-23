using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RollingUI : MonoBehaviour
{
    // Start is called before the first frame update
    Transform[] buttons;
    int[] buttonPosition;
    const float vectorSizePerScroll = 120f;
    float scrollSpeed = 0.2f;
    int menuSize = 5;
    float UIRadius = 120f;
    int buttonCount;

    void Start()
    {
        buttonCount = transform.childCount;
        buttonPosition = new int[buttonCount];
        buttons = new Transform[buttonCount];
        for (int idx = 0; idx < buttonCount; idx++)
        {
            buttons[idx] = transform.GetChild(idx);
            buttonPosition[idx] = -idx;
        }
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Display()
    {
        for (int idx = 0; idx < buttonCount; idx++)
        {
            buttons[idx].localPosition = new Vector3(UIRadius * Mathf.Cos(scrollSpeed * buttonPosition[idx]),
                                                     UIRadius * Mathf.Sin(scrollSpeed * buttonPosition[idx]),
                                                     0);
            int order = menuSize - Mathf.Abs(buttonPosition[idx]);
            order = Mathf.Clamp(order, 0, menuSize);
            buttons[idx].GetComponent<Canvas>().sortingOrder = order;
            //buttons[idx].GetComponent<CanvasRenderer>().SetAlpha((float)order / menuSize);
            var buttonRenderers = buttons[idx].GetComponentsInChildren<CanvasRenderer>();
            foreach (CanvasRenderer renderer in buttonRenderers)
                renderer.SetAlpha((float)order / menuSize);
        }
    }

    void UIScroll(int moveDelta)
    {
        for (int idx = 0; idx < buttonCount; idx++)
        {
            buttonPosition[idx] += moveDelta;
        }
        Display();
    }

    public void OnScrollWheel(InputValue input)
    {
        Vector2 scroll = input.Get<Vector2>();
        if (scroll.y > 0 && buttonPosition[buttonCount - 1] < 0)
        {
            UIScroll(1);
        }
        else if (scroll.y < 0 && buttonPosition[0] > 0)
        {
            UIScroll(-1);
        }
    }
}
