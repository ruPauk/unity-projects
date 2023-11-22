using System;
using System.Collections.Generic;
using UnityEngine;

public class VisitorModel : IDisposable
{
    public bool IsVisitorReady;

    private List<OrderDish> _dishOrderList;
    private float _time;

    public event Action OnComplete;
    public event Action<DishEnum> OnTakeDish;
    

    public VisitorModel(Order order)
    {
        IsVisitorReady = false;
        ModuleLocator.GetModule<TableModule>().OnDishTakeAwayR += TakeAwayDish;
        _dishOrderList = ModuleLocator.GetModule<OrdersModule>().GetOrderDishesList(order);
    }

    ~VisitorModel()
    {
        ModuleLocator.GetModule<TableModule>().OnDishTakeAwayR -= TakeAwayDish;
    }

    public IReadOnlyList<OrderDish> DishList
    {
        get => _dishOrderList;
    }

    private void TakeAwayDish(Data data)
    {
        if (!data.Flag && IsVisitorReady)
        {
            OrderDish temp = _dishOrderList.Find((x) => x.DishEnum == data.Dish);
            if (!temp.IsEmpty)
            {
                OnTakeDish?.Invoke(data.Dish);
                data.Flag = true;
                _dishOrderList.Remove(temp);
                if (_dishOrderList.Count <= 0)
                {
                    OnComplete?.Invoke();
                }
            }
        }
    }

    public void Dispose()
    {
        OnComplete = null;
        OnTakeDish = null;
    }
}

