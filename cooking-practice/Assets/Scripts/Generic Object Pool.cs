using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool<T> : MonoBehaviour, IObjectPool<T> where T : MonoBehaviour, IPoolable
{
    [SerializeField]
    private T prefab;

    private Stack<T> reusableInstances = new Stack<T>();

    private T ResetInstance(ref T instance, Transform parent, bool isActive)
    {
        instance.gameObject.SetActive(isActive);
        instance.transform.SetParent(parent);
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localScale = Vector3.one;
        instance.transform.localEulerAngles = Vector3.one;
        return instance;
    }

    public T GetPrefabInstance()
    {
        T instance;
        if (reusableInstances.Count > 0)
        {
            instance = reusableInstances.Pop();
            instance = ResetInstance(ref instance, null, true);
        }
        else
        {
            instance = Instantiate(prefab);
        }

        instance.Origin = this;
        instance.PrepareToUse();

        return instance;
    }

    public void ReturnToPool(T instance)
    {
        instance = ResetInstance(ref instance, transform, false);
        reusableInstances.Push(instance);
    }

    public void ReturnToPool(object instance)
    {
        if (instance is T)
            ReturnToPool(instance as T);
    }
}
