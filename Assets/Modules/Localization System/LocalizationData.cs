using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Localization Data", menuName = "ScriptableObject/Others/LocalizationData")]
public class LocalizationData : ScriptableObject
{
    [Resource(ResourceAttribute.Tag.Data)] public LocalizationData localizationData;

    public List<ScriptableObject> localizers = new();
    private Dictionary<string, ILocalizer> dict = new();

    public void Populate(){
        dict.Clear();
        foreach(var data in localizers)
            dict.Add(data.name, data as ILocalizer);
    }

    public string Get(string assetName){
        try{
            return dict[assetName].GetLocalizer().Localize();
        } catch {
            return "";
        }
    }
}

public class LocalizeAttribute : Attribute
{
    public string key;
    public LocalizeAttribute(string key){
        this.key = key;
    }
}