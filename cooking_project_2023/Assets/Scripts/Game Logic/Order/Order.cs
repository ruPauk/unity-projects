using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Order
{
    public List<DishEnum> Dishes;

    public void RemoveDish(DishEnum dish)
    {
        Dishes.Remove(dish);
    }
}
