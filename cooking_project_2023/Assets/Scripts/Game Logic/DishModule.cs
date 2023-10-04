using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishModule : MonoBehaviour
{
    [SerializeField] private Color _greenDishColor;
    [SerializeField] private Color _redDishColor;
    [SerializeField] private Color _blueDishColor;
    [SerializeField] private Color _yellowDishColor;

    [SerializeField] private SpriteRenderer _dishPrefab;
    private ObjectPool<SpriteRenderer> _dishPrefabPool;


    private void Start()
    {
        _dishPrefabPool = new ObjectPool<SpriteRenderer>(_dishPrefab);
    }

    private Color ChooseColorByDish(Dish dish)
    {
        switch (dish)
        {
            case Dish.None:
                return Color.black;
            case Dish.Food1:
                return _greenDishColor;
            case Dish.Food2:
                return _redDishColor;
            case Dish.Food3:
                return _blueDishColor;
            case Dish.Food4:
                return _yellowDishColor;
            default:
                return Color.black;
        }
    }

    public GameObject GetColoredDish(Dish dish)
    {
        var color = ChooseColorByDish(dish);
        var result = _dishPrefabPool.Spawn();
        result.color = color;

        return result.gameObject;
    }

    




}
