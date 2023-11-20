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
    public event Action<Data> OnDishTakeAwayR;

    public void TakeAwayDish(DishEnum dish)
    {
        OnDishTakeAwayR?.Invoke(new Data() { Flag = false, Dish = dish });
        Debug.Log($"Takeaway has been invoked with ({dish})");
    }
}
