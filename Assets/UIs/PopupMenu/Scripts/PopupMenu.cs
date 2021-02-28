using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PopupMenu : MonoBehaviour
{
    public float radius = 240f;
    public float time = 0.2f;

    bool enabled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (!enabled)
            MenuEnable();
    }

    public void MenuEnable()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        transform.position = mousePos;
        var buttons = transform.GetComponentsInChildren<PopupButton>();
        int idx = 0;
        int len = buttons.Length;
        foreach (PopupButton button in buttons)
        {
            StartCoroutine(button.Animate(0f, radius, (float)idx / len * 2f * Mathf.PI, time, time * idx));
            idx++;
        }
        enabled = true;
    }

    public void MenuDisable()
    {
        var buttons = transform.GetComponentsInChildren<PopupButton>();
        int idx = 0;
        int len = buttons.Length;
        foreach (PopupButton button in buttons)
        {
            StartCoroutine(button.Animate(radius, 0f, (float)idx / len * 2f * Mathf.PI, time));
            idx++;
        }
    }
}
