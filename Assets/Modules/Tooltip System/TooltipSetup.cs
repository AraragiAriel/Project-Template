using UnityEngine;

public abstract class TooltipSetup : MonoBehaviour
{
    protected Tooltip _tooltip;
    protected Tooltip tooltip => _tooltip ??= GetComponent<Tooltip>();

    public abstract void Setup(TooltipData data);
}
