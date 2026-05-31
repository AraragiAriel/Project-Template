using UnityEngine;
using TMPro;

public class DevToolLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private DevTools.Slot slot;

    public void Setup(DevTools.Slot slot)
    {
        this.slot = slot;
        string s = "";
        if(slot.key != KeyCode.None)
            s += $"<b>[{slot.key}]</b> ";
        s += slot.type.ToString();
        tmp.Set(s);
    }

    public void Click()
    {
        GetComponentInParent<DevTools>().Use(slot.type);
    }
}
