using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UCamera
{
    public static float sensitivity = 3F;

    public static float minimumX = -360F;
    public static float maximumX = 360F;

    public static float minimumY = -90F;
    public static float maximumY = 90F;

    public static float rotationX;
    public static float rotationY;

    public static  Quaternion originalRotation;

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
