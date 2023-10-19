using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private LayerMask _spawnLayerMask;

    [SerializeField] private List<EnemyConfig> _enemyTypes;

    [Header("Two transforms creating an area where enemies will spawn")]
    [SerializeField] private Transform _spawnAreaMinLeft;
    [SerializeField] private Transform _spawnAreaMaxRight;

    [Header("Debugging")]
    public Color GizmoColor;

    private Camera _camera;

    [Inject]
    private void Construct(CameraFollow camera)
    {
        _camera = camera.GetComponent<Camera>();
    }

    private void Start()
    {
        SpawnEnemyByDifficulty(EnemyDifficulty.Easy);
        SpawnEnemyByDifficulty(EnemyDifficulty.Easy);
        SpawnEnemyByDifficulty(EnemyDifficulty.Easy);
    }

    private void SpawnEnemyByDifficulty(EnemyDifficulty difficulty)
    {
        var enemyConfig = _enemyTypes.Find(x => x.Difficulty == difficulty);
        var enemyToSpawn = Instantiate(enemyConfig.Prefab, FindPlaceToSpawn(), Quaternion.identity)
            .AddComponent<EnemyInput>();

        enemyToSpawn.SetDependencies(_spawnLayerMask, _targetLayerMask, enemyConfig);

    }
    private Vector3 FindPlaceToSpawn()
    {
        Vector3 cameraWorldPointMin = _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.transform.position.z));
        Vector3 cameraWorldPointMax = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, _camera.transform.position.z));

        return GenerateOffCamera(GenerateRandomPosition(), cameraWorldPointMin, cameraWorldPointMax);
    }
    private Vector3 GenerateRandomPosition()
    {
        return new Vector3(
            Random.Range(_spawnAreaMinLeft.position.x, _spawnAreaMaxRight.position.x),
            Random.Range(_spawnAreaMinLeft.position.y, _spawnAreaMaxRight.position.y),
            0);
    }

    private Vector3 GenerateOffCamera(Vector3 spawnPosition, Vector3 cameraMin, Vector3 cameraMax)
    {
        while (spawnPosition.x < cameraMax.x && spawnPosition.x > cameraMin.x &&
            spawnPosition.y < cameraMax.y && spawnPosition.y > cameraMin.y)
        {
            spawnPosition = GenerateRandomPosition();
        }
        return spawnPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;

        float squareWidth = Mathf.Abs(_spawnAreaMaxRight.position.x - _spawnAreaMinLeft.position.x);
        float squareHeight = Mathf.Abs(_spawnAreaMaxRight.position.y - _spawnAreaMinLeft.position.y);

        Gizmos.DrawCube(Vector3.Lerp(_spawnAreaMinLeft.transform.position, _spawnAreaMaxRight.transform.position, 0.5f), 
            new Vector3(squareWidth, squareHeight, 0));
    }
}
