using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPool<T> where T : Object
{
    public T Spawn();
    public void Despawn(T objectToDespawn);
    public void OnSpawn(T objectToSpawn);
    public void OnDespawn(T objectToDespawn);

}
