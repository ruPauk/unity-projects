using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = nameof(DishSetter), menuName = "DishSetter/" + nameof(DishSetter))]
public class DishSetter : ScriptableObject
{
    [SerializeField] private List<Settings> _settings;

    public OrderDish GetOrderDish(DishEnum dishEnum)
    {
        var tmp = _settings.FirstOrDefault<Settings>(x => x.DishEnum == dishEnum);
        return new OrderDish()
        {
            DishEnum = dishEnum,
            Color = tmp.Color,
            Sprite = tmp.Sprite
        };
    }

    [Serializable]
    private class Settings
    {
        public DishEnum DishEnum;
        public Color Color;
        public Sprite Sprite;

    }
}
