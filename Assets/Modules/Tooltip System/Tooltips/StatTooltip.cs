using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatTooltip : TooltipSetup
{
    [SerializeField] private List<Image> icons = new();
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI breakdownText;
    [SerializeField] private TextBox nameBox;

    public override void Setup(TooltipData data)
    {
        Stat stat = (data as StatTooltipData).stat;

        foreach(var icon in icons)
            icon.sprite = stat.icon;
        nameBox.Set(stat.statName, stat.color);
        descriptionText.Set(stat.description);
        breakdownText.Set(stat.BreakdownText());
    }
}

public class StatTooltipData : TooltipData
{
    public Stat stat;
}