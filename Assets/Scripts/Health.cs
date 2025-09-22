using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    private const int DefaultValue = 0;
    private const int MinHealth = 0;
    private const int MinHealAmount = 0;
    private const int MinAllowedMaxHealth = 1;
    private const int MinDamageAmount = 0;

    [Header("Health Settings")]
    [SerializeField] private int _max = 100;
    [SerializeField] private int _current = 100;

    public event Action<int, int> Changed;
    public event Action<int, int> MaxChanged;
    public event Action Died;
    public event Action Revived;

    public int Current => _current;
    public int Max => _max;
    public bool IsAlive { get; private set; } = true;
    public float Normalized => _max > MinAllowedMaxHealth ? (float)_current / _max : DefaultValue;

    public void TakeDamage(int damage)
    {
        if (IsAlive == false || damage <= MinDamageAmount)
            return;

        _current = Mathf.Max(MinHealth, _current - damage);
        Changed?.Invoke(_current, _max);

        if (_current <= MinHealth)
            Die();
    }

    public void Heal(int amount)
    {
        if (IsAlive == false || amount <= MinHealAmount)
            return;

        _current = Mathf.Min(_max, _current + amount);
        Changed?.Invoke(_current, _max);
    }

    public void SetMax(int newMaxHealth, bool healToFull = false)
    {
        _max = Mathf.Max(MinAllowedMaxHealth, newMaxHealth);

        _current = healToFull ? _max : Mathf.Min(_current, _max);

        MaxChanged?.Invoke(_current, _max);
        Changed?.Invoke(_current, _max);
    }

    public void Die()
    {
        if (IsAlive == false)
            return;

        IsAlive = false;
        Died?.Invoke();
    }

    public void Revive(int healthAmount = 0)
    {
        if (IsAlive)
            return;

        IsAlive = true;
        _current = healthAmount > MinHealAmount ? Mathf.Min(healthAmount, _max) : _max;

        Revived?.Invoke();
        Changed?.Invoke(_current, _max);
    }

    protected virtual void OnValidate()
    {
        _max = Mathf.Max(MinAllowedMaxHealth, _max);
        _current = Mathf.Clamp(_current, MinHealth, _max);
    }
}