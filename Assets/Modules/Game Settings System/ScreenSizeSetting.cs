using System;
using Steamworks.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "Screen Size Setting", menuName = "ScriptableObject/Game Settings/Screen Size")]
public class ScreenSizeSetting : GameSetting
{
    private const string fullscreen = "fullscreen";
    private const string native = "native";
    private const string resolution = "resolution";

    public override void Load()
    {
        data.fullscreen = PlayerPrefs.GetInt(fullscreen, 1) == 1;
        data.nativeResolution = PlayerPrefs.GetInt(native, 1) == 1;
        if (PlayerPrefs.HasKey(resolution))
        {
            // resolution present in player prefs
            data.selectedScreenSize = (ScreenSize)PlayerPrefs.GetInt(resolution, 0);
        }
        else
        {
            // resolution not present, try to match native
            data.selectedScreenSize = ScreenSize._1920x1080;
            int heightToMatch = Display.main.systemHeight;
            foreach(var screenSize in Util.EnumList<ScreenSize>())
                if(screenSize.ToVector().y == heightToMatch)
                {
                    data.selectedScreenSize = screenSize;
                    break;
                }
        }
    }

    public override void Apply()
    {
        Vector2Int resolution = data.nativeResolution ? 
            new Vector2Int(Display.main.systemWidth, Display.main.systemHeight) :
            data.selectedScreenSize.ToVector();
            
        Screen.SetResolution(
            resolution.x,
            resolution.y,
            data.fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed
        );
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(fullscreen, data.fullscreen ? 1 : 0);
        PlayerPrefs.SetInt(native, data.nativeResolution ? 1 : 0);
        PlayerPrefs.SetInt(resolution, (int)data.selectedScreenSize);
    }
}
