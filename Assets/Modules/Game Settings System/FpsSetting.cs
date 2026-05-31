using UnityEngine;

[CreateAssetMenu(fileName = "FPS Setting", menuName = "ScriptableObject/Game Settings/FPS")]
public class FpsSetting : GameSetting
{
    private const string fpsType = "fpsType";
    private const string customFps = "customFps";

    public override void Load()
    {
        data.fpsType = (FpsType)PlayerPrefs.GetInt(fpsType, 0);
        data.customFps = PlayerPrefs.GetInt(customFps, 60);
    }

    public override void Apply()
    {
        switch (data.fpsType)
        {
            case FpsType.Vsync:
                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = -1;
                break;
            case FpsType.Custom:
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = data.customFps;
                break;
            case FpsType.Unlimited:
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = -1;
                break;
        }
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(fpsType, (int)data.fpsType);
        PlayerPrefs.SetInt(customFps, data.customFps);
    }
}
