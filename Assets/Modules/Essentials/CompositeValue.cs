using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValueMod
{
    public static string Format(float value, Type type, bool overrideFlatToPercent = false, bool hideSign = false){
        if(overrideFlatToPercent && type == Type.Flat)
            type = Type.Percent;
        switch(type){
            case Type.Flat:
                return hideSign ? value.ToString() : Util.ExposeSign(value);
            case Type.Percent:
                return hideSign ? Util.FormatPercent(value) : $"{Util.ExposeSign(value*100)}%";
            case Type.Mult:
                return hideSign ? value.ToString() : $"X{value}";
            default:
                return value.ToString();
        } 
    }

    public enum Type{
        Flat,
        Percent,
        Mult,
    }

    [HideInInspector] public RID id;
    public float value;
    public Type type;

    public string Format(bool overrideFlatToPercent = false, bool hideSign = false)
        => Format(value, type, overrideFlatToPercent, hideSign);

    public ValueMod(RID id, float value, Type type){
        this.id = id;
        this.value = value;
        this.type = type;
    }
}
    
[System.Serializable]
public class CompositeValue
{
    public CompositeValue(){
        CalculateValue();
    }

    public CompositeValue(float value){
        baseValue = value;
    }

    public static implicit operator float(CompositeValue cv) =>
        cv != null ? cv.value : 0f;
    public static implicit operator string(CompositeValue cv) =>
        cv != null ? cv.value.ToString() : "";

    [SerializeField] private float _baseValue;
    public float baseValue{
        get => _baseValue;
        set{
            _baseValue = value;
            CalculateValue();
        }
    }

    public bool useMin = false;
    public float min = 0f;
    public bool useMax = false;
    public float max = 1f;

    [SerializeField] private float value;
    [SerializeField] private List<ValueMod> mods = new();

    public int intValue => Mathf.RoundToInt(value);
    public bool boolValue => intValue >= 1;
    public Action<float> OnValueChange;
    public int modCount => mods.Count;

    public void CalculateValue(){
        float flatValue = 0f;
        float percentValue = 0f;
        float multValue = 1f;
        foreach(ValueMod mod in mods)
            switch(mod.type){
                case ValueMod.Type.Flat:
                    flatValue += mod.value;
                    break;
                case ValueMod.Type.Percent:
                    percentValue += mod.value;
                    break;
                case ValueMod.Type.Mult:
                    multValue *= mod.value;
                    break;
            }

        value = (baseValue + flatValue)*(1f + percentValue)*multValue;
        if(useMin)
            value = Mathf.Max(value, min);
        if(useMax)
            value = Mathf.Min(value, max);
        OnValueChange?.Invoke(value);
    }

    public void SetMod(ValueMod mod){
        bool itemFound = false;
        for(int i = 0; i < mods.Count; i++)
            if(mods[i].id == mod.id){
                mods[i] = mod;
                itemFound = true;
                break;
            }

        if(!itemFound)
            mods.Add(mod);

        CalculateValue();
    }

    public void RemoveMod(RID id){
        for(int i = 0; i < mods.Count; i++)
            if(mods[i].id == id){
                mods.RemoveAt(i);
                break;
            }

        CalculateValue();
    }

    public void Reset(){
        mods.Clear();
        CalculateValue();
    }
}
