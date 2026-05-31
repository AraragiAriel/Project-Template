using UnityEngine;

[RequireComponent(typeof(MySlider))]
public class FpsSlider : MonoBehaviour
{
    private MySlider _slider;
    private MySlider slider => _slider ??= GetComponent<MySlider>();

    private void OnEnable()
    {
        slider.OnValueChange += SliderChange;
    }

    private void OnDisable()
    {
        slider.OnValueChange -= SliderChange;   
    }

    private void Start()
    {
        slider.value = Res.data.gameSettingsData.customFps;
    }

    private void SliderChange(float value)
    {
        Res.data.gameSettingsData.customFps = Mathf.RoundToInt(value);
        GameSettingsManager.Apply();
    }
}
