using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    [SerializeField] private ToggleButton toggle;
    private bool setDone = false;

    private void OnEnable(){
        toggle.OnToggle += ToggleChange;
    }

    private void OnDisable(){
        toggle.OnToggle -= ToggleChange;        
    }

    private void Start(){
        toggle.Set(Res.data.gameSettings.fullscreen);
        setDone = true;
    }

    private void ToggleChange(bool toggle){
        if(!setDone)
            return;

        Res.data.gameSettings.fullscreen = toggle;
        GameSettingsManager.instance.Apply();
    }
}
