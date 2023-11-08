using System;
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
        Model.Dispose();
    }

    public void SendVisitorToHisPlace(Transform place, Transform[] incPath)
    {
        View.transform.position = incPath[0].position;
        View.StartMovingByPath(incPath, place, () =>
        {
            //var orderPanel = OrderPanelPool.Spawn(PanelCanvas, View.PivotOrderTable);
            //orderPanel.transform.localScale = new Vector3 (0.1f, 0.1f, 1);
            //View.SetOrderTable(orderPanel);

            View.SetOrderTable(OrderPanelPool.Spawn(PanelCanvas, View.PivotOrderTable));
            View.OrderTable.ShowAllDishesInPanel(Model.DishList);
            View.ShowOrder();
            //View.ShowOrderContent(Model.DishList);
        });

        View.Seat = place;
    }

    public void GoAwayFromScene(Transform[] outPath)
    {
        //View.transform.position = outPath[0].position;
        View.HideOrder();
        View.StartMovingByPath(outPath, null, () =>
        {
            this.Dispose();
        });
    }

    public void CompleteHandler()
    {
        ModuleLocator.GetModule<VisitorsModule>().UtilizeVisitor(this, View.Seat);
    }

    private void TakeAwayDish(DishEnum dishEnum)
    {
        View.OrderTable.SwitchOffDish(dishEnum);
    }
}

