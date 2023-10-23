using System;
using UnityEngine;

public class VisitorR : IDisposable
{
    private readonly VisitorView View;
    private readonly VisitorModel Model;
    private readonly IObjectPool<VisitorView> VisitorsPool;

    public VisitorR(IObjectPool<VisitorView> visitorPool, VisitorModel model)
    {
        View = visitorPool.Spawn();
        VisitorsPool = visitorPool;
        Model = model;
        Model.OnComplete += CompleteHandler;
    }

    public void Dispose()
    {
        VisitorsPool.Despawn(View);
        Model.Dispose();
    }

    public void SendVisitorToHisPlace(Transform place, Transform[] incPath)
    {
        View.transform.position = incPath[0].position;
        View.StartMovingByPath(incPath, place, () =>
        {
            View.ShowOrder();
            //Зачем это переносить в Model? Это же вроде работа View.
            View.ShowOrderContent(Model.DishList);
            
            //Старая версия
            //View.ShowOrderContent(GetDishesArray(visitor));
        });

        View.Seat = place;
    }

    public void GoAwayFromScene(Transform[] outPath)
    {
        View.transform.position = outPath[0].position;
        View.HideOrder();
        View.StartMovingByPath(outPath, null, () =>
        {
            this.Dispose();
        });
    }

    public void CompleteHandler()
    {
        
    }
}

