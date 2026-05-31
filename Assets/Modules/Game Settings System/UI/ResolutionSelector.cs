using UnityEngine;
using System.Collections.Generic;

public class ResolutionSelector : MonoBehaviour
{
    private bool setDone = false;

    private Selector _selector;
    private Selector selector => _selector ??= GetComponent<Selector>();

    private List<ScreenSize> screenSizes => Util.EnumList<ScreenSize>();

    private void Awake()
    {
        selector.OnIdChange += SwitchChange;
    }

    private void OnDestroy()
    {
        selector.OnIdChange -= SwitchChange; 
    }

    private void Start()
    {
        List<LocalizedString> resolutions = new();
        foreach(ScreenSize screenSize in screenSizes)
        {
            Vector2Int vector = screenSize.ToVector(); 
            resolutions.Add(vector.x + "x" + vector.y);
        }
        
        selector.Set(resolutions, screenSizes.IndexOf(Res.data.gameSettingsData.selectedScreenSize));
        setDone = true;
    }

    private void SwitchChange(int id){
        if(!setDone)
            return;
            
        Res.data.gameSettingsData.selectedScreenSize = screenSizes[id];
        GameSettingsManager.Apply();
    }
}
