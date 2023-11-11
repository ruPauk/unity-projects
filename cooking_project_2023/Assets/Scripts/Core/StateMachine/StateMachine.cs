using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState : IDisposable
{
    void Enter();
    void Update();
    void Exit();
}

public interface IStateMachine
{
    IState CurrentState { get; }

    void ChangeState<T>() 
        where T : class, IState, new();
    void Update();
}

public interface IStateMachineOwner { };

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
        CurrentState.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}

public abstract class BaseState<T> : IState 
    where T : class, IStateMachineOwner
{
    public T Value { get; set; }

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
