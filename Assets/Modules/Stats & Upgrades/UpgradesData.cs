using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Upgrades Data", menuName = "ScriptableObject/Others/Upgrades Data")]
[System.Serializable]
public class UpgradesData : ScriptableObject
{
    public List<UpgradeData> upgrades = new();

    public int uniqueCount => upgrades.Count;
    public int totalCount{
        get{
            int count = 0;
            foreach(var upgrade in upgrades)
                count += upgrade.maxLevel;
            return count;
        }
    }

    public UpgradeData GetData(string uid){
        foreach(UpgradeData upgrade in upgrades)
            if(upgrade.uid == uid)
                return upgrade;
        return null;
    }
}
