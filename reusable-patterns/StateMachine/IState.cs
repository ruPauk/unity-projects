using System;

public interface IState : IDisposable
{
    IStateMachine StateMachine { get; set; } 

    void Enter();
    void Update();
    void Exit();
}
