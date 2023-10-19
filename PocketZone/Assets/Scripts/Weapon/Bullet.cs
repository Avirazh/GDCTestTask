using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    private float _speed;
    private float _damage;

    private Vector2 _direction;
    [SerializeField] private LayerMask _layerMask;

    public void SetDependencies(Transform target, float speed, float damage, LayerMask layerMask)
    {
        _target = target;
        _speed = speed;
        _damage = damage;

        _direction = (_target.position - transform.position).normalized;
        _layerMask = layerMask;
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_layerMask == (_layerMask | (1 << collision.gameObject.layer)))
        {
            Debug.Log(collision.gameObject.name);
            //event here
            Destroy(gameObject);
        }
    }
}
