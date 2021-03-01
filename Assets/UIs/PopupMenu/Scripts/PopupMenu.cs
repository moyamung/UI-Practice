using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PopupMenu : MonoBehaviour
{
    public float radius = 240f;
    public float time = 0.2f;
    public GameObject cancelButton;

    [SerializeField]
    bool isEnabled;

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
        if (!EventSystem.current.IsPointerOverGameObject())
            MenuEnable();
    }

    public void MenuEnable()
    {
        if (isEnabled)
            return;
        Vector2 mousePos = Mouse.current.position.ReadValue();
        transform.position = mousePos;
        var buttons = transform.GetComponentsInChildren<PopupButton>(true);
        int idx = 0;
        int len = buttons.Length;
        foreach (PopupButton button in buttons)
        {
            button.gameObject.SetActive(true);
            StartCoroutine(button.Animate(0f, radius, (float)idx / len * 2f * Mathf.PI, time, time * idx));
            idx++;
        }
        cancelButton.SetActive(true);
        isEnabled = true;
    }

    public void MenuDisable()
    {
        if (!isEnabled)
            return;
        var buttons = transform.GetComponentsInChildren<PopupButton>();
        int idx = 0;
        int len = buttons.Length;
        foreach (PopupButton button in buttons)
        {
            StartCoroutine(button.Animate(radius, 0f, (float)idx / len * 2f * Mathf.PI, time));
            idx++;
        }
        cancelButton.SetActive(false);
        isEnabled = false;
    }
}
