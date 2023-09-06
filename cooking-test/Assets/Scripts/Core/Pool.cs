using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }

    private List<T> _pool;

    public Pool(T prefab, int count, bool autoExpand)
    {
        this.prefab = prefab;
        this.container = null; // prefab.transform;
        this.CreatePool(count);
        this.autoExpand = autoExpand;
    }

    public Pool(T prefab, int count, Transform container, bool autoExpand)
    {
        this.prefab = prefab;
        this.container = container;
        this.CreatePool(count);
        this.autoExpand = autoExpand;
    }

    private void CreatePool(int count)
    {
        this._pool = new List<T>();
        for (int i = 0; i < count; i++)
            this.CreateObject();
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        //var createdObject = Object.Instantiate(this.prefab, this.container); ???
        var createdObject = GameObject.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this._pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach(var obj in this._pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                element = obj;
                obj.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;
        if (this.autoExpand)
            return this.CreateObject(true);
        throw new Exception($"There is no free element in the pool of type {typeof(T)}");
    }

}
