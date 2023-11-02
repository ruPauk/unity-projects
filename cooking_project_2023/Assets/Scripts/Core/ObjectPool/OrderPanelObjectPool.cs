using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderPanelObjectPool<T> : ObjectPool<T>
    where T : Component
{
    public OrderPanelObjectPool(T prefab) : base(prefab)
    {

    }

    public T Spawn(Canvas canvas)
    {
        var tmp = base.Spawn();
        tmp.transform.SetParent(canvas.transform, false);
        return tmp;
    }
}
