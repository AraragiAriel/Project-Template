using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Currency{
    Null = 0,
    Ticket = 1,
    Token = 2,
}

[System.Serializable]
public struct CurrencyAmount{
    public Currency type;
    public float amount;

    public CurrencyAmount(Currency currency = Currency.Null, float amount = 0f){
        this.type = currency;
        this.amount = amount;
    }

    public CurrencyData GetData() => CurrencyManager.GetData(type);

    public static implicit operator float(CurrencyAmount ca) => ca.amount;
}

public static class CurrencyManager
{   
    private static List<CurrencyAmount> currencies => Res.save.currencySave.currencies;

    public static void AddCurrency(CurrencyAmount c){
        Variation v = new();
        v.previous = GetCurrency(c.type);
        bool found = false;
        for(int i = 0; i < currencies.Count; i++){
            if(currencies[i].type == c.type){
                currencies[i] = new CurrencyAmount(c.type, currencies[i].amount + c.amount);
                found = true;
            }
        }
        if(!found)
            currencies.Add(c);
        v.current = GetCurrency(c.type);
        StaticActions.OnCurrencyChange?.Invoke(c.type, v);
        StaticActions.OnEconUpdate?.Invoke();
    }

    public static bool SpendCurrency(CurrencyAmount c){
        if(!HasEnoughCurrency(c))
            return false;

        Variation v = new();
        v.previous = GetCurrency(c.type);
        for(int i = 0; i < currencies.Count; i++){
            if(currencies[i].type == c.type){
                currencies[i] = new CurrencyAmount(c.type, currencies[i].amount - c.amount);
                break;
            }
        }     
        v.current = GetCurrency(c.type);
        StaticActions.OnCurrencyChange?.Invoke(c.type, v);
        StaticActions.OnEconUpdate?.Invoke();
        return true;
    }

    public static void SetCurrency(CurrencyAmount c){
        Variation v = new();
        v.previous = GetCurrency(c.type);
        bool found = false;
        for(int i = 0; i < currencies.Count; i++){
            if(currencies[i].type == c.type){
                currencies[i] = new CurrencyAmount(c.type, c.amount);
                found = true;
            }
        }
        if(!found)
            currencies.Add(c);

        v.current = GetCurrency(c.type);
        StaticActions.OnCurrencyChange?.Invoke(c.type, v);
        StaticActions.OnEconUpdate?.Invoke();
    }

    public static CurrencyAmount GetCurrency(Currency c) => currencies.Find(ca => ca.type == c);
    public static bool HasEnoughCurrency(CurrencyAmount c) => GetCurrency(c.type).amount >= c.amount;
    public static CurrencyData GetData(Currency c) => Res.data.currenciesData.Get(c);
    
}