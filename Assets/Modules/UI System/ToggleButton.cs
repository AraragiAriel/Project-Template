using UnityEngine;
using System;

[RequireComponent(typeof(ButtonEvents))]
public class ToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject image;

    public Action<bool> OnToggle;
    
    private bool _toggle = false;
    public bool toggle{
        get => _toggle;
        set
        {
            _toggle = value;
            image.SetActive(_toggle);
            OnToggle?.Invoke(_toggle);
        }
    }

    private ButtonEvents _buttonEvents;
    private ButtonEvents buttonEvents => _buttonEvents ??= GetComponent<ButtonEvents>();

    void OnEnable()
    {
        buttonEvents.OnLeftClick.AddListener(Click);
    }

    void OnDisable()
    {
        buttonEvents.OnLeftClick.RemoveListener(Click);
    }

    public void Click()
    {
        toggle = !toggle;
    }
}
