public class KillButton : HealthButton
{
    protected override void OnClick()
    {
        TargetHealth?.Die();
    }
}