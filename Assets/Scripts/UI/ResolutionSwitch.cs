using UnityEngine;
using System.Collections.Generic;

public class ResolutionSwitch : MonoBehaviour
{
    [SerializeField] private Selector selector;
    [SerializeField] private LocalizedStringData adaptString;
    private bool setDone = false;

    private void OnEnable(){
        selector.OnIdChange += SwitchChange;
    }

    private void OnDisable(){
        selector.OnIdChange -= SwitchChange;        
    }

    private void Start(){
        List<LocalizedString> resolutions = new();
        resolutions.Add(adaptString.localizedString);
        foreach(Vector2Int resolution in GameSettingsManager.resolutionsList)
            resolutions.Add(resolution.x + "X" + resolution.y);
        
        selector.Set(resolutions, Res.data.gameSettings.selectedResolution);
        setDone = true;
    }

    private void SwitchChange(int id){
        if(!setDone)
            return;
            
        Res.data.gameSettings.selectedResolution = id;
        GameSettingsManager.instance.Apply();
    }
}
