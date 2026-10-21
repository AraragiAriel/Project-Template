using UnityEngine;

public class SimpleTooltipTrigger : MonoBehaviour, ITooltip
{
    [SerializeField] private LocalizedStringData title;
    [SerializeField] private LocalizedStringData description;
    [SerializeField] private Tooltip prefab;
    [SerializeField] private bool setColor = false;
    [SerializeField] private ColorTag color;
    [SerializeField] private bool hideTitle = false;

    public TooltipData TooltipData()
        => new SimpleTooltipData
            {
                prefab = prefab == null ? Res.data.simpleTooltip : prefab,
                title = title,
                description = description,
                setColor = setColor,
                color = color,
                hideTitle = hideTitle,
            };
}
