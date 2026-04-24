using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "GameSettingsData", menuName = "ScriptableObject/Others/GameSettingsData")]
public class GameSettingsData : ScriptableObject
{
    [Resource(ResourceAttribute.Tag.Options)] public GameSettingsData gameSettings;

    #if UNITY_EDITOR
    public bool toApply = false;
    #endif
    
    public GameLanguage language = 0;
    public int selectedResolution = 0;
    public int selectedFps = 0;
    public bool fullscreen = true;
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
}

public enum GameLanguage{
    English = 0,
    Portuguese = 1,
}