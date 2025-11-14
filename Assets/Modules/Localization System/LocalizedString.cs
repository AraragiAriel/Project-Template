using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizedString{
    public List<LanguageString> strings = new();

    public string Localize(){
        try{
            var currentLanguage = Res.data.gameSettings.language;

            string toReturn = null;
            foreach(LanguageString languageString in strings)
                if(languageString.language == currentLanguage){
                    toReturn = languageString.text;
                    break;
                }

            if(!string.IsNullOrEmpty(toReturn))
                return Res.data.colorTags.Parse(toReturn);
            return Res.data.colorTags.Parse(strings[0].text);
        } catch{
            return "";
        }
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