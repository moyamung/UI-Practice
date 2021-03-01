using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalRollingButton : MonoBehaviour
{
    // Start is called before the first frame update
    float radius;
    float distance;
    float angle;
    float targetAngle;
    float border;
    int menuSize;
    public float maxRotateSpeed;
    public float minRotateSpeed;
    public float accelSection;

    public Vector2 size;

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
        SetPosition();
        Display();
    }

    public void SetButton(float _distance, float _radius, float _angle, float _border, int _menuSize)
    {
        distance = _distance;
        radius = _radius;
        angle = _angle;
        border = _border;
        menuSize = _menuSize;
        targetAngle = angle;
        SetPosition();
        Display();
    }

    public void SetTargetAngle(float target)
    {
        targetAngle = target;
    }

    void SetPosition()
    {
        transform.localPosition = new Vector3(0,
                                              radius * Mathf.Sin(angle),
                                              0);
        var sizeCoeff = distance / (distance + radius * (1 - Mathf.Cos(angle)));
        GetComponent<RectTransform>().sizeDelta = size * sizeCoeff;
    }

    public void Display()
    {
        int pos = Mathf.RoundToInt(angle / border);
        int order = menuSize - Mathf.Abs(pos);
        order = Mathf.Clamp(order, 0, menuSize);
        GetComponent<Canvas>().sortingOrder = order;
        //buttons[idx].GetComponent<CanvasRenderer>().SetAlpha((float)order / menuSize);
        var buttonRenderers = GetComponentsInChildren<CanvasRenderer>();
        foreach (CanvasRenderer renderer in buttonRenderers)
        {
            float alpha = Mathf.Clamp(1 - Mathf.Abs(angle) / border / (float)menuSize, 0, 1);
            renderer.SetAlpha(alpha);
        }
    }
}
