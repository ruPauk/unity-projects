using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool<T> : IObjectPool<T>
    where T : Component
{
    protected Queue<T> _objectPool;
    protected T _prefab;

    public ObjectPool(T prefab)
    {
        _objectPool = new Queue<T>();
        _prefab = prefab;
    }

    public void Despawn(T objectToDespawn)
    {
        OnDespawn(objectToDespawn);
        _objectPool.Enqueue(objectToDespawn);
    }

    public virtual void OnDespawn(T objectToDespawn) {
        objectToDespawn.gameObject.SetActive(false);
    }
    public virtual void OnSpawn(T objectToSpawn) {
        objectToSpawn.gameObject.SetActive(true);
    }

    public T Spawn()
    {
        T objectToSpawn;
        if (_objectPool.Count > 0)
        {
            objectToSpawn = _objectPool.Dequeue();
        }
        else
        {
            objectToSpawn = GameObject.Instantiate(_prefab);
        }
        OnSpawn(objectToSpawn);
        return objectToSpawn;
    }
}
