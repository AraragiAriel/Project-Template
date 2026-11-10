using UnityEngine;
using TMPro;

[RequireComponent(typeof(ButtonEvents))]
public class DevToolLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private DevTools.Slot slot;

    private ButtonEvents _button;
    private ButtonEvents button => _button ??= GetComponent<ButtonEvents>();

    void OnEnable()
    {
        button.OnAnyClick.AddListener(Click);
    }

    void OnDisable()
    {
        button.OnAnyClick.RemoveListener(Click);
    }

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
