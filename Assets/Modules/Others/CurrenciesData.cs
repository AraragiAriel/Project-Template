using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "currency Data", menuName = "ScriptableObject/Others/Currencies Data")]
public class CurrenciesData : ScriptableObject
{
    public List<CurrencyData> list;

    public CurrencyData Get(Currency c){
        foreach(var data in list)
            if(data.type == c)
                return data;
        return null;
    }
}