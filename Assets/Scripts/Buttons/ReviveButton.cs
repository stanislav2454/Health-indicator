using UnityEngine;

public class ReviveButton : HealthButton
{
    [SerializeField] private int _healthAmount = 0;

    protected override void OnClick()
    {
        TargetHealth?.Revive(_healthAmount);
    }
}