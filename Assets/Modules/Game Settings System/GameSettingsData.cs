using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameSettingsData : ScriptableObject
{    
    [Header("Language")]
    public GameLanguage language = GameLanguage.English;

    [Header("Audio Volume")]
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    [Header("Screen Size")]
    public bool fullscreen = true;
    public bool nativeResolution;
    public ScreenSize selectedScreenSize = 0;

    [Header("FPS")]
    public FpsType fpsType;
    public int customFps = 60;
}

public enum GameLanguage
{
    English = 0,
    Portuguese = 1,
}

public enum FpsType
{
    [Localize("Vsync")] Vsync = 0,
    [Localize("Custom")] Custom = 1,
    [Localize("Unlimited")] Unlimited = 2,
}

public enum ScreenSize
{
    [ScreenSizeVector(1280, 720)] _1280x720 = 0,
    [ScreenSizeVector(1366, 768)] _1366x768 = 10,
    [ScreenSizeVector(1920, 1080)] _1920x1080 = 20,
    [ScreenSizeVector(2560, 1440)] _2560x1440 = 30,
    [ScreenSizeVector(3840, 2160)] _3840x2160 = 40,
}

public class ScreenSizeVectorAttribute : Attribute
{
    public Vector2Int vector;

    public ScreenSizeVectorAttribute(int x, int y)
    {
        vector = new(x, y);
    }
}

public static class ScreenSizeVectorAttributeExtension
{
    public static Vector2Int ToVector(this ScreenSize screenSize)
    {
        var member = screenSize.GetType().GetMember(screenSize.ToString())[0];
        var attr = member.GetCustomAttribute<ScreenSizeVectorAttribute>();
        return attr != null ? attr.vector : new Vector2Int(1920, 1080);
    }
}