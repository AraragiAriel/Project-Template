using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public static class StringManager
{
    public static string Parse(this string s, StringReplacer replacer = null){
        if(replacer != null)
            s = replacer.Replace(s);
        s = Res.data.localizationData.Parse(s);
        s = Res.data.colorTags.Parse(s);
        return s;
    }

    public static void Set(this TMP_Text tmp, string s, StringReplacer replacer = null)
        => tmp.text = s.Parse(replacer);
        
    public static string RemoveTags(this string s) => Regex.Replace(s, "<.*?>", string.Empty);
}
