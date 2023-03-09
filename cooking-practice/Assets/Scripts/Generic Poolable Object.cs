using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPoolableObject : MonoBehaviour, IPoolable
{
    public IObjectPool Origin { get; set; }

    public void PrepareToUse()
    {
        Debug.Log("Preparing the object for usage...");
    }

    public void ReturnToPool()
    {
        Origin.ReturnToPool(this);
    }
}