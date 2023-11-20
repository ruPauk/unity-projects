using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private OrdersModule _orders;

    public int VisitorsCount;
    public int DishCount;
    public int Timer;

    public TextAsset levelJson;

    [SerializeField] private DishSetter _dishSetter;
    [SerializeField] private TableSeats _tableSeats;

    void Start()
    {
        _orders = ModuleLocator.GetModule<OrdersModule>();
        _orders.SetUpLevel(JsonConvert.DeserializeObject<JsonObjectNewtonsoft>(levelJson.text),
            _dishSetter);
        ModuleLocator.GetModule<VisitorsModule>().OnVisitorsRunOut += LevelReset;
        //Debug.Log($"Making sure dishSetterr is in Level - {_dishSetter.GetOrderDish(DishEnum.Green).DishEnum}");
    }

    private void LevelReset()
    {
        Debug.Log("LEVEL IS FINISHED");
    }

    private void GameLoop()
    {
        if (_tableSeats.hasFreeSeat && !_orders.IsDone)
            ModuleLocator.GetModule<VisitorsModule>().GetNewVisitor();
    }

    void Update()
    {
        GameLoop();
    }
}
