using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
    
[System.Serializable]
public class CompositeValue
{
    public CompositeValue()
    {
        CalculateValue();
    }

    public CompositeValue(float value)
    {
        baseValue = value;
        CalculateValue();
    }

    public static implicit operator float(CompositeValue cv) =>
        cv != null ? cv.value : 0f;
    public static implicit operator string(CompositeValue cv) =>
        cv != null ? cv.value.ToString() : "";

    [SerializeField] private float _baseValue;
    public float baseValue
    {
        get => _baseValue;
        set
        {
            _baseValue = value;
            CalculateValue();
        }
    }

    [Space(16)]

    public bool useMin = false;
    public float min = 0f;
    public bool useMax = false;
    public float max = 1f;
    
    [SerializeField] private List<ValueMod> _mods = new();
    public List<ValueMod> mods
    {
        get => _mods;
        set => _mods = value;
    }

    public List<float> Mults()
    {
        List<float> mults = new();
        foreach(var mod in mods)
            if(mod.type == ValueMod.Type.Mult)
                mults.Add(mod.value);
        mults = mults.OrderBy(m => m).ToList();
        return mults;
    }

    [SerializeField] public float flatValue {get; private set;}
    [SerializeField] public float flatTotalValue {get; private set;}
    [SerializeField] public float percentValue {get; private set;}
    [SerializeField] public float multValue {get; private set;}
    [SerializeField] public float value {get; private set;}

    public int intValue => Mathf.RoundToInt(value);
    public bool boolValue => intValue >= 1;

    public Action<float> OnValueChange;

    public void CalculateValue()
    {
        flatValue = 0f;
        flatTotalValue = 0f;
        percentValue = 0f;
        multValue = 1f;

        foreach(ValueMod mod in mods)
            switch(mod.type)
            {
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

        flatTotalValue = baseValue + flatValue;
        value = flatTotalValue*(1f + percentValue)*multValue;

        if(useMin)
            value = Mathf.Max(value, min);
        if(useMax)
            value = Mathf.Min(value, max);

        OnValueChange?.Invoke(value);
    }

    public void SetMod(ValueMod mod)
    {
        bool itemFound = false;
        foreach(int i in 0.To(mods.Count - 1, true))
            if(mods[i].id == mod.id)
            {
                mods[i] = mod;
                itemFound = true;
                break;
            }

        if(!itemFound)
            mods.Add(mod);

        CalculateValue();
    }

    public void RemoveMod(RID id)
    {
        for(int i = 0; i < mods.Count; i++)
            if(mods[i].id == id)
            {
                mods.RemoveAt(i);
                CalculateValue();
                break;
            }
    }

    public void Reset()
    {
        mods.Clear();
        CalculateValue();
    }
}