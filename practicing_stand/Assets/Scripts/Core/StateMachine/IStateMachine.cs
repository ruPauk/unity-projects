public interface IStateMachine
{
    IState CurrentState { get; }
    //Пришлось сюда добавить Owner еще, чотбы можно было его инициализировать
    IStateMachineOwner Owner { get; }
    void ChangeState<T>() 
        where T : class, IState, new();
    void Update();
}
