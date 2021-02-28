using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Publics : MonoBehaviour
{
    /// <summary>
    /// calculate position on the circle.
    /// </summary>
    /// <returns>polar coordinates (radius, theta) to cartesian coordinates.</returns>
    public static Vector3 CirclePos (float radius, float theta)
    {
        if (radius == 0f) return Vector3.zero;
        return new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
    }
}
