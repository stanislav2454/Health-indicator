using System;

public interface IHealth
{
    public int Current { get; }
    public int Max { get; }
    public bool IsAlive { get; }
    public float Normalized { get; }

    public event Action<int, int> Changed;
    public event Action<int, int> MaxChanged;
    public event Action Died;
    public event Action Revived;
}