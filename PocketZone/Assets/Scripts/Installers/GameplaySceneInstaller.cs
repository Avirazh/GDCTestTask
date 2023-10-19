using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [Header("Player bindings")]

    [SerializeField] private PlayerInput _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private PlayerConfig _playerConfig;

    [Header("Camera bindings")]
    [SerializeField] private CameraConfig _cameraConfig;

    public override void InstallBindings()
    {
        BindCamera();
        BindPlayer();
    }

    private void BindPlayer()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig);
        PlayerInput player = Container.InstantiatePrefabForComponent<PlayerInput>(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<PlayerInput>().FromInstance(player).AsSingle();
    }
    private void BindCamera()
    {
        Container.Bind<CameraConfig>().FromInstance(_cameraConfig);
        Container.Bind<CameraFollow>().FromComponentInHierarchy().AsSingle();
    }
}