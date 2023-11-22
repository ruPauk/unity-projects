using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactValueCollection<T, IReactValue>
{
    private Dictionary<T, IReactValue> _keyValuePairs;

    public ReactValueCollection()
    {
        _keyValuePairs = new Dictionary<T, IReactValue>();
    }

    public void AddReactValueWithTag(T tag, IReactValue value)
    {
        if (_keyValuePairs.ContainsKey(tag))
        {
            _keyValuePairs[tag] = value;
        }
        else
        {
            _keyValuePairs.Add(tag, value);
        }
    }
}
