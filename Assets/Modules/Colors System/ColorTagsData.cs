using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Tags Data", menuName = "ScriptableObject/Others/ColorTags")]
public class ColorTagsData : ScriptableObject
{
    [Resource(ResourceAttribute.Tag.Data)] public ColorTagsData colorTags;

    public List<ColorFlex> colors = new();

    public Color Get(string tag) => colors.Find(ct => ct.tag == tag).color;

    public string Parse(string s){
        foreach(var tag in colors){
            if(string.IsNullOrEmpty(tag.tag)) continue;

            s = s.Replace($"<{tag.tag}>", $"<color=#{ColorUtility.ToHtmlStringRGBA(tag.color)}>");
            s = s.Replace($"</{tag.tag}>", $"</color>");
        }
        return s;
    }

    private void OnValidate(){
        foreach(var color in colors)
            color.OnValidate();
    }
}
