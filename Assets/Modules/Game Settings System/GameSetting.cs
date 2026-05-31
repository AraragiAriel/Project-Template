using UnityEngine;

public abstract class GameSetting : ScriptableObject
{
    protected GameSettingsData data => Res.data.gameSettingsData;

    public abstract void Load();
    public abstract void Apply();
    public abstract void Save();
}
