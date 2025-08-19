using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObject/Stat Data")]
public class Stat : ScriptableObject
{
    [System.Serializable]
    private class LabeledValue{
        public string id;
        public float value;
        public bool percent;

        public LabeledValue(string id, float value, bool percent){
            this.id = id;
            this.value = value;
            this.percent = percent;
        }
    }

    public UID uid;

    [Header("Display")]
    public LocalizedString statName;
    public LocalizedString description;
    [SerializeField] private Sprite icon;
    public List<UpgradeData> iconPriority = new();
    public bool isPercent = false;
    public bool usePercent = false;
    public bool requireMod = true;
    public Stat requiredMod;

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

    [Header("Value")]
    public float baseValue;
    public float recycleIncrement;
    [SerializeField] private List<LabeledValue> modifiers = new();

    [Space]
    
    public float flatValue;
    public float percentValue;
    public float value;
    public int intValue => Mathf.RoundToInt(value);
    public bool boolValue => intValue >= 1;
    public int modCount{get{return modifiers.Count;}}

    public Action<float> OnValueChange;

    private void OnValidate(){
        CalculateValue();
    }

    public void Initialize(){
        modifiers.Clear();
        CalculateValue();
    }

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

    private void CalculateValue(){
        flatValue = 0f;
        percentValue = 0f;
        foreach(LabeledValue labeledValue in modifiers)
            if(labeledValue.percent)
                percentValue += labeledValue.value;
            else
                flatValue += labeledValue.value;

        value = (baseValue + flatValue)*(1f + percentValue);
        OnValueChange?.Invoke(value);
    }

    public void SetModifier(string id, float value, bool percent){
        bool itemFound = false;
        foreach(LabeledValue labeledValue in modifiers)
            if(labeledValue.id == id){
                labeledValue.value = value;
                labeledValue.percent = percent;
                itemFound = true;
                break;
            }

        if(!itemFound)
            modifiers.Add(new LabeledValue(id, value, percent));

        CalculateValue();
    }

    public bool RemoveModifier(string id){
        foreach(LabeledValue labeledValue in modifiers)
            if(labeledValue.id == id){
                modifiers.Remove(labeledValue);
                CalculateValue();
                return true;
            }
            
        return false;
    }

    public static implicit operator float(Stat stat) =>
        stat != null? stat.value : 0f;
}
