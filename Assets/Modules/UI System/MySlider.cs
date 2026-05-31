using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MySlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Vector2 range;
    [SerializeField] private bool percent;

    public Action<float> OnValueChange;

    private Slider _slider;
    private Slider slider => _slider ??= GetComponent<Slider>();

    public float value
    {
        get => slider.value;
        set => slider.value = value;
    }

    void OnEnable()
    {
        slider.onValueChanged.AddListener(SliderChange);
    }

    void OnDisable()
    {
        slider.onValueChanged.RemoveListener(SliderChange);
    }

    private void SliderChange(float value)
    {
        value = Mathf.Lerp(range.x, range.y, value);
        tmp.Set(percent ? Util.FormatPercent(value, false) : Util.Concat(value, false));
        OnValueChange?.Invoke(value);
    }
}
