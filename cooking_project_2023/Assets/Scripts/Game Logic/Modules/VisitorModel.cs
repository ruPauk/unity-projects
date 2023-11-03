using System;
using System.Collections.Generic;

public class VisitorModel : IDisposable
{
    //private List<OrderDish> _dishList;
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

    public IReadOnlyList<OrderDish> DishList
    {
        get => _dishOrderList;
    }

    /*
    private void ClearDishList()
    {
        foreach (var dish in _dishList)
        {
            dish.Remove();
        }
    }*/

    private void TakeAwayDish(Data data)
    {
        if (!data.Flag)
        {
            OrderDish temp = _dishOrderList.Find((x) => x.DishEnum == data.Dish);
            if (!temp.IsEmpty)
            {
                OnTakeDish?.Invoke(data.Dish);
                _dishOrderList.Remove(temp);
                if (_dishOrderList.Count <= 0)
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
        //ClearDishList();
        OnComplete = null;
        OnTakeDish = null;
    }
}

