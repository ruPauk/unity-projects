using UnityEngine;

public struct OrderDish
{
    public DishEnum DishEnum;
    public Color Color;
    public Sprite Sprite;

    public void Deconstruct(out Color color, out Sprite sprite)
    {
        color = Color;
        sprite = Sprite;
    }
}