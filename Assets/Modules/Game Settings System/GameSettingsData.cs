using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsData : ScriptableObject
{
#if UNITY_EDITOR
    public bool toApply = false;
#endif
    
    [Header("Language")]
    public GameLanguage language = 0;

    [Header("Audio Volume")]
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    [Header("Screen Size")]
    public List<Vector2Int> resolutions = new();
    public bool fullscreen = true;
    public int selectedResolution = 0;

    [Header("FPS")]
    public FpsType fpsType;
    public int customFps = 0;
}

public enum GameLanguage{
    English = 0,
    Portuguese = 1,
}

public enum FpsType
{
    Vsync = 0,
    Custom = 1,
    Unlimited = 2,
}