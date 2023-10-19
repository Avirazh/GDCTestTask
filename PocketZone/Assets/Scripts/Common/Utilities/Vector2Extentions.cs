using UnityEngine;

public static class Vector2Extentions
{
    public static void RotateObjectToTarget(Transform objectTransform, Transform target)
    {
        var localPosition = objectTransform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(localPosition.y, localPosition.x) * Mathf.Rad2Deg;

        objectTransform.Rotate(0, 0, angle);
    }
}
