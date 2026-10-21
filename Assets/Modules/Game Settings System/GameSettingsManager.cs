using System.Collections.Generic;
using UnityEngine;

public static class GameSettingsManager
{
    private static List<GameSetting> settings => Res.data.gameSettings;

    public static bool GetBool(string id, bool fallBack) => PlayerPrefs.GetInt(id, fallBack ? 1 : 0) == 1;

    public static void Initialize()
    {
        Load();
        Apply();
    }

    public static void Load()
    {
        foreach(var setting in settings)
            setting.Load();
    }

    public static void Apply()
    {
        foreach(var setting in settings)
            setting.Apply();

        Save();

        StaticActions.OnGameSettingsChange?.Invoke();
    }

    public static void Save()
    {
        foreach(var setting in settings)
            setting.Save();

        PlayerPrefs.Save();
    }

    public static void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Load();
    }
}
