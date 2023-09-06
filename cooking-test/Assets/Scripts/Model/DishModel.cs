using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishModel
{
    public enum DishColor { Blue , Green, Red, Yellow};
    private DishColor color;

    public DishModel(DishColor color)
    {
        this.color = color;
    }
}
