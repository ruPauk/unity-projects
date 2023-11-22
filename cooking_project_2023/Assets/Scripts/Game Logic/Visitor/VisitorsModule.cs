using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class VisitorsModule : IModule
{
    private IObjectPool<VisitorView> _visitorsPool;
    private OrderPanelObjectPool<OrderPanelController> _orderPanelPool;
    private TableSeats _tableSeats;

    private int _visitorsCounter;
    private int _satisfiedVisitorsCounter;
    private int _sadVisitorsCounter;

    public VisitorsModule(
        IObjectPool<VisitorView> visitorsPool,
        OrderPanelObjectPool<OrderPanelController> orderPanelPool)
    {
        _visitorsPool = visitorsPool;
        _orderPanelPool = orderPanelPool;
    }

    public Canvas Canvas { get; private set; }

    public event Action<int, int> OnVisitorsRunOut;
    public event Action<int, int, int> OnVisitorCounterUpdate;

    public void SetUp(TableSeats tableSeats, Canvas canvas)
    {
        _tableSeats = tableSeats;
        Canvas = canvas;
        _visitorsCounter = ModuleLocator.GetModule<OrdersModule>().OverallVisitors;
        OnVisitorCounterUpdate?.Invoke(
           _visitorsCounter,
           _satisfiedVisitorsCounter,
           _sadVisitorsCounter);
    }

    public void GetNewVisitor()
    {
        if (_tableSeats.hasFreeSeat &&
            !ModuleLocator.GetModule<OrdersModule>().IsDone)
        {
            var order = ModuleLocator.GetModule<OrdersModule>().GetNextOrder();
            if (order != null)
            {
                //Debug.Log($"Debugging order dishes - {order.Dishes}");
                var place = _tableSeats.GetFreeSeat();
                if (place is not null)
                {
                    var visitor = new VisitorPresenter(_visitorsPool, _orderPanelPool, new VisitorModel(order), Canvas);
                    visitor.SendVisitorToHisPlace(place, _tableSeats.GetIncomingPath);
                }
            }
        }
    }
    
    public void UtilizeVisitor(VisitorPresenter visitor, Transform seat, bool isVisitorSatisfied)
    {
        _tableSeats.SetSeatFree(seat);
        visitor.GoAwayFromScene(_tableSeats.GetOutgoingPath);
        if (isVisitorSatisfied) 
            ++_satisfiedVisitorsCounter;
        else 
            ++_sadVisitorsCounter;
        OnVisitorCounterUpdate?.Invoke(
            --_visitorsCounter,
            _satisfiedVisitorsCounter,
            _sadVisitorsCounter);
        if (_visitorsCounter <= 0 &&
            ModuleLocator.GetModule<OrdersModule>().IsDone)
        {
            OnVisitorsRunOut?.Invoke(_satisfiedVisitorsCounter, ModuleLocator.GetModule<OrdersModule>().OverallVisitors);
        }
    }    
}