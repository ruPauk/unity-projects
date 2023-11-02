using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishModule : MonoBehaviour, IModule
{
    [SerializeField] private Color _greenDishColor;
    [SerializeField] private Color _redDishColor;
    [SerializeField] private Color _blueDishColor;
    [SerializeField] private Color _yellowDishColor;

    [SerializeField] private OrderDishOld _dishPrefab;
    private ObjectPool<OrderDishOld> _dishPrefabPool;

    private void Start()
    {
        _dishPrefabPool = new ObjectPool<OrderDishOld>(_dishPrefab);
    }

    private Color ChooseColorByDish(DishEnum dish)
    {
        switch (dish)
        {
            case DishEnum.None:
                return Color.black;
            case DishEnum.Green:
                return _greenDishColor;
            case DishEnum.Red:
                return _redDishColor;
            case DishEnum.Blue:
                return _blueDishColor;
            case DishEnum.Yellow:
                return _yellowDishColor;
            default:
                return Color.black;
        }
    }

    public OrderDishOld GetColoredDish(DishEnum dish)
    {
        var color = ChooseColorByDish(dish);
        var result = _dishPrefabPool.Spawn();
        result.SetUpOrderDish(this, dish, color);
        //result.spriteRenderer.color = color;
        //result.dishType = dish;

        return result;
    }

    public void RemoveFromPool(OrderDishOld orderDish)
    {
        _dishPrefabPool.Despawn(orderDish);
    }
    




}
