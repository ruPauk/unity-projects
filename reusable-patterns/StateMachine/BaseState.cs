using System;

public abstract class BaseState<T> : IState 
    where T : class, IStateMachineOwner
{
    public T Owner { get; set; }
    public IStateMachine StateMachine { get; set; }

    public virtual void Dispose()
    { 
        GC.SuppressFinalize(this);
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {

    }
}
