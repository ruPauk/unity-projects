using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Order
{
    public List<DishEnum> Dishes;
    /* Будто бы хотелось сюда добавить список OrderDish,
     * но это же Serializable
     */

    public void RemoveDish(DishEnum dish)
    {
        Dishes.Remove(dish);
    }
}
