using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private Orders _orders;

    public int VisitorsCount;
    public int DishCount;
    public int Timer;

    public TextAsset levelJson;

    void Start()
    {
        _orders = FindObjectOfType<Orders>();
        _orders.SetUpLevel(JsonConvert.DeserializeObject<JsonObjectNewtonsoft>(levelJson.text));
    }

    void Update()
    {
        
    }
}
