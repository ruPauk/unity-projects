using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> : IStateMachine
    where T : class, IStateMachineOwner
{
    public StateMachine(T owner)
    {
        Owner = owner;
    }

    public T Owner { get; }
    public IState CurrentState { get; private set; }

    public void ChangeState<T>() 
        where T : class, IState, new()
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
            CurrentState.Dispose();
        }
        CurrentState = new T();
        CurrentState.StateMachine = this;
        CurrentState.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}
