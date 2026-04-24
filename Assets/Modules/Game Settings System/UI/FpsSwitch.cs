using UnityEngine;
using System.Collections.Generic;

public class FpsSwitch : MonoBehaviour
{
    [SerializeField] private Selector selector;
    [SerializeField] private List<LocalizedStringData> strings = new();
    private bool setDone = false;

    private void OnEnable(){
        selector.OnIdChange += SwitchChange;
    }

    private void OnDisable(){
        selector.OnIdChange -= SwitchChange;        
    }

    private void Start(){
        List<LocalizedString> aux = new();
        foreach(LocalizedStringData data in strings)
            aux.Add(data.localizedString);
        
        selector.Set(aux, (int)Res.data.gameSettings.fpsType);
        setDone = true;
    }

    private void SwitchChange(int id){
        if(!setDone)
            return;
            
        Res.data.gameSettings.fpsType = (FpsType)id;
        GameSettingsManager.instance.Apply();
    }
}
