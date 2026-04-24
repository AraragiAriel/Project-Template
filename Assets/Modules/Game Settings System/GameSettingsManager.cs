using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager instance;

    private GameSettingsData data => Res.data.gameSettings;
    private List<GameSetting> settings = new();

    private void Awake(){
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        settings = GetComponentsInChildren<GameSetting>().ToList();

#if UNITY_EDITOR
        if(data.toApply)
        {
            Apply();
            data.toApply = false;
        }
#endif
        
        Load();
        Apply();
    }

    private void Load()
    {
        foreach(var setting in settings)
            setting.Load();
    }

    public void Apply()
    {
        foreach(var setting in settings)
            setting.Apply();

        Save();

        StaticActions.OnGameSettingsChange?.Invoke();
    }

    private void Save()
    {
        foreach(var setting in settings)
            setting.Save();

        PlayerPrefs.Save();
    }
}
