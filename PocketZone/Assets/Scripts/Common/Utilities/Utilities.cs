using UnityEngine;

public static class Utilities
{
    public static void RotateObjectToTarget(Transform objectTransform, Transform target)
    {
        var localPosition = objectTransform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(localPosition.y, localPosition.x) * Mathf.Rad2Deg;

        objectTransform.Rotate(0, 0, angle);
    }
    public static int LayerMaskToLayer(LayerMask layerMask)
    {
        return (int)Mathf.Log(layerMask.value, 2);
    }
}
