using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Currency{
    Null = 0,
    Drop = 1,
    C_oin = 2,
    En = 3,
    Nuc = 4,
}

[System.Serializable]
public struct CurrencyAmount{
    public Currency type;
    public float amount;

    public CurrencyAmount(Currency currency = Currency.Null, float amount = 0f){
        this.type = currency;
        this.amount = amount;
    }

    public CurrencyData GetData(){
        switch(type){
            case Currency.Drop:
                return ResourcesSystem.data.currenciesData[0];
            case Currency.C_oin:
                return ResourcesSystem.data.currenciesData[1];
            case Currency.En:
                return ResourcesSystem.data.currenciesData[2];
            case Currency.Nuc:
                return ResourcesSystem.data.currenciesData[3];
            default:
                return ResourcesSystem.data.currenciesData[0];
        }
    }
}

public static class CurrencyManager
{   
    public static void AddCurrency(CurrencyAmount c){
        ResourcesSystem.save.currencySave.AddCurrency(c);
        if(!ResourcesSystem.save.currencySave.unlocked.Contains(c.type))
            ResourcesSystem.save.currencySave.unlocked.Add(c.type);
        StaticActions.OnCurrencyChange?.Invoke(GetCurrency(c.type), c);
    }

    public static bool SpendCurrency(CurrencyAmount c){
        bool spent = ResourcesSystem.save.currencySave.SpendCurrency(c);
        if(spent){
            c.amount = -c.amount;
            StaticActions.OnCurrencyChange?.Invoke(GetCurrency(c.type), c);
        }
        return spent;
    }

    public static CurrencyAmount GetCurrency(Currency c){
        return ResourcesSystem.save.currencySave.GetCurrency(c);
    }

    public static bool HasEnoughCurrency(CurrencyAmount c){
        return ResourcesSystem.save.currencySave.HasEnoughCurrency(c);
    }

    public static CurrencyData GetData(Currency c){
        return new CurrencyAmount(c, 0f).GetData();
    }
}
