using UnityEngine;

public abstract class GameSetting : MonoBehaviour
{
    protected GameSettingsData data => Res.data.gameSettings;

    public abstract void Load();
    public abstract void Apply();
    public abstract void Save();
}
