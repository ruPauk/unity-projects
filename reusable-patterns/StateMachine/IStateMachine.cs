public interface IStateMachine
{
    IState CurrentState { get; }

    void ChangeState<T>() 
        where T : class, IState, new();
    void Update();
}
