using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrencySave
{
    public CurrencySave(){
        currencies = new();

        var enums = Enum.GetValues(typeof(Currency));
        foreach(var e in enums){
            if((Currency)e == Currency.Null)
                continue;
            currencies.Add(new CurrencyAmount((Currency)e, 0f));
        }
    }

    public List<CurrencyAmount> currencies = new();
    public List<Currency> unlocked = new();

    public void AddCurrency(CurrencyAmount c){
        for(int i = 0; i < currencies.Count; i++){
            if(currencies[i].type == c.type){
                currencies[i] = new CurrencyAmount(c.type, currencies[i].amount + c.amount);
                return;
            }
        }
        currencies.Add(c);
    }

    public bool SpendCurrency(CurrencyAmount c){
        if(!HasEnoughCurrency(c))
            return false;

        for(int i = 0; i < currencies.Count; i++){
            if(currencies[i].type == c.type){
                currencies[i] = new CurrencyAmount(c.type, currencies[i].amount - c.amount);
                return true;
            }
        }     
        return true;
    }

    public bool HasEnoughCurrency(CurrencyAmount c){
        return GetCurrency(c.type).amount >= c.amount;
    }

    public CurrencyAmount GetCurrency(Currency c){
        foreach(CurrencyAmount ca in currencies)
            if(ca.type == c)
                return ca;
                
        return new CurrencyAmount();
    }
}
