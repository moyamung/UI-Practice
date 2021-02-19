using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VerticalRollingUI : MonoBehaviour
{
    // Start is called before the first frame update
    Transform[] buttons;
    int[] buttonPosition;
    const float vectorSizePerScroll = 120f;
    float scrollSpeed = 0.2f;
    int menuSize = 5;
    
    public float UIRadius = 120f;
    public float UIDistance = 500f;

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
            buttons[idx].GetComponent<VerticalRollingButton>().SetButton(UIDistance, UIRadius, scrollSpeed * buttonPosition[idx], scrollSpeed, menuSize);
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
            buttons[idx].GetComponent<VerticalRollingButton>().SetTargetAngle(scrollSpeed * buttonPosition[idx]);
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
