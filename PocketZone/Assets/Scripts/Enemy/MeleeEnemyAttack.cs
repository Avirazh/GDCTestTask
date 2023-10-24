using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeEnemyAttack : MonoBehaviour
{
    private const float DAMAGE_TARGET_ITERATION_COOLDOWN = 0.2f;

    private TargetFinder _targetFinder;
    private float _damage;
    private float _attackRange;
    private float _attackCooldown;

    public void SetDependencies(TargetFinder targetFinder, float damage, float attackRange, float attackCooldown)
    {
        _targetFinder = targetFinder;
        _damage = damage;
        _attackRange = attackRange;
        _attackCooldown = attackCooldown;

        StartCoroutine(DamageTargetCoroutine());
    }

    private IEnumerator DamageTargetCoroutine()
    {
        while (true)
        {
            if (_targetFinder.TryGetTarget(out var target))
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= _attackRange)
                {
                    target.gameObject.GetComponent<Health>().ApplyDamage(_damage);

                    yield return new WaitForSeconds(_attackCooldown);
                }
            }

            yield return new WaitForSeconds(DAMAGE_TARGET_ITERATION_COOLDOWN);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
