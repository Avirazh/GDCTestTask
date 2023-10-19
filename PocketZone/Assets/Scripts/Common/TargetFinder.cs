using System.Collections;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{ 
    [SerializeField] private float _searchRadius;    
    [SerializeField] private Color _gizmosColor;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _searchIterationCooldown;

    private Vector2 _center;
    private Transform _target;

    public Transform Target => _target;

    public bool IsSearching = true;
    private void Awake()
    {
        _center = transform.position;
    }

    private void Start()
    {
        StartCoroutine(FindTarget(IsSearching));
    }

    public bool TryGetTarget(out Transform target)
    {
        target = Target;
        
        return target ? true : false;
    }

    private IEnumerator FindTarget(bool isSearching)
    {
        while (isSearching)
        {
            _center = transform.position;

            var hitCollider = Physics2D.OverlapCircle(_center, _searchRadius, _layerMask);

            if (hitCollider)
            {
                _target = hitCollider.transform;
                Debug.Log($"Target finded: {hitCollider.gameObject.name}");

                
            }            
            if (_target && Vector2.Distance((Vector2)_target.position, _center) > _searchRadius)
            {
                _target = null;
                Debug.Log($"No target avaliable: _target = {_target}, Target = {Target}");
            }
            yield return new WaitForSeconds(_searchIterationCooldown);
        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawWireSphere(_center, _searchRadius);       
    }
}
