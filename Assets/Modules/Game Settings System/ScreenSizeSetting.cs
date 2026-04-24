using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeSetting : GameSetting
{
    public override void Load()
    {
        data.fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        data.selectedResolution = PlayerPrefs.GetInt("resolution", -1);
    }

    public override void Apply()
    {
        Vector2Int resolution = data.selectedResolution == -1 ? 
            new Vector2Int(Display.main.systemWidth, Display.main.systemHeight) :
            data.resolutions[data.selectedResolution - 1];
            
        Screen.SetResolution(
            resolution.x,
            resolution.y,
            data.fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed
        );
    }

    public override void Save()
    {
        PlayerPrefs.SetInt("fullscreen", data.fullscreen ? 1 : 0);
        PlayerPrefs.SetInt("resolution", data.selectedResolution);
    }
}
