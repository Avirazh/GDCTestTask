using System.Collections;
using UnityEngine;

public class HealthBarRect : MonoBehaviour
{
    [SerializeField] private RectTransform _topRectTransform;
    [SerializeField] private RectTransform _bottomRectTransform;
    [SerializeField] private float _animationSpeed = 10f;

    private float _barFullWidth;
    private float _targetWidth;
    private Coroutine _adjustBarWidthCoroutine;

    private Health _health;
    private float _maxHealth;

    public void SetHealthComponent(Health health, float maxHealth)
    {
        _health = health;
        _maxHealth = maxHealth;

        _health.OnHealthChanged += ChangeBar;

        gameObject.SetActive(true);
        Debug.Log(gameObject.activeInHierarchy);

        _barFullWidth = _topRectTransform.rect.width;
        Debug.Log(_barFullWidth);
    }

    public void ChangeBar(float damageAmount, float currentHealth)
    {
        _targetWidth = currentHealth * _barFullWidth / _maxHealth;

        if (_adjustBarWidthCoroutine != null)
            StopCoroutine(_adjustBarWidthCoroutine);

        _adjustBarWidthCoroutine = StartCoroutine(AdjustBarWidth(damageAmount, currentHealth));
    }

    private IEnumerator AdjustBarWidth(float amount, float currentHealth)
    {
        var suddenChangeBar = amount >= 0 ? _bottomRectTransform : _topRectTransform;
        var slowChangeBar = amount >= 0 ? _topRectTransform : _bottomRectTransform;

        Debug.Log($"AdjustBarWidth: {currentHealth}, {_targetWidth}");
        suddenChangeBar.SetWidth(_targetWidth);

        while(Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            slowChangeBar.SetWidth(Mathf.Lerp(slowChangeBar.rect.width, _targetWidth, Time.deltaTime * _animationSpeed));

            yield return null;
        }

        slowChangeBar.SetWidth(_targetWidth);
    }
}
