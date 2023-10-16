using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderDish : MonoBehaviour
{
    private DishModule _dishModule;

    public DishEnum DishType;
    public Image Image;

    private void Start()
    {
        //Image = new Image();
    }

    public void SetUpOrderDish(DishModule dishModule, DishEnum dishEnum, Color color)
    {
        _dishModule = dishModule;
        DishType = dishEnum;
        Image.color = color;
    }

    public void Remove()
    {
        DishType = DishEnum.None;
        Image.color = Color.black;
        _dishModule.RemoveFromPool(this);
    }
}