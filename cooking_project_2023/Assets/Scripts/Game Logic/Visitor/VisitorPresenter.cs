using System;
using System.Security.Cryptography;
using UnityEngine;

public class VisitorPresenter : IDisposable
{
    private readonly VisitorView View;
    private readonly VisitorModel Model;
    private readonly IObjectPool<VisitorView> VisitorsPool;
    private readonly OrderPanelObjectPool<OrderPanelController> OrderPanelPool;
    private readonly Canvas PanelCanvas;

    public VisitorPresenter(
        IObjectPool<VisitorView> visitorPool,
        OrderPanelObjectPool<OrderPanelController> orderPanelPool,
        VisitorModel model,
        Canvas canvas)
    {
        View = visitorPool.Spawn();
        Model = model;
        Model.OnComplete += CompleteHandler;
        Model.OnTakeDish += TakeAwayDish;
        VisitorsPool = visitorPool;
        OrderPanelPool = orderPanelPool;
        PanelCanvas = canvas;
    }

    public void Dispose()
    {
        VisitorsPool.Despawn(View);
        OrderPanelPool.Despawn(View.OrderTable);
        Model.Dispose();
    }

    public void SendVisitorToHisPlace(Transform place, Transform[] incPath)
    {
        View.transform.position = incPath[0].position;
        View.StartMovingByPath(incPath, place, () =>
        {
            var orderPanel = OrderPanelPool.Spawn(PanelCanvas, View.PivotOrderTable);
            orderPanel.OnTimerEnd += TimerOffHandler;
            View.SetOrderTable(orderPanel);
            View.OrderTable.ShowAllDishesInPanel(Model.DishList);
            View.ShowOrder();
            Model.IsVisitorReady = true;
        });
        View.Seat = place;
    }

    public void GoAwayFromScene(Transform[] outPath)
    {
        View.HideOrder();
        View.StartMovingByPath(outPath, null, () =>
        {
            this.Dispose();
        });
    }

    public void TimerOffHandler()
    {
        ModuleLocator.GetModule<VisitorsModule>().UtilizeVisitor(this, View.Seat, false);
    }

    public void CompleteHandler()
    {
        ModuleLocator.GetModule<VisitorsModule>().UtilizeVisitor(this, View.Seat, true);
    }

    private void TakeAwayDish(DishEnum dishEnum)
    {
        View.OrderTable.SwitchOffDish(dishEnum);
    }
}

