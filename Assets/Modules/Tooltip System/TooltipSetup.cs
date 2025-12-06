using UnityEngine;

public abstract class TooltipSetup : MonoBehaviour
{
    protected Tooltip _tooltip;
    protected Tooltip tooltip => _tooltip ??= GetComponent<Tooltip>();

    private void OnEnable(){
        tooltip.OnSetup += Setup;
    }

    private void OnDisable(){
        tooltip.OnSetup -= Setup;        
    }

    protected abstract void Setup(TooltipData data);
}
