using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrencySave
{
    public List<CurrencyAmount> currencies = new();

    public CurrencySave(){
        currencies = new();

        foreach(var e in Util.EnumList<Currency>())
            currencies.Add(new CurrencyAmount(e, 0f));
    }
}
