using TMPro;
using UnityEngine;

public class SimpleTooltip : TooltipSetup
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public override void Setup(TooltipData data)
    {
        var simpleData = data as SimpleTooltipData;

        titleText.Set(simpleData.title);
        descriptionText.Set(simpleData.description);
    }
}

public class SimpleTooltipData : TooltipData
{
    public string title;
    public string description;
}
