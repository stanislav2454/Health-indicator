using System.Collections.Generic;
using UnityEngine;

public class HealthTester : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health _health;
    [SerializeField] private List<HealthButton> _healthButtons = new List<HealthButton>();

    [Header("Test Settings")]
    [SerializeField] private int _damageAmount = 10;
    [SerializeField] private int _healAmount = 15;

    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        foreach (var button in _healthButtons)
        {
            if (button != null)
                button.Initialize(_health);
        }
    }

    public void RegisterButton(HealthButton button)
    {
        if (button != null && !_healthButtons.Contains(button))
        {
            _healthButtons.Add(button);
            button.Initialize(_health);
        }
    }

    public void UnregisterButton(HealthButton button)
    {
        if (button != null && _healthButtons.Contains(button))
            _healthButtons.Remove(button);
    }

    public void SetTargetHealth(Health newTarget)
    {
        _health = newTarget;

        foreach (var button in _healthButtons)
        {
            if (button != null)
                button.Initialize(newTarget);
        }
    }
}