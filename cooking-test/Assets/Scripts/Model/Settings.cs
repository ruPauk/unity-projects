using UnityEngine;

[System.Serializable]
public class Settings
{
    [SerializeField] private int _visitorsCount;
    [SerializeField] private int _dishesCount;
    [SerializeField] private float _timer;
    [Range(1,3)]
    [SerializeField] private int _maxDishCount;

    public int visitorsCount => this._visitorsCount;
    public int dishesCount => this._dishesCount;
    public float timer => this._timer;
    public int maxDishCount => this._maxDishCount;
}
