using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputReader : MonoBehaviour
{
    [SerializeField] protected List<GameState> activeGameStates = new();
    protected bool active = true;
    
    protected InputActions inputActions;
    protected ControlScheme _currentScheme = ControlScheme.None;
    protected ControlScheme currentScheme{
        get => _currentScheme;
        set{
            if(value == _currentScheme)
                return;
            
            _currentScheme = value;
            DisableMap();
            inputActions.bindingMask = InputBinding.MaskByGroup(currentScheme.ToString());
            EnableMap();
        }
    }

    protected virtual void Awake(){
        if(inputActions == null)
            inputActions = new();
    }

    protected virtual void OnEnable(){
        StaticActions.OnControlSchemeChange += ControlSchemeChange;
        StaticActions.OnGameStateChange += GameStateChange;

        AddCallbacks();
        EnableMap();
    }

    protected virtual void OnDisable(){
        StaticActions.OnControlSchemeChange -= ControlSchemeChange;        
        StaticActions.OnGameStateChange -= GameStateChange;

        DisableMap();
        RemoveCallbacks();
    }

    protected virtual void ControlSchemeChange(ControlScheme scheme) => currentScheme = scheme;
    protected virtual void GameStateChange(GameState state) => active = activeGameStates.Contains(state);

    protected abstract void EnableMap();
    protected abstract void DisableMap();
    protected abstract void AddCallbacks();
    protected abstract void RemoveCallbacks();
}
