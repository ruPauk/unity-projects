using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Модуль Orders - Настройка уровня из JSON и отдача заказа + проверка на окончание уровня
public class OrdersModule : IModule
{
    [SerializeField] private List<Order> _orders;
    private int _currentOrderNumber;
    private DishSetter _dishSetter;

    public void SetUpLevel(JsonObjectNewtonsoft gameData, DishSetter dishSetter)
    {
        _orders = gameData.Orders;
        Debug.Log($"Orders count = {_orders.Count}");
        _currentOrderNumber = 0;
        _dishSetter = dishSetter;
    }

    public Order GetOrder()
    {
        Order result = null;
        if (_currentOrderNumber < _orders.Count)
        {
            result = _orders[_currentOrderNumber];
            _currentOrderNumber++;
        }
        return result;
    }

    public List<OrderDish> GetOrderList()
    {
        var tmpOrder = GetOrder();
        var list = new List<OrderDish>();
        Debug.Log($"Orders: {tmpOrder.Dishes.Count}, ");
        //foreach (var item in tmpOrder.Dishes)
        foreach (var item in tmpOrder.Dishes)
        {
            Debug.Log($"Testing dishSetter - {_dishSetter.GetOrderDish(item).DishEnum}, item - {item}");
            list.Add(_dishSetter.GetOrderDish(item));
        }
        return list;
    }
}