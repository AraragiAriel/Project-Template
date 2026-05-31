using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GameSettingsManager
{
    private static List<GameSetting> settings => Res.data.gameSettings;

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
}
