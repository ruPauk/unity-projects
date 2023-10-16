using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableModule : IModule
{
    public event Action<DishEnum> OnDishTakeAway;

    public void TakeAwayDish(DishEnum dish)
    {
        OnDishTakeAway?.Invoke(dish);
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