using TMPro;
using UnityEngine;

public class TextHealthVisualizer : HealthVisualizer
{
    [Header("Text Settings")]
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _format = "{0}/{1}";
    [SerializeField] private bool _showMaxHealth = true;
    [SerializeField] private Color _normalHealthColor = Color.white;
    [SerializeField] private Color _lowHealthColor = Color.red;
    [SerializeField] private float _lowHealthThreshold = 0.3f;

    protected override void UpdateDisplay()
    {
        if (_text == null || Health == null)
            return;

        _text.text = _showMaxHealth
            ? string.Format(_format, Health.Current, Health.Max) : Health.Current.ToString();

        _text.color = Health.Normalized <= _lowHealthThreshold ? _lowHealthColor : _normalHealthColor;
    }
}