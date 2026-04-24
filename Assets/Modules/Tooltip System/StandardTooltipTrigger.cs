using UnityEngine;

public class StandardTooltipTrigger : MonoBehaviour, ITooltip
{
    [SerializeField] private Tooltip prefab;
    [SerializeField] private TooltipAssets subTooltips;

    public TooltipData TooltipData() => new TooltipData{
        prefab = prefab,
        subTooltips = subTooltips.TooltipList(),
    };
}
