using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingButton : MonoBehaviour
{
    // Start is called before the first frame update
    float radius;
    float angle;
    float targetAngle;
    float border;
    int menuSize;
    public float maxRotateSpeed;
    public float minRotateSpeed;
    public float accelSection;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(targetAngle - angle) < 0.01f)
        {
            return;
        }
        else if (Mathf.Abs(targetAngle - angle) > accelSection)
        {
            if (targetAngle > angle)
            {
                angle += maxRotateSpeed * Time.deltaTime;
            }
            else
            {
                angle -= maxRotateSpeed * Time.deltaTime;
            }
        }
        else
        {
            float rotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, Mathf.Abs(targetAngle - angle) / accelSection);
            if (targetAngle > angle)
            {
                angle += rotateSpeed * Time.deltaTime;
            }
            else
            {
                angle -= rotateSpeed * Time.deltaTime;
            }
        }
        transform.localPosition = new Vector3(radius * Mathf.Cos(angle),
                                              radius * Mathf.Sin(angle),
                                              0);
        Display();
    }

    public void SetButton(float _radius, float _angle, float _border, int _menuSize)
    {
        radius = _radius;
        angle = _angle;
        border = _border;
        menuSize = _menuSize;
        transform.localPosition = new Vector3(radius * Mathf.Cos(angle),
                                              radius * Mathf.Sin(angle),
                                              0);
        targetAngle = angle;
        Display();
    }

    public void SetTargetAngle(float target)
    {
        targetAngle = target;
    }

    public void Display()
    {
        int pos = (int)((angle + border * 0.5f) / border);
        int order = menuSize - Mathf.Abs(pos);
        order = Mathf.Clamp(order, 0, menuSize);
        GetComponent<Canvas>().sortingOrder = order;
        //buttons[idx].GetComponent<CanvasRenderer>().SetAlpha((float)order / menuSize);
        var buttonRenderers = GetComponentsInChildren<CanvasRenderer>();
        foreach (CanvasRenderer renderer in buttonRenderers)
            renderer.SetAlpha((float)order / menuSize);
    }
}
