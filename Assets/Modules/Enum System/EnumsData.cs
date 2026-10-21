using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Enums Data", menuName = "ScriptableObject/Enum/Enums Data")]
public class EnumsData : ScriptableObject
{
    public List<EnumData> datas = new();
    private Dictionary<(Type, Enum), EnumData> dict = new();

    public TData Get<TData>(Enum toFind) where TData : EnumData
    {
        if(dict.TryGetValue((toFind.GetType(), toFind), out var data))
            return data as TData;
            
        return null;
    }

    public void Populate()
    {
        dict.Clear();
        foreach(var data in datas)
            dict[(data.Value.GetType(), data.Value)] = data;
    }
}

public static class EnumExtension
{
    public static TData Data<TData>(this Enum value) where TData : EnumData
        => Res.data.enumsData.Get<TData>(value);
}
