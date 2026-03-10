using UnityEngine;
using TMPro;

public class StatTooltip : TooltipSetup
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public override void Setup(TooltipData data){
        Stat stat = (data as StatTooltipData).stat;
        nameText.Set(stat.statName);
        descriptionText.Set(stat.description);
    }
}

public class StatTooltipData : TooltipData{
    public Stat stat;
}
