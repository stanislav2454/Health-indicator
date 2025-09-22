using UnityEngine;
using UnityEngine.UI;

public class HealthTester : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health _health;
    [SerializeField] private Button _damageButton;
    [SerializeField] private Button _healButton;
    [SerializeField] private Button _killButton;
    [SerializeField] private Button _reviveButton;

    [Header("Test Settings")]
    [SerializeField] private int _damageAmount = 10;
    [SerializeField] private int _healAmount = 15;

    private void Start()
    {
        SetupButtons();
    }

    private void OnDestroy()
    {
        RemoveButtonListener(_damageButton, ApplyDamage);
        RemoveButtonListener(_healButton, ApplyHeal);
        RemoveButtonListener(_killButton, Kill);
        RemoveButtonListener(_reviveButton, Revive);
    }

    private void SetupButtons()
    {
        AddButtonListener(_damageButton, ApplyDamage);
        AddButtonListener(_healButton, ApplyHeal);
        AddButtonListener(_killButton, Kill);
        AddButtonListener(_reviveButton, Revive);
    }

    private void AddButtonListener(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button != null)
            button.onClick.AddListener(action);
    }

    private void ApplyDamage() =>
        _health?.TakeDamage(_damageAmount);

    private void ApplyHeal() =>
        _health?.Heal(_healAmount);

    private void Kill() =>
        _health?.Die();

    private void Revive() =>
        _health?.Revive();

    private void RemoveButtonListener(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button != null)
            button.onClick.RemoveListener(action);
    }
}