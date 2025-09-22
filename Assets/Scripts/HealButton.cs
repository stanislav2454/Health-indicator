using UnityEngine;

public class HealButton : HealthButton
{
    [SerializeField] private int _amount = 15;

    protected override void OnClick()
    {
        TargetHealth?.Heal(_amount);
    }
}