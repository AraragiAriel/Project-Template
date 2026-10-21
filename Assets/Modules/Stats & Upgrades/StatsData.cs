using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObject/Others/Stats Data")]
[System.Serializable]
public class StatsData : ScriptableObject

#if UNITY_EDITOR
, IDataList<Stat>
#endif

{
    public List<Stat> stats = new();
    public List<Stat> displayStats = new();

    public Stat GetStat(string id)
    {
        foreach(Stat stat in stats)
            if(stat.uid == id)
                return stat;
        return null;
    }

    public List<Stat> List() => stats;
}
