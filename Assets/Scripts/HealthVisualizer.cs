using UnityEngine;

public abstract class HealthVisualizer : MonoBehaviour
{
    [Header("Health Reference")]
    [SerializeField] protected Health Health;

    protected virtual void Start()
    {
        TryFindHealthComponent();
        SubscribeToEvents();
        UpdateVisualization();
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

        Health.HealthChanged += HandleHealthChanged;
        Health.MaxHealthChanged += HandleMaxHealthChanged;
        Health.Died += HandleDeath;
        Health.Revived += HandleRevive;
    }

    protected virtual void UnsubscribeFromEvents()
    {
        if (Health == null) 
            return;

        Health.HealthChanged -= HandleHealthChanged;
        Health.MaxHealthChanged -= HandleMaxHealthChanged;
        Health.Died -= HandleDeath;
        Health.Revived -= HandleRevive;
    }

    protected virtual void HandleHealthChanged(int current, int max) => 
        UpdateVisualization();

    protected virtual void HandleMaxHealthChanged(int current, int max) => 
        UpdateVisualization();

    protected virtual void HandleDeath() => 
        UpdateVisualization();

    protected virtual void HandleRevive() => 
        UpdateVisualization();

    protected abstract void UpdateVisualization();
}