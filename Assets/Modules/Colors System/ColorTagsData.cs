using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

// [CreateAssetMenu(fileName = "Color Tags Data", menuName = "ScriptableObject/Others/ColorTags")]
public class ColorTagsData : ScriptableObject
{
    public List<ScriptableObject> colors = new();
    public Dictionary<string, IColor> dict {get; private set;} = new();

    public void Populate()
    {
        dict.Clear();
        foreach(var data in colors)
        {
            var color = data as IColor;
            if(color != null && !string.IsNullOrEmpty(color.GetColor().tag))
            {
                var tag = color.GetColor().tag;
                if(dict.ContainsKey(tag))
                    Util.Debug($"color tag already added: object - {data.name} / tag - {tag}");
                dict[tag] = color;
            }
        }
    }

    public Color Get(string tag) => dict[tag].GetColor();
    
    public Color GetLight()
    {
        try
        {
            return Get("light");
        }
        catch
        {
            return Color.white;
        }
    }
    
    public Color GetDark()
    {
        try
        {
            return Get("dark");
        }
        catch
        {
            return Color.black;
        }
    }

    public Color GetContrast(Color color) => color.IsLight() ? GetDark() : GetLight();

    private static readonly Regex MyColorTag = new
    (
        @"<myColor\s*=\s*(?<key>[^>]+?)\s*>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    public string Parse(string s)
    {
        s = MyColorTag.Replace(s, match =>
        {
            var key = match.Groups["key"].Value.Trim();

            return $"<color=#{ColorUtility.ToHtmlStringRGBA(Get(key))}>";
        });
        s = s.Replace($"</myColor>", $"</color>");
        return s;
    }
}

public class ColorAttribute : Attribute
{
    public string key;
    
    public ColorAttribute(string key)
    {
        this.key = key;
    }
}

public static class ColorAttributeExtension
{
    public static Color GetColor(this Enum value)
    {
        var enumData = Res.data.enumsData.Get<EnumData>(value);
        if(enumData != null)
            return enumData.GetColor();

        var member = value.GetType().GetMember(value.ToString())[0];
        var attr = member.GetCustomAttribute<ColorAttribute>();
        return attr != null ? Res.data.colorTags.Get(attr.key) : Color.white;
    }
}
