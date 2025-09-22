using UnityEngine;

public abstract class HealthVisualizer : MonoBehaviour
{
    [Header("Health Reference")]
    [SerializeField] protected Health Health;

    protected virtual void Start()
    {
        TryFindHealthComponent();
        SubscribeToEvents();
        UpdateDisplay();
    }

    protected virtual void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    protected virtual void TryFindHealthComponent()
    {
        if (Health == null)
            Health = GetComponentInParent<Health>();

        if (Health == null)
            Debug.LogWarning($"HealthComponent not found for {GetType().Name} on {gameObject.name}", this);
    }

    protected virtual void SubscribeToEvents()
    {
        if (Health == null) 
            return;

        Health.Changed += HandleHealthChanged;
        Health.MaxChanged += HandleMaxHealthChanged;
        Health.Died += HandleDeath;
        Health.Revived += HandleRevive;
    }

    protected virtual void UnsubscribeFromEvents()
    {
        if (Health == null) 
            return;

        Health.Changed -= HandleHealthChanged;
        Health.MaxChanged -= HandleMaxHealthChanged;
        Health.Died -= HandleDeath;
        Health.Revived -= HandleRevive;
    }

    protected virtual void HandleHealthChanged(int current, int max) => 
        UpdateDisplay();

    protected virtual void HandleMaxHealthChanged(int current, int max) => 
        UpdateDisplay();

    protected virtual void HandleDeath() => 
        UpdateDisplay();

    protected virtual void HandleRevive() => 
        UpdateDisplay();

    protected abstract void UpdateDisplay();
}