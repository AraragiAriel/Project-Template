using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

// [CreateAssetMenu(fileName = "Localization Data", menuName = "ScriptableObject/Others/LocalizationData")]
public class LocalizationData : ScriptableObject
{
    public List<ScriptableObject> localizers = new();
    private Dictionary<string, ILocalizer> dict = new();

    public void Populate(){
        dict.Clear();
        foreach(var data in localizers)
            dict.Add(data.name, data as ILocalizer);
    }

    public string Get(string assetName, string field = ""){
        try{
            return dict[assetName].Localize(field);
        } catch {
            return "";
        }
    }

    private static readonly Regex LocTag = new(
        @"<loc\s*=\s*(?<key>[^,>]+)(?:\s*,\s*field\s*=\s*(?<field>[^>]+))?>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    public string Parse(string s){
        return LocTag.Replace(s, match =>
        {
            var key = match.Groups["key"].Value.Trim();
            var field = match.Groups["field"].Success
                ? match.Groups["field"].Value.Trim()
                : "";

            return Get(key, field);
        });
    }
}

public class LocalizeAttribute : Attribute
{
    public string key;
    public string field;
    
    public LocalizeAttribute(string key){
        this.key = key;
    }
    
    public LocalizeAttribute(string key, string field){
        this.key = key;
        this.field = field;
    }
}

public static class LocalizeAttributeExtension
{
    public static string Localize(this Enum value){
        var member = value.GetType().GetMember(value.ToString())[0];
        var attr = member.GetCustomAttribute<LocalizeAttribute>();
        return attr != null ? Res.data.localizationData.Get(attr.key, attr.field) : value.ToString();
    }
}