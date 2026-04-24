#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

public class StatsOffsetData : ScriptableObject
{
    public List<StatOffset> offsets = new();
}

[System.Serializable]
public class StatOffset{
    public Stat stat;
    public float offset;
}

#endif