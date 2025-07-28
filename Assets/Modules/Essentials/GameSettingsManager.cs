using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager instance;

    public GameSettingsData data;

    public static readonly List<Vector2Int> resolutionsList = new List<Vector2Int>{
        {new Vector2Int(1280,720)},
        {new Vector2Int(1366,768)},
        {new Vector2Int(1920,1080)},
        {new Vector2Int(2560,1440)},
        {new Vector2Int(3840,2160)},
    };

    public static readonly List<string> fpsList = new List<string>{
        {"vsync"},
        {"15"},
        {"30"},
        {"60"},
        {"120"},
        {"240"},
        {"Unlimited"},
    };

    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
            return;
        } else {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        #if UNITY_EDITOR
        if(data.toApply){
            Apply();
            data.toApply = false;
        }
        #endif
        
        Load();
        Apply();
    }

    private void Load(){
        data.selectedFps = PlayerPrefs.GetInt("fps", 0);
        data.fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        data.bgmVolume = PlayerPrefs.GetFloat("bgmVolume", 1f);
        data.sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1f);

        // Resolution
        int storedResolution = PlayerPrefs.GetInt("resolution", 0);
        data.selectedResolution = storedResolution;
        // if(storedResoultion != -1)
        //     // Resolution found in PlayerPrefs
        //     data.selectedResolution = storedResoultion;
        // else {
        //     // Not found, try to match current resolution
        //     data.selectedResolution = 2;
        //     Vector2Int currentRes = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
        //     for(int i = 0; i < resolutionsList.Count; i++){
        //         if(resolutionsList[i] == currentRes){
        //             data.selectedResolution = i;
        //             break;
        //         }
        //     }
        // } 

        // Language
        int storedLanguage =  PlayerPrefs.GetInt("language", -1);
        if(storedLanguage != -1)
            // Language found in PlayerPrefs
            data.language = (GameLanguage)Enum.ToObject(typeof(GameLanguage), storedLanguage);       
        else {
            // Not found, try to match system language
            CultureInfo ci = CultureInfo.CurrentUICulture;
            switch(ci.ThreeLetterWindowsLanguageName){
                case "ENU":
                    data.language = GameLanguage.English;
                    break;
                case "PTB":
                    data.language = GameLanguage.Portuguese;
                    break;
                default:
                    data.language = GameLanguage.English;
                    break;
            }
        }
    }

    private void Save(){
        PlayerPrefs.SetInt("language", (int)data.language);
        PlayerPrefs.SetInt("resolution", data.selectedResolution);
        PlayerPrefs.SetInt("fps", data.selectedFps);
        PlayerPrefs.SetInt("fullscreen", data.fullscreen ? 1 : 0);
        PlayerPrefs.SetFloat("bgmVolume", data.bgmVolume);
        PlayerPrefs.SetFloat("sfxVolume", data.sfxVolume);
    }

    public void Apply(){
        Vector2Int resolution = data.selectedResolution == 0 ? 
            new Vector2Int(Display.main.systemWidth, Display.main.systemHeight) :
            resolutionsList[data.selectedResolution - 1];
        int fps = data.selectedFps;
        QualitySettings.vSyncCount = fps == 0 ? 1 : 0;
        Application.targetFrameRate = fps == 0 || fps == 6 ? -1 : int.Parse(fpsList[fps]);
        Screen.SetResolution(
            resolution.x,
            resolution.y,
            data.fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed
        );

        Save();

        StaticActions.OnGameSettingsChange?.Invoke();
    }
}
