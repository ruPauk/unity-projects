using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReactValue<T> : IDisposable
{
    T Value { get; set; }
    event Action<T> OnChange;
}
