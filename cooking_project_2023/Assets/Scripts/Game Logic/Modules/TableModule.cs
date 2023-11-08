using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Data
{
    public bool Flag;
    public DishEnum Dish;
}

public class TableModule : IModule
{
    //public event Action<DishEnum> OnDishTakeAway;
    public event Action<Data> OnDishTakeAwayR;

    public void TakeAwayDish(DishEnum dish)
    {
        //OnDishTakeAway?.Invoke(dish);
        OnDishTakeAwayR?.Invoke(new Data() { Flag = false, Dish = dish });
        Debug.Log($"Takeaway has been invoked with ({dish})");
    }
}


/*
public class TableModule : MonoBehaviour
{
    public event Action<DishEnum> OnDishTakeAway;

    public void TakaAwayDish(DishEnum dish)
    {
        OnDishTakeAway?.Invoke(dish);
        Debug.Log($"Takeaway has been invoked with ({dish})");
    }

    

}
*/