using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ValueMod{
    public enum Type{
        Flat,
        Percent,
        Mult,
    }

    public string id;
    public float value;
    public Type type;

    public ValueMod(string id, float value, Type type){
        this.id = id;
        this.value = value;
        this.type = type;
    }
}
    
[Serializable]
public class CompositeValue
{
    public CompositeValue(){
        CalculateValue();
    }

    public static implicit operator float(CompositeValue cv) =>
        cv != null ? cv.value : 0f;

    private float _baseValue;
    public float baseValue{
        get => _baseValue;
        set{
            _baseValue = value;
            CalculateValue();
        }
    }
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

    public void RemoveMod(string id){
        for(int i = 0; i < mods.Count; i++)
            if(mods[i].id == id){
                mods.RemoveAt(i);
                return;
            }

        CalculateValue();
    }

    public void Reset(){
        mods.Clear();
        CalculateValue();
    }
}
