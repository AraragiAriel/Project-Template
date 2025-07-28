using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public List<State> states = new();
    public State currentState;
    public Action<Type> OnStateChange;

    #if UNITY_EDITOR
    [Header("Editor")]
    [SerializeField] private bool debug = false;
    #endif

    protected virtual void Awake(){
        PopulateStates();
        Initialize();
    }

    protected virtual void PopulateStates(){}
    protected virtual void Initialize(){
        if(states.Count == 0){
            enabled = false;
            return;
        }
        
        foreach(State state in states)
            state.Initialize(this);
    }
    protected virtual void OnEnable(){}
    protected virtual void OnDisable(){}
    protected virtual void Start(){}

    protected virtual void Update(){
        currentState?.Update();
    }

    protected virtual void FixedUpdate(){
        currentState?.FixedUpdate();
    }

    public void ChangeState<T>() where T : State {
        Type stateType = typeof(T);
        State newState = null;
        if(stateType != null){
            // NOT SUPPOSED TO BE NULL
            newState = states.Find(s => s.GetType() == stateType);
            if(newState == null){
                // NOT FOUND
                Debug.LogWarning("state not found: " + stateType.ToString());
                return;
            } else if(!newState.CanEnter()){
                // FOUND, CAN'T ENTER
                return;
            }
        }

        currentState?.Exit();
        #if UNITY_EDITOR
        if(Res.editor.debugStateMachine && debug)
            if(currentState != null)
                Util.Debug(currentState.ToString());
        #endif
        currentState?.Enter();
        currentState?.Update();
        OnStateChange?.Invoke(currentState.GetType());
    }

    private void OnDestroy(){
        currentState?.Exit();
    }
}
