using System;
using System.Collections;
using System.Collections.Generic;

namespace Core.EventBus
{
    public class EventBus
    {
        private Dictionary<Type, List<Action<IEvent>>> _callbacks;

        public void Subscribe<T>(Action<T> callback)
            where T : IEvent
        {
            Type type = typeof(T);
            if (!_callbacks.ContainsKey(type))
            {
                _callbacks[type] = new List<Action<IEvent>>();
            }
            _callbacks[type].Add(callback as Action<IEvent>);
        }

        public void Unsubscribe<T>(Action<T> callback)
            where T : IEvent
        {
            Type type = typeof(T);
            if (!_callbacks.ContainsKey(type))
            {
                return;
            }
            // ”€звимое место TryGetValue
            _callbacks[type].Remove(callback as Action<IEvent>);
        }

        public void Invoke<T>(T eventListType)
            where T : IEvent
        {
            Type type = typeof(T);
            if (!_callbacks.ContainsKey(type))
            {
                return;
            }
            foreach(var callback in _callbacks[type])
            {
                callback?.Invoke(eventListType);
            }
        }
    }
}

