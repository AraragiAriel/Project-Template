using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizedString{
    public List<LanguageString> strings = new();

    public string Localize(){
        GameLanguage currentLanguage = ResourcesSystem.data.gameSettings.language;

        foreach(LanguageString languageString in strings)
            if(languageString.language == currentLanguage)
                return languageString.text.ToUpperInvariant();

        Debug.LogWarning("String not found");
        if(strings[0] != null)
            return strings[0].text;
        return "";
    }

    public static implicit operator string(LocalizedString ls) =>
        ls != null ? ls.Localize() : "";   

    public static implicit operator LocalizedString(string s){
        LocalizedString ls = new LocalizedString();
        foreach(LanguageString languageString in ls.strings)
            languageString.text = s;
        return ls;
    }

    public LocalizedString(){
        strings = new List<LanguageString>{
            new LanguageString(GameLanguage.English),
            new LanguageString(GameLanguage.Portuguese)
        };
    }
}

[System.Serializable]
public class LanguageString{
    public GameLanguage language;
    [TextArea(3, 15)]
    public string text;

    public LanguageString(GameLanguage language){
        this.language = language;
    }
}