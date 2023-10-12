using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishModule : MonoBehaviour
{
    [SerializeField] private Color _greenDishColor;
    [SerializeField] private Color _redDishColor;
    [SerializeField] private Color _blueDishColor;
    [SerializeField] private Color _yellowDishColor;

    [SerializeField] private OrderDish _dishPrefab;
    private ObjectPool<OrderDish> _dishPrefabPool;

    private void Start()
    {
        _dishPrefabPool = new ObjectPool<OrderDish>(_dishPrefab);
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

    public OrderDish GetColoredDish(DishEnum dish)
    {
        var color = ChooseColorByDish(dish);
        var result = _dishPrefabPool.Spawn();
        result.SetUpOrderDish(this, dish, color);
        //result.spriteRenderer.color = color;
        //result.dishType = dish;

        return result;
    }

    public void RemoveFromPool(OrderDish orderDish)
    {
        _dishPrefabPool.Despawn(orderDish);
    }
    




}
