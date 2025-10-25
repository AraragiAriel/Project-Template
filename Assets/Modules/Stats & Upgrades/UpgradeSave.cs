using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeSave
{
    public int uniqueUpgrades => upgrades.Count;
    public int totalUpgrades{
        get{
            int count = 0;
            foreach(var upgrade in upgrades)
                count += upgrade.level;
            return count;
        }
    }

    public List<SingleUpgradeSave> upgrades = new();

    public int GetLevel(string uid){
        foreach(SingleUpgradeSave upgradeSave in upgrades)
            if(upgradeSave.uid == uid)
                return upgradeSave.level;
        return 0;
    }

    public void AddLevel(string uid){
        foreach(SingleUpgradeSave upgradeSave in upgrades)
            if(upgradeSave.uid == uid){
                upgradeSave.level++;
                return;
            }
        upgrades.Add(new SingleUpgradeSave(uid, 1));
    }

    public void ClearRemoved(){
        UpgradesData upgradesData = Res.data.upgradesData;

        for(int i = upgrades.Count - 1; i >= 0; i--){
            if(upgradesData.GetData(upgrades[i].uid) == null)
                upgrades.RemoveAt(i);
        }
    }

    public void ConcatLimits(){
        UpgradesData upgradesData = Res.data.upgradesData;

        foreach(SingleUpgradeSave upgradeSave in upgrades)
            upgradeSave.level = Mathf.Min(
                upgradeSave.level,
                upgradesData.GetData(upgradeSave.uid).maxLevel
            );        
    }
}

[System.Serializable]
public class SingleUpgradeSave
{
    public string uid;
    public int level;

    public SingleUpgradeSave(string uid, int level){
        this.uid = uid;
        this.level = level;
    }
}
