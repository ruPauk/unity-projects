using System;

namespace Core.EventBus
{
    public interface IEvent { }

    public interface IBaseEventReceiver { }

    //Интерфейс для параметризированных слушателей событий
    public interface IEventReceiver<T> : IBaseEventReceiver 
        where T : struct, IEvent
    {
        void OnEvent(T @event);
    }
}
