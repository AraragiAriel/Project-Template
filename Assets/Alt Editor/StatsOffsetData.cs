#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Stats Offset Data", menuName = "ScriptableObject/Others/Stats Offset Data")]
public class StatsOffsetData : ScriptableObject
{
    [Resource(ResourceAttribute.Tag.Editor)] public StatsOffsetData offsetData;

    public List<StatOffset> offsets = new();
}

[System.Serializable]
public class StatOffset{
    public Stat stat;
    public float offset;
}

#endif