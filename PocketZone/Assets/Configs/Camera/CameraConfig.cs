using UnityEngine;


[CreateAssetMenu(fileName = "CameraConfigData", menuName = "Configs/CameraConfig", order = 51)]
public class CameraConfig : ScriptableObject
{
    [field : SerializeField, Range(1, 20)] public float CameraOffset { get; private set; }
    [field : SerializeField, Range(0, 2)] public float CameraSmoothTime { get; private set; }
}
