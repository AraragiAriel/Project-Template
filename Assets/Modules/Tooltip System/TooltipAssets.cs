using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TooltipAssets
{
    public List<Object> assets;

    public List<ITooltip> TooltipList(){
        List<ITooltip> tooltips = new();

        foreach (var asset in assets){
            if(asset == null) continue;

            ITooltip iTooltip = null;
            switch(asset){
                case ScriptableObject so when so is ITooltip tooltip:
                    iTooltip = tooltip;
                    break;
                case GameObject go when go.TryGetComponent<ITooltip>(out var tooltip):
                    iTooltip = tooltip;
                    break;
            }
            if(iTooltip != null)
                tooltips.Add(iTooltip);
            else
                Debug.LogWarning($"Asset {asset.name} doesn't implement ITooltip");            
        }

        return tooltips;
    }
}
