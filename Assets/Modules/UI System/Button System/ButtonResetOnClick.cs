using UnityEngine;

[RequireComponent(typeof(ButtonEvents))]
public class ButtonResetOnClick : MonoBehaviour
{
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

    private void Click()
    {
        Util.DeselectPointerHandler(gameObject);
        Util.SelectPointerHandler(gameObject);
    }
}
