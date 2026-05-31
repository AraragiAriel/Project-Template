using System;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "Language Setting", menuName = "ScriptableObject/Game Settings/Language")]
public class LanguageSetting : GameSetting
{
    private const string language = "language";

    public override void Load()
    {        
        if(PlayerPrefs.HasKey(language))
        {
            // Language present in PlayerPrefs
            int id = PlayerPrefs.GetInt(language, 0);
            data.language = (GameLanguage)id;
        }
        else
        {
            // Not present, try to match system language
            CultureInfo ci = CultureInfo.CurrentUICulture;
            switch(ci.ThreeLetterWindowsLanguageName)
            {
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
        PlayerPrefs.SetInt(language, (int)data.language);
    }
}
