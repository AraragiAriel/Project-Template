using UnityEngine;

public abstract class State
{
    protected StateMachine manager;

    public virtual void Initialize(StateMachine manager){
        this.manager = manager;
    }
    public virtual void Enter(){}
    public virtual void Update(){}
    public virtual void FixedUpdate(){}
    public virtual void CollisionEnter(){}
    public virtual void Exit(){}
    public virtual bool CanEnter() => true;
}
