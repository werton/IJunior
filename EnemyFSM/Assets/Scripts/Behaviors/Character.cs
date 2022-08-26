using System;
using UnityEngine;

[RequireComponent(typeof(HealthPoint))]
public abstract class Character : MonoBehaviour, IDamagebleTarget
{
    private HealthPoint _health;

    public event Action<Vector3> PositionChanged;
    public event Action<int> Damaged;
    public event Action<IDamageble> Died;

    public int Health => _health.Value;

    private void Awake()
    {
        _health = GetComponent<HealthPoint>();
    }

    private void OnEnable()
    {
        _health.ValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= OnHealthChanged;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void ReciveDamage(int damageValue)
    {
        _health.Reduce(damageValue);
    }

    private void OnHealthChanged(int healthValue, int maxHealthValue)
    {
        Damaged?.Invoke(healthValue);

        if (_health.Value == 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        Died?.Invoke(this);
    }
}

