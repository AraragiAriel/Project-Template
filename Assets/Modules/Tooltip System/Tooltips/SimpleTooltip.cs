using TMPro;
using UnityEngine;

public class SimpleTooltip : TooltipSetup
{
    [SerializeField] protected TextBox textBox;
    [SerializeField] protected TextMeshProUGUI descriptionText;
    [SerializeField] protected GameObject titleHolder;

    public override void Setup(TooltipData data)
    {
        var simpleData = data as SimpleTooltipData;

        textBox.Set(simpleData.title, simpleData.GetColor(), simpleData.replacer);
        if(simpleData.hideTitle)
            titleHolder.SetActive(false);
            
        descriptionText.Set(simpleData.description, data.replacer);
    }
}

public class SimpleTooltipData : TooltipData
{
    public string title;
    public string description;
    public bool setColor = false;
    public Color color;
    public bool hideTitle = false;

    public Color GetColor() => setColor ? color : Res.data.colorTags.Get("ticket");
}
