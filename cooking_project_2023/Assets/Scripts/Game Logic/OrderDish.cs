using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OrderDish : MonoBehaviour
{
    private DishModule _dishModule;

    public DishEnum dishType;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetUpOrderDish(DishModule dishModule, DishEnum dishEnum, Color color)
    {
        _dishModule = dishModule;
        dishType = dishEnum;
        spriteRenderer.color = color;
    }

    public void Remove()
    {
        dishType = DishEnum.None;
        _dishModule.RemoveFromPool(this);
    }
}