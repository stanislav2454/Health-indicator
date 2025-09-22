using UnityEngine;
using System.Collections;

public class SmoothBarHealthVisualizer : BarHealthVisualizer
{
    private const float DefaultValue = 0f;

    [Header("Smooth Settings")]
    [SerializeField] private float _smoothSpeed = 2f;

    private float _targetValue;
    private Coroutine _smoothCoroutine;

    protected override void Start()
    {
        base.Start();
        _targetValue = Health != null ? Health.GetNormalized() : DefaultValue;

        if (_healthSlider != null)
            _healthSlider.value = _targetValue;
    }

    protected override void UpdateVisualization()
    {
        if (Health == null)
            return;

        _targetValue = Health.GetNormalized();
        UpdateBarColor(); 

        StartSmoothAnimation();
    }

    private void StartSmoothAnimation()
    {
        if (_smoothCoroutine != null)
        {
            StopCoroutine(_smoothCoroutine);
        }

        _smoothCoroutine = StartCoroutine(SmoothAnimationRoutine());
    }

    private IEnumerator SmoothAnimationRoutine()
    {
        if (_healthSlider == null)
            yield break;

        while (Mathf.Approximately(_healthSlider.value, _targetValue) == false)
        {
            _healthSlider.value = Mathf.MoveTowards(
                _healthSlider.value,
                _targetValue,
                _smoothSpeed * Time.deltaTime);

            yield return null;
        }

        _healthSlider.value = _targetValue;
        _smoothCoroutine = null;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (_smoothCoroutine != null)
        {
            StopCoroutine(_smoothCoroutine);
        }
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