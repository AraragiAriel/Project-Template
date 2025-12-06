using System.Collections.Generic;
using UnityEngine;

public class StringReplacer : MonoBehaviour
{
    private Dictionary<string, string> placeholders = new();
    public void Add(string key, string value){
        if(placeholders.ContainsKey(key))
            placeholders[key] = value;
        else
            placeholders.Add(key, value);
    }

    public string Replace(string s){
        foreach (var kvp in placeholders)
            s = s.Replace("{" + kvp.Key + "}", kvp.Value);
        s = Res.data.colorTags.Parse(s);
        return s;
    }
}
