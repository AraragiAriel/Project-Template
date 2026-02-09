using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObject/Stat Data")]
public class Stat : UIDAsset, IColor
{
    public static implicit operator float(Stat stat) =>
        stat != null ? stat.value : 0f;
        
    [Header("Display")]
    [SerializeField]private LocalizedString _statName;
    public string statName => _statName.Localize().ColorWrap(color);
    public LocalizedString description;
    public Sprite icon;
    public ColorTag color;
    public bool isPercent = false;
    public bool allowDecimal = true;
    public bool usePercent = false;

    public ColorTag GetColor() => color;

    public string valueText{
        get{
            if(!isPercent)
                return Util.Concat(value, allowDecimal);
            else
                return Util.Concat(value*100, allowDecimal) + "%";
        }
    }

    public CompositeValue value;

    public int intValue => value.intValue;
    public bool boolValue => value.boolValue;
    public int modCount => value.modCount;

    public Action<float> OnValueChange;

    public void SetModifier(ValueMod mod) => value.SetMod(mod);
    public void RemoveModifier(RID id) => value.RemoveMod(id);
    
    public void Initialize(){
        value.OnValueChange -= ValueChangeConnect;
        value.OnValueChange += ValueChangeConnect;

        value.Reset();
    }

    private void ValueChangeConnect(float value) => OnValueChange?.Invoke(value);

    [System.Serializable]
    public class Mod{
        public Stat stat;
        public float value;
        public ValueMod.Type type;

        public void SetMod(RID id, float mult = 1f){
            stat.SetModifier(new ValueMod(id, mult*value, type));
        }

        public void RemoveMod(RID id){
            stat.RemoveModifier(id);
        }

        public string Format(float mult = 1f, bool includeStat = true, bool includeColor = true){
            string s = "";
            s += $"{ValueMod.Format(mult*value, type, stat.isPercent)}";
            if(includeStat)
                s += $" {stat.statName}";
            if(includeColor)
                s.ColorWrap(stat.color);
            return s;
        }
    }
}