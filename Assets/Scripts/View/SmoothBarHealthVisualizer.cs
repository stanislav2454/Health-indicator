using UnityEngine;
using System.Collections;

public class SmoothBarHealthVisualizer : BarHealthVisualizer
{
    private const float DefaultValue = 0f;

    [SerializeField] private float _smoothSpeed = 0.2f;

    private float _targetValue;
    private Coroutine _smoothCoroutine;

    protected override void Start()
    {
        base.Start();
        _targetValue = Health?.Normalized ?? DefaultValue;

        if (_slider != null)
            _slider.value = _targetValue;
    }

    protected override void UpdateDisplay()
    {
        if (Health == null)
            return;

        _targetValue = Health.Normalized;
        UpdateColor();

        StartSmoothAnimation();
    }

    private void StartSmoothAnimation()
    {
        if (_smoothCoroutine != null)
            StopCoroutine(_smoothCoroutine);

        _smoothCoroutine = StartCoroutine(SmoothAnimationRoutine());
    }

    private IEnumerator SmoothAnimationRoutine()
    {
        if (_slider == null)
            yield break;

        while (Mathf.Approximately(_slider.value, _targetValue) == false)
        {
            _slider.value = Mathf.MoveTowards(
                _slider.value,
                _targetValue,
                _smoothSpeed * Time.deltaTime);

            yield return null;
        }

        _slider.value = _targetValue;
        _smoothCoroutine = null;
    }

    protected override void OnDestroy()
    {
        if (_smoothCoroutine != null)
            StopCoroutine(_smoothCoroutine);

        base.OnDestroy();
    }

    private void OnDisable()
    {
        if (_smoothCoroutine != null)
        {
            StopCoroutine(_smoothCoroutine);
            _smoothCoroutine = null;
        }
    }
}