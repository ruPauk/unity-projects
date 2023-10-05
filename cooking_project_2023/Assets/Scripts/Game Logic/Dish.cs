using System.Collections.Generic;
using System.Drawing;

public enum Dish
{
    None = 0,
    Food1,
    Food2,
    Food3,
    Food4
}

public struct DishElement
{
    public Color Color;
    public Dish Dish;
}

/*
public class Test
{
    public List<DishElement> test = new();

    public DishElement Run(Dish dish)
    {
        DishElement element = test.Find(x => { return x.Dish == dish; });

        if(element != default()

        return element;
    }
}
*/