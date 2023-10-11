using System.Collections.Generic;
using System.Drawing;

public enum DishEnum
{
    None = 0,
    Green,
    Red,
    Blue,
    Yellow
}

public struct DishElement
{
    public Color Color;
    public DishEnum Dish;
}