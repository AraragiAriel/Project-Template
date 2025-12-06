using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObject/Others/Stats Data")]
[System.Serializable]
public class StatsData : ScriptableObject
{
    [Resource(ResourceAttribute.Tag.Data)] public StatsData statsData;

    public List<Stat> stats = new();

    public Stat GetStat(string id){
        foreach(Stat stat in stats)
            if(stat.uid == id)
                return stat;
        return null;
    }
}
