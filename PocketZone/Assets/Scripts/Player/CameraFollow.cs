using UnityEngine;
using Zenject;


public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _cameraOffsetZ;
    [SerializeField] private float _cameraSmoothTime;

    private Vector3 _velocity = Vector3.zero;

    [Inject]
    private void Construct(CameraConfig cameraConfig, IMovable playerTransform)
    {
        _cameraOffsetZ = cameraConfig.CameraOffset;
        _cameraSmoothTime = cameraConfig.CameraSmoothTime;
        _playerTransform = playerTransform.Transform;
    }

    private void Awake()
    {
        transform.position = SetPosition();
    }


    private void FixedUpdate()
    {
        if(_playerTransform != null)
            transform.position = Vector3.SmoothDamp(transform.position, SetPosition(), ref _velocity, _cameraSmoothTime);
    }

    private Vector3 SetPosition()
    {
        return new Vector3()
        {
            x = _playerTransform.position.x,
            y = _playerTransform.position.y,
            z = _playerTransform.position.z - _cameraOffsetZ
        };
    }
    
}
