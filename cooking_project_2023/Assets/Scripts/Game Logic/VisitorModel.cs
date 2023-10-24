using System;
using System.Collections.Generic;

public class VisitorModel : IDisposable
{
    private List<OrderDish> _dishList;
    private float _time;
    //OnComplete дергать же надо, когда модель изменяется? А пока она никак не изменятется.
    //Будто нужно переносить RemoveDish из View в Presenter и оттуда менять модель?
    public event Action OnComplete;

    public VisitorModel()
    {
        ModuleLocator.GetModule<TableModule>().OnDishTakeAwayR += TakeAwayDish;
    }

    ~VisitorModel()
    {
        ModuleLocator.GetModule<TableModule>().OnDishTakeAwayR -= TakeAwayDish;
    }

    public List<OrderDish> DishList
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
            OrderDish temp = _dishList.Find((x) => x.DishType == data.Dish);
            if (temp != null)
            {
                //Debug.Log($"Seat - {temp.Seat}, Order - {String.Join(", ", temp.Order.Dishes.ToArray())}");
                _dishList.Remove(temp);
                if (_dishList.Count <= 0)
                {
                    OnComplete?.Invoke();
                }
            }
        }
    }

    //Зачем в модели Transform здесь? Нам же он во View нужен?
    public void SetUpOrder(Order order)//, Transform place)
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
    }

    public void Dispose()
    {
        ClearDishList();
        OnComplete = null;
    }
}

