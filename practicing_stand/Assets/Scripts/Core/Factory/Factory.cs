using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory<T>
{
    T Prototype { get; }
    T Create();
}

public abstract class Factory<T> : IFactory<T>
{
    public Factory(T prototype)
    {
        Prototype = prototype;
    }

    public T Prototype { get; private set; }

    public abstract T Create();
}

public class NativeFactory<T> : Factory<T>
    where T : class
{
    public NativeFactory(T prototype) : base(prototype)
    {
        
    }

    public override T Create()
    {
        return Activator.CreateInstance<T>();
    }
}

public class MonoFactory<T> : Factory<T>
    where T : Component
{
    public MonoFactory(T component) : base(component)
    {
        
    }

    public override T Create()
    {
        if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
        {
            return GameObject.Instantiate<T>(Prototype);
        }
        else
            return new GameObject().AddComponent<T>();
    }
}