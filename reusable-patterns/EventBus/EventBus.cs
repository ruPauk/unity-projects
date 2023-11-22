using System;
using System.Collections.Generic;


public class EventBus
{
    //Типы событий, список подписчиков
    private Dictionary<Type, List<WeakReference<IBaseEventReceiver>>> _receivers;
    private Dictionary<int, WeakReference<IBaseEventReceiver>> __receiverHashToReference;

    public EventBus()
    {
        _receivers = new Dictionary<Type, List<WeakReference<IBaseEventReceiver>>>();
        __receiverHashToReference = new Dictionary<int, WeakReference<IBaseEventReceiver>>();
    }

    public void Register<T>(IEventReceiver<T> receiver) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        if (!_receivers.ContainsKey(eventType))
        {
            _receivers[eventType] = new List<WeakReference<IBaseEventReceiver>>();

            WeakReference<IBaseEventReceiver> reference = new WeakReference<IBaseEventReceiver>(receiver);

            _receivers[eventType].Add(reference);
            __receiverHashToReference[receiver.GetHashCode()] = reference;
        }
    }

    public void Unregister<T>(IEventReceiver<T> receiver) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        int receiverHash = receiver.GetHashCode();
        if (!_receivers.ContainsKey(eventType) || !__receiverHashToReference.ContainsKey(receiverHash))
            return;

        WeakReference<IBaseEventReceiver> reference = __receiverHashToReference[receiverHash];
        _receivers[eventType].Remove(reference);
        __receiverHashToReference.Remove(receiverHash);
    }

    public void Raise<T>(T @event) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        if (!_receivers.ContainsKey(eventType))
        {
            return;
        }

        foreach(WeakReference<IBaseEventReceiver> reference in _receivers[eventType])
        {
            if (reference.TryGetTarget(out IBaseEventReceiver receiver))
                ((IEventReceiver<T>)receiver).OnEvent(@event);
        }
    }
}
