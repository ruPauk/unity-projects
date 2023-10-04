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

    void Start()
    {
        _orders = FindObjectOfType<OrdersModule>();
        _orders.SetUpLevel(JsonConvert.DeserializeObject<JsonObjectNewtonsoft>(levelJson.text));
    }

    void Update()
    {
        
    }
}
