using UnityEngine;
using TMPro;

public class LocalizedStringSetter : MonoBehaviour
{
    [SerializeField] private LocalizedStringData s;

    private void OnEnable(){
        StaticActions.OnGameSettingsChange += GameSettingsChange;
    }

    private void OnDisable(){
        StaticActions.OnGameSettingsChange -= GameSettingsChange;        
    }

    private void Start(){
        Set();
    }

    private void Set(){
        if(s == null)
            return;

        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        if(tmp == null)
            tmp = GetComponentInChildren<TextMeshProUGUI>();
        if(tmp != null)
            tmp.Set(s);
    }

    private void GameSettingsChange(){
        Set();
    }
}
