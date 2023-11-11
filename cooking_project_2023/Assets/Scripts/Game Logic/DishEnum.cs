using System.Collections.Generic;
using System.Drawing;

public enum DishEnum
{
    None = 0,
    Burger,
    Fries,
    Pizza,
    Cola
}

public struct DishElement
{
    public Color Color;
    public DishEnum Dish;
}