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

    public T Spawn(Canvas canvas, Transform pivotPoint)
    {
        var tmp = base.Spawn();
        tmp.transform.SetParent(canvas.transform, true);
        tmp.transform.position = Camera.main.WorldToScreenPoint(pivotPoint.position);
        tmp.transform.localScale = new Vector3(1f, 1f, 1f);
        return tmp;
    }

    public override void OnDespawn(T objectToDespawn)
    {
        (objectToDespawn as OrderPanelController).ResetOrderPanelController();
        base.OnDespawn(objectToDespawn);
    }
}
