using UnityEngine;

public class StandardTooltipTrigger : MonoBehaviour, ITooltip
{
    [SerializeField] private Tooltip prefab;
    [SerializeField] private TooltipAsset subTooltips;

    public TooltipData TooltipData() => new TooltipData{
        prefab = prefab,
        subTooltips = subTooltips.TooltipList(),
    };
}
