using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizedString
{
    public List<LanguageString> strings = new();

    public string Localize()
    {
        try
        {
            var currentLanguage = Res.data.gameSettingsData.language;

            string toReturn = null;
            foreach(LanguageString languageString in strings)
                if(languageString.language == currentLanguage)
                {
                    toReturn = languageString.text;
                    break;
                }

            if(!string.IsNullOrEmpty(toReturn))
                return toReturn;
            return strings[0].text;
        }
        catch
        {
            return "";
        }
    }

    public static implicit operator string(LocalizedString ls) =>
        ls != null ? ls.Localize() : "";

    public static implicit operator LocalizedString(string s)
    {
        var ls = new LocalizedString();
        ls.strings[0].text = s;
            
        return ls;
    }

    public LocalizedString()
    {
        strings = new();
        foreach(var language in Util.EnumList<GameLanguage>())
            strings.Add(new(language));
    }
}

[System.Serializable]
public class LanguageString
{
    public GameLanguage language;
    [TextArea(3, 15)]
    public string text;

    public LanguageString(GameLanguage language)
    {
        this.language = language;
    }
}