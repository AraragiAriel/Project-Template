using UnityEngine;
using TMPro;

public class Localizer : MonoBehaviour
{
    [SerializeField] private LocalizedStringData data;

    private void OnEnable(){
        StaticActions.OnGameSettingsChange += GameSettingsChange;
    }

    private void OnDisable(){
        StaticActions.OnGameSettingsChange -= GameSettingsChange;        
    }

    private void Start(){
        Set();
    }
    
    private void GameSettingsChange() => Set();

    private void Set(){
        if(data == null)
            return;

        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        if(tmp == null)
            tmp = GetComponentInChildren<TextMeshProUGUI>();
        if(tmp != null)
            tmp.text = data;        
    }
}
