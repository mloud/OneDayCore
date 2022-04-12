using UnityEngine;

public static class TransformExtensions
{
    public static void SetLocalRotationX(this Transform tr, float x)
    {
        var locRot = tr.localEulerAngles;
        locRot.x = x;
        tr.localEulerAngles = locRot;
    }

    public static void SetLocalRotationY(this Transform tr, float y)
    {
        var locRot = tr.localEulerAngles;
        locRot.y = y;
        tr.localEulerAngles = locRot;
    }

    public static void SetLocalRotationZ(this Transform tr, float z)
    {
        var locRot = tr.localEulerAngles;
        locRot.z = z;
        tr.localEulerAngles = locRot;
    }

    public static void SetLocalPositionX(this Transform tr, float x)
    {
        var locPos = tr.localPosition;
        locPos.x = x;
        tr.localPosition = locPos;
    }

    public static void SetLocalPositionY(this Transform tr, float y)
    {
        var locPos = tr.localPosition;
        locPos.y = y;
        tr.localPosition = locPos;
    }

    public static void SetLocalPositionZ(this Transform tr, float z)
    {
        var locPos = tr.localPosition;
        locPos.z = z;
        tr.localPosition = locPos;
    }
}
