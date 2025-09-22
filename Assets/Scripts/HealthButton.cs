using UnityEngine;
using UnityEngine.UI;

public abstract class HealthButton : MonoBehaviour
{
    [SerializeField] protected Health TargetHealth;
    [SerializeField] protected Button Button;

    protected virtual void Start()
    {
        if (Button != null)
            Button.onClick.AddListener(OnClick);
    }

    protected virtual void OnDestroy()
    {
        if (Button != null)
            Button.onClick.RemoveListener(OnClick);
    }

    protected abstract void OnClick();

    public virtual void Initialize(Health targetHealth)
    {
        TargetHealth = targetHealth;
    }
}