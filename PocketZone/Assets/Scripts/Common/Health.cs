using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private HealthBarRect _healthBar;
    [SerializeField] private Transform _healthBarPosition;

    public Action<GameObject> OnDeath;
    public Action<float, float> OnHealthChanged;

    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    private void Start()
    {
        CreateHealthBar();
    }
    public void ApplyDamage(float damage)
    {
        damage = -damage;
        _currentHealth += damage;

        OnHealthChanged?.Invoke(damage, _currentHealth);
        TryDie();
    }
    public void ApplyHeal(float heal) 
    {
        _currentHealth += heal;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        OnHealthChanged?.Invoke(heal, _currentHealth);
    }

    private void TryDie()
    {
        if(_currentHealth <= 0)
        {
            OnDeath?.Invoke(gameObject);
            OnHealthChanged = null;
            Destroy(gameObject);
        }
    }
    private void CreateHealthBar()
    {
        HealthBarRect healthBar = Instantiate(_healthBar);

        healthBar.gameObject.transform.SetParent(_healthBarPosition.transform, false);
        healthBar.GetComponentInChildren<HealthBarRect>().SetHealthComponent(this, MaxHealth);
    }
}
