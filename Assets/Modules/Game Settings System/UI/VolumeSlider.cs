using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(MySlider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private bool bgm;

    private MySlider _slider;
    private MySlider slider => _slider ??= GetComponent<MySlider>();

    private void OnEnable()
    {
        float value =  bgm ? 
            Res.data.gameSettingsData.bgmVolume :
            Res.data.gameSettingsData.sfxVolume;
        slider.value = value;

        slider.OnValueChange += SliderChange;
    }

    private void OnDisable()
    {
        slider.OnValueChange -= SliderChange;   
    }

    private void SliderChange(float value)
    {
        if(bgm)
            Res.data.gameSettingsData.bgmVolume = value;
        else
            Res.data.gameSettingsData.sfxVolume = value;
        GameSettingsManager.Apply();
    }
}
