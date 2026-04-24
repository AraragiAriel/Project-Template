using System;
using System.Globalization;
using UnityEngine;

public class LanguageSetting : GameSetting
{
    public override void Load()
    {        
        int storedLanguage =  PlayerPrefs.GetInt("language", -1);
        if(storedLanguage != -1)
            // Language found in PlayerPrefs
            data.language = (GameLanguage)Enum.ToObject(typeof(GameLanguage), storedLanguage);       
        else
        {
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

    public override void Apply()
    {
    }

    public override void Save()
    {
        PlayerPrefs.SetInt("language", (int)data.language);
    }
}
