using UnityEngine;

[RequireComponent(typeof(TargetFinder), typeof(Rigidbody2D))]
public class EnemyInput : MonoBehaviour
{
    private const float SEARCH_ITERATION_COOLDOWN = 0.2f;

    private TargetFinder _targetFinder;
    private UnitMovement _movement;
    private Rigidbody2D _rigidbody;

    private Vector3 spawnPosition;

    private LayerMask _targetLayerMask;
    private EnemyConfig _enemyConfig;

    public void SetDependencies(LayerMask spawnLayerMask, LayerMask targetLayerMask, EnemyConfig enemyConfig)
    {
        gameObject.layer = Utilities.LayerMaskToLayer(spawnLayerMask);
        _targetLayerMask = targetLayerMask;
        _enemyConfig = enemyConfig;
    }

    private void Start()
    {
        spawnPosition = transform.position;

        SetTargetFinder();
        SetUnitMovement();
        SetAttack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_targetFinder.TryGetTarget(out var target))
            _movement.Move((Vector2)(target.transform.position - transform.position), _enemyConfig.Speed);

        else
            _movement.MoveToPoint(spawnPosition - transform.position, _enemyConfig.Speed);
    }

    private void SetTargetFinder()
    {
        _targetFinder = GetComponent<TargetFinder>();
        _targetFinder.SetDependencies(_enemyConfig.AggroRadius, SEARCH_ITERATION_COOLDOWN, Color.black);

        _targetFinder.IsSearching = true;
        _targetFinder.FindTarget(_targetLayerMask);
    }

    private void SetUnitMovement()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _movement = new UnitMovement(transform, _rigidbody);
    }

    private void SetAttack()
    {
        if(TryGetComponent<MeleeEnemyAttack>(out var meleeEnemyAttack))
            meleeEnemyAttack.SetDependencies(_targetFinder, _enemyConfig.Damage, _enemyConfig.AttackRange, _enemyConfig.AttackCooldown);
    }
}