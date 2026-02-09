using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    // STATIC

    public static CurrencyManager _instance;
    public static CurrencyManager instance => _instance??= FindFirstObjectByType<CurrencyManager>();

    private static List<CurrencyAmount> currencies => Res.save.currencySave.currencies;

    //INSTANCE

    [System.Serializable]
    private class Modifier{
        public Currency type;
        public Stat gain;
        public Stat discount;
        public ClipData gainClip;
        public ClipData spendClip;
    };
    [SerializeField] private List<Modifier> modifiers = new();

    private void OnEnable(){
        foreach(var modifier in modifiers){
            if(modifier.gain != null)
                modifier.gain.OnValueChange += ModifierChange;
            if(modifier.discount != null)
                modifier.discount.OnValueChange += ModifierChange;
        }
    }

    private void OnDisable(){
        foreach(var modifier in modifiers){
            if(modifier.gain != null)
                modifier.gain.OnValueChange -= ModifierChange;
            if(modifier.discount != null)
                modifier.discount.OnValueChange -= ModifierChange;
        }        
    }

    private void ModifierChange(float value) => StaticActions.OnEconUpdate?.Invoke();

    public void AddCurrency(CurrencyAmount c){
        c.amount *= Gain(c.type);
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
        StaticActions.OnCurrencyChange?.Invoke(GetCurrency(c.type), v);
        StaticActions.OnEconUpdate?.Invoke();
        AudioManager.Play(Clip(c.type, true));
    }

    public bool SpendCurrency(CurrencyAmount c){
        if(!HasEnoughCurrency(c))
            return false;

        c.amount *= 1 - Discount(c.type);
        Variation v = new();
        v.previous = GetCurrency(c.type);
        for(int i = 0; i < currencies.Count; i++){
            if(currencies[i].type == c.type){
                currencies[i] = new CurrencyAmount(c.type, currencies[i].amount - c.amount);
                break;
            }
        }     
        v.current = GetCurrency(c.type);
        StaticActions.OnCurrencyChange?.Invoke(GetCurrency(c.type), v);
        StaticActions.OnEconUpdate?.Invoke();
        AudioManager.Play(Clip(c.type, false));
        return true;
    }

    public void SetCurrency(CurrencyAmount c){
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
        StaticActions.OnCurrencyChange?.Invoke(c, v);
        StaticActions.OnEconUpdate?.Invoke();
    }

    public bool HasEnoughCurrency(CurrencyAmount c){
        c.amount *= 1f - Discount(c.type);
        return GetCurrency(c.type).amount >= c.amount;
    }

    public CurrencyAmount GetCurrency(Currency c) => currencies.Find(ca => ca.type == c);

    private float Gain(Currency type){
        foreach(var modifier in modifiers)
            if(modifier.type == type && modifier.gain != null)
                return modifier.gain;
        return 1f;
    }

    private float Discount(Currency type){
        foreach(var modifier in modifiers)
            if(modifier.type == type && modifier.discount != null)
                return modifier.discount;
        return 0f;
    }

    private ClipData Clip(Currency type, bool gain){
        foreach(var modifier in modifiers)
            if(modifier.type == type)
                return gain ? modifier.gainClip : modifier.spendClip;
        return null;
    }
}

[System.Serializable]
public struct CurrencyAmount{
    public Currency type;
    public float amount;

    public CurrencyAmount(Currency type = Currency.Null, float amount = 0f){
        this.type = type;
        this.amount = amount;
    }

    public CurrencyData Data() => type.Data();

    public string Format(){
        return Mathf.FloorToInt(amount).ToString();
    }

    public static implicit operator float(CurrencyAmount ca) => ca.amount;
}

public enum Currency{
    [EnumSkip] Null = 0,
    [Localize("Ticket", "name")] Ticket = 1,
    [Localize("Token", "name")] Token = 2,
}

public static class CurrencyExtension{
    public static CurrencyData Data(this Currency c) => Res.data.currenciesData.Get(c);
}