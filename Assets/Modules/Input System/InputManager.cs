using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ControlScheme{
    None = 0,
    Keyboard = 1,
    Gamepad = 2,
}

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput input;

    private ControlScheme _currentControlScheme = ControlScheme.None;
    private ControlScheme currentControlScheme{
        get => _currentControlScheme;
        set{
            if(value == _currentControlScheme)
                return;

            _currentControlScheme = value;
            StaticActions.OnControlSchemeChange?.Invoke(_currentControlScheme);
        }
    }

    private void OnEnable(){
        input.onControlsChanged += ControlsChange;
    }

    private void OnDisable(){
        input.onControlsChanged -= ControlsChange;
    }

    private void Start(){
        SetScheme();
    }

    private void ControlsChange(PlayerInput input) => SetScheme();
    private void SetScheme() => currentControlScheme = Enum.Parse<ControlScheme>(input.currentControlScheme);
}
