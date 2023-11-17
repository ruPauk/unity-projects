using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactValue<T> : IReactValue<T>
{
    private T _value;
    private bool _isDisposed;

    public ReactValue(T value)
    {
        _value = value;
    }

    public T Value 
    {
        get => _value;
        set
        {
            _value = value;
            OnChange?.Invoke(_value);
        }
    }

    public event Action<T> OnChange;

    public void Dispose()
    {
        if (_isDisposed)
            return;
        OnChange = null;
        _isDisposed = true;
        GC.SuppressFinalize(this);
    }
}
