using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObject/Upgrade Data")]
[System.Serializable]
public class UpgradeData : ScriptableObject
{
    public UID uid;

    [Space]

    [Header("Display")]
    public Sprite icon;
    public Sprite maxedIcon;
    public Sprite iconToUse{
        get{
            if(maxedOut && maxedIcon != null)
                return maxedIcon;
            return icon;
        }
    }
    public LocalizedString upgradeName;
    public LocalizedString description;
    public bool special = false;

    [Space]

    [Header("Data")]
    public int maxLevel = 10;
    public CurrencyAmount initialCost = new CurrencyAmount(Currency.Null, 0);
    public int costIncrease;
    public List<Requirement> requirements = new();


    [Space]

    [Header("Stat")]
    public Stat stat;
    public float valuePerLevel;
    public ValueMod.Type valueModType;

    
    [Space]
    
    [Header("Exe")]
    public GameObject exe;
    
    public bool maxedOut{
        get {return level >= maxLevel;}
    }
    
    public CurrencyAmount cost => Cost(level);
    public CurrencyAmount Cost(int level){
        float multiplier = 1f;
        try{
            Stat discount = Res.data.statsData.GetStat("33a7850f010b6f74fa84ca1ce3e94954");
            multiplier = 1f - discount;
        } catch {}
        if(level >= maxLevel)
            return new CurrencyAmount(Currency.Null, 0);
        else{
            CurrencyAmount c = initialCost;
            c.amount += costIncrease*level;
            c.amount *= multiplier;
            if(!Mathf.Approximately(c.amount, Mathf.Round(c.amount)))
                c.amount = Mathf.Floor(c.amount);
            return c;
        }
    }
    public CurrencyAmount CumulativeCost(int level){
        int cost = 0;
        for(int i = 0; i < level; i++)
            cost += Mathf.RoundToInt(Cost(i).amount);
        return new CurrencyAmount(initialCost.type, cost);
    }

    public int level => Res.save.upgradeSave.GetLevel(uid);

    private void OnValidate(){
        if(maxLevel <= 0)
            maxLevel = 1;
    }

    public bool MeetRequirements(){
        foreach(Requirement requirement in requirements)
            if(Res.save.upgradeSave.GetLevel(requirement.upgrade.uid) < requirement.levelReq)
                return false;

        return true;
    }
}
