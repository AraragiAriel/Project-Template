using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObject/Stat Data")]
public class Stat : ScriptableObject
{    
    public static implicit operator float(Stat stat) =>
        stat != null? stat.value : 0f;
        
    public UID uid;

    [Header("Display")]
    public LocalizedString statName;
    public LocalizedString description;
    [SerializeField] private Sprite icon;
    public List<UpgradeData> iconPriority = new();
    public bool requireMod = false;
    public Stat requiredMod;
    public bool isPercent = false;
    public bool allowDecimal = true;
    public bool usePercent = false;

    public bool meetsReq{
        get{
            if(!requireMod)
                return true;
            else if(requiredMod == null)
                return modCount > 0;
            else
                return requiredMod.meetsReq;
        }
    }

    public string valueText{
        get{
            if(!isPercent)
                return Util.Concat(value, allowDecimal);
            else
                return Util.Concat(value*100, allowDecimal) + "%";
        }
    }

    [Header("Value")]
    public float baseValue;
    [SerializeField] private CompositeValue value;

    public int intValue => value.intValue;
    public bool boolValue => value.boolValue;
    public int modCount => value.modCount;

    public Action<float> OnValueChange;

    public Sprite GetIcon(bool baseIcon){
        try{
            if(baseIcon)
                return icon;
            if(iconPriority.Count == 0)
                return icon;
            for(int i = iconPriority.Count - 1; i >= 0; i--)
                if(iconPriority[i].level > 0)
                    return iconPriority[i].icon;
            return iconPriority[0].icon;
        } catch {
            Debug.LogWarning("icon missing");
            return icon;
        }
    }

    public void SetModifier(ValueMod mod) => value.SetMod(mod);
    public void RemoveModifier(string id) => value.RemoveMod(id);
    
    public void Initialize(){
        value.OnValueChange -= ValueChangeConnect;
        value.OnValueChange += ValueChangeConnect;

        value.Reset();
        value.baseValue = baseValue;
    }

    private void ValueChangeConnect(float value) => OnValueChange?.Invoke(value);
}
