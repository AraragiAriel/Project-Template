using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObject/Stat Data")]
public class Stat : UIDAsset, IColor, ILocalizer
{
    public static implicit operator float(Stat stat) =>
        stat != null ? stat.value : 0f;
        
    [Header("Display")]
    [SerializeField]private LocalizedString _statName;
    public string statName => _statName.Localize().ColorWrap(color);
    public LocalizedString description;
    public Sprite icon;
    public ColorTag color;

    [Header("Formatting")]
    public bool isPercent = false;
    public int decimalDigits = 0;

    public LocalizedString Localize(string field = default)
    {
        return field switch
        {
            "name" => statName,
            "description" => description,
            _ => statName,
        };
    }

    public ColorTag GetColor() => color;

    public string ValueText() => ((float)value).Format(new()
        {
            percent = isPercent,
            decimalDigits = decimalDigits,
            setDigits = true,
            digits = 3,    
        }
    );

    public string BreakdownText()
    {
        string s = "= ";
        if(!isPercent)
        {
            s += "(";
            string flat = value.flatTotalValue.Format(new(){});
            string percent = value.percentValue.Format(new()
            {
                percent = true,
                plusSign = true,
                postSign = " ",
            });
            s += $"{flat} {percent}";
            s += ")";
        } else
        {
            string percent = value.flatTotalValue.Format(new(){percent = true});
            s += $"{percent}";
        }
        
        foreach(var mult in value.Mults())
            s += $" X{mult.Format()}";

        return s;
    }

    public CompositeValue value = new();

    public int intValue => value.intValue;
    public bool boolValue => value.boolValue;
    public int modCount => value.mods.Count;

    public Action<float> OnValueChange;

    public void SetModifier(ValueMod mod) => value.SetMod(mod);
    public void RemoveModifier(RID id) => value.RemoveMod(id);
    
    public void Initialize()
    {
        value.OnValueChange -= ValueChangeConnect;
        value.OnValueChange += ValueChangeConnect;

        value.Reset();
    }

    private void ValueChangeConnect(float value) => OnValueChange?.Invoke(value);

    [System.Serializable]
    public class Mod : ValueMod
    {
        public Stat stat;

        public Mod() : base(){}
        public Mod(RID id, float value, Type type) : base(id, value, type){}
        public Mod(Mod mod) : base(mod)
        {
            stat = mod.stat;
        }

        public void SetMod() => stat.SetModifier(this);
        public void RemoveMod() => stat.RemoveModifier(id);

        public override string Format() => Format(new(this));

        public static string Format(StatFormatting formatting)
        {
            string s = "";
            formatting.overrideFlatToPercent = formatting.statMod.stat.isPercent;

            if(formatting.includeStat && formatting.statFirst)
                s += $"{formatting.statMod.stat.statName} ";
                
            s += $"{ValueMod.Format(formatting)}";

            if(formatting.includeStat && !formatting.statFirst)
                s += $" {formatting.statMod.stat.statName}";

            if(formatting.includeColor)
                s.ColorWrap(formatting.statMod.stat.color);

            return s;
        }

        public class StatFormatting : ValueFormatting
        {
            public Mod _statMod;
            public Mod statMod
            {
                get => _statMod;
                set
                {
                    _statMod = value;
                    valueMod = value;
                }
            }
            public bool includeStat = true;
            public bool includeColor = true;
            public bool statFirst = false;

            public override string Format() => Mod.Format(this);

            public StatFormatting(){}
            public StatFormatting(ValueMod statMod) : base(statMod){}

            public StatFormatting(Mod statMod) : base(statMod)
            {
                this.statMod = new(statMod);
            }
        }
    }
}