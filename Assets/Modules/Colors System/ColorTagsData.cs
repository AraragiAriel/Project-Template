using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// [CreateAssetMenu(fileName = "Color Tags Data", menuName = "ScriptableObject/Others/ColorTags")]
public class ColorTagsData : ScriptableObject
{
    public List<ScriptableObject> colors = new();
    private Dictionary<string, IColor> dict = new();

    public void Populate(){
        dict.Clear();
        foreach(var data in colors){
            var color = data as IColor;
            if(color != null && !string.IsNullOrEmpty(color.GetColor().tag))
                dict.Add(color.GetColor().tag, color);
        }
    }

    public Color Get(string tag) => dict[tag].GetColor();

    public string Parse(string s){
        foreach(var kvp in dict){
            s = s.Replace($"<{kvp.Key}>", $"<color=#{ColorUtility.ToHtmlStringRGBA(kvp.Value.GetColor())}>");
            s = s.Replace($"</{kvp.Key}>", $"</color>");
        }
        return s;
    }
}

public class ColorAttribute : Attribute
{
    public string key;
    
    public ColorAttribute(string key){
        this.key = key;
    }
}

public static class ColorAttributeExtension
{
    public static Color GetColor(this Enum value){
        var member = value.GetType().GetMember(value.ToString())[0];
        var attr = member.GetCustomAttribute<ColorAttribute>();
        return attr != null ? Res.data.colorTags.Get(attr.key) : Color.white;
    }
}
