using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private bool bgm;
    [SerializeField] private TextMeshProUGUI tmp;
    private Slider slider;

    private void Awake(){
        slider = GetComponent<Slider>();
    }

    private void OnEnable(){
        slider.onValueChanged.AddListener(SliderChange);
    }

    private void OnDisable(){
        slider.onValueChanged.RemoveListener(SliderChange);        
    }

    private void Start(){
        float value =  bgm ? 
            Res.data.gameSettings.bgmVolume :
            Res.data.gameSettings.sfxVolume;
        slider.value = value;
        SetText(value);
    }

    private void SliderChange(float value){
        value = Util.SetDigits(value, 2, true);
        if(bgm)
            Res.data.gameSettings.bgmVolume = value;
        else
            Res.data.gameSettings.sfxVolume = value;
        GameSettingsManager.instance.Apply();
        SetText(value);
    }

    private void SetText(float value){
        tmp.text = Mathf.RoundToInt(value*100).ToString();
    }
}
