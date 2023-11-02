using System;
using System.Collections.Generic;

public class VisitorModel : IDisposable
{
    private List<OrderDishOld> _dishList;
    private List<OrderDish> _dishOrderList;
    private float _time;
    public event Action OnComplete;
    public event Action<DishEnum> OnTakeDish;

    public VisitorModel()
    {
        ModuleLocator.GetModule<TableModule>().OnDishTakeAwayR += TakeAwayDish;
        _dishOrderList = ModuleLocator.GetModule<OrdersModule>().GetOrderList();
    }

    ~VisitorModel()
    {
        ModuleLocator.GetModule<TableModule>().OnDishTakeAwayR -= TakeAwayDish;
    }

    public List<OrderDishOld> DishList
    {
        get => _dishList;
    }

    private void ClearDishList()
    {
        foreach (var dish in _dishList)
        {
            dish.Remove();
        }
    }

    private void TakeAwayDish(Data data)
    {
        if (!data.Flag)
        {
            OrderDishOld temp = _dishList.Find((x) => x.DishType == data.Dish);
            if (temp != null)
            {
                OnTakeDish?.Invoke(data.Dish);
                _dishList.Remove(temp);
                if (_dishList.Count <= 0)
                {
                    OnComplete?.Invoke();
                }
            }
        }
    }

    /*public void SetUpOrder(Order order)
    {
        var dishModule = ModuleLocator.GetModule<DishModule>();

        if (_dishList == null)
            _dishList = new();
        else
            ClearDishList();

        foreach (var dish in order.Dishes)
        {
            _dishList.Add(dishModule.GetColoredDish(dish));
        }
    }*/

    public void Dispose()
    {
        ClearDishList();
        OnComplete = null;
    }
}

