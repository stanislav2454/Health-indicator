using UnityEngine;

public class DamageButton : HealthButton
{
    [SerializeField] private int _amount = 10;

    protected override void OnClick()
    {
        TargetHealth?.TakeDamage(_amount);
    }
}