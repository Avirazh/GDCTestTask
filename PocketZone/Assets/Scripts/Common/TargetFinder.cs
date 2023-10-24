using System.Collections;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{ 
    [SerializeField] private float _searchRadius;    
    [SerializeField] private float _searchIterationCooldown;

    [Header("Debugging")]
    [SerializeField] private Color _gizmosColor;

    public LayerMask _layerMask;
    private Vector2 _center;
    private Transform _target;

    public Transform Target => _target;
    public bool IsSearching { get; set; }
    public LayerMask LayerMask => _layerMask;

    public void SetDependencies(float searchRadius, float searchIterationCooldown, Color gizmoColor)
    {
        _searchRadius = searchRadius;
        _searchIterationCooldown = searchIterationCooldown;
        _gizmosColor = gizmoColor;
    }

    public void FindTarget(LayerMask layerMask)
    {
        _layerMask = layerMask;

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
            //Debug.Log($"searching... current hitCollider: {hitCollider}");
            if (hitCollider)
            {
                _target = hitCollider.transform;
                //Debug.Log($"Target finded: {hitCollider.gameObject.name}");

                
            }            
            if (_target && Vector2.Distance((Vector2)_target.position, _center) > _searchRadius)
            {
                _target = null;
                //Debug.Log($"No target avaliable: _target = {_target}, Target = {Target}");
            }
            yield return new WaitForSeconds(_searchIterationCooldown);
        }        
    }

    private void OnDrawGizmos()
    {       
        if (Target)
            Gizmos.color = Color.green;
        else 
            Gizmos.color = _gizmosColor;
        Gizmos.DrawWireSphere(_center, _searchRadius);
    }
}
