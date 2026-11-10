using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObject/Others/Stats Data")]
public class StatsData : ScriptableObject
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
}
