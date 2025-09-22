using System;

public interface IHealthProvider
{
    int CurrentHealth { get; }
    int MaxHealth { get; }
    bool IsAlive { get; }

    event Action<int, int> HealthChanged;
    event Action<int, int> MaxHealthChanged;
    event Action Died;
    event Action Revived;
}