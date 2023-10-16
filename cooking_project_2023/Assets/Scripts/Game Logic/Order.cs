using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Order
{
    public List<DishEnum> Dishes;
    /* ����� �� �������� ���� �������� ������ OrderDish,
     * �� ��� �� Serializable
     */

    public void RemoveDish(DishEnum dish)
    {
        Dishes.Remove(dish);
    }
}
