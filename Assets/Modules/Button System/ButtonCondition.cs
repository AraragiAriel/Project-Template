using UnityEngine;

[RequireComponent(typeof(ButtonEnabler))]
public abstract class ButtonCondition : MonoBehaviour
{
    protected bool _pass = true;
    protected bool pass
    {
        get => _pass;
        set
        {
            if(_pass == value)
                return;

            _pass = value;
            enabler.SetUnit(pass, rid);
        }
    }

    private RID rid = new();

    private ButtonEnabler _enabler;
    private ButtonEnabler enabler => _enabler ??= GetComponent<ButtonEnabler>();
}
