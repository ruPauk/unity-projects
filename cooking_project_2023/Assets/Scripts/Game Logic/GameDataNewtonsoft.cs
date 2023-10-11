using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = nameof(GameDataNewtonsoft), menuName = "DataNewtonsoft/" + nameof(GameDataNewtonsoft))]
public class GameDataNewtonsoft : ScriptableObject
{
    [SerializeField] private JsonObjectNewtonsoft _jsonObject;
    private string _jsonPath = Application.streamingAssetsPath + "/jsonObjectNewtonsoft.json";

    [ContextMenu("Save")]
    public void SaveToJson()
    {
        File.WriteAllText(_jsonPath, JsonConvert.SerializeObject(_jsonObject));
    }

    [ContextMenu("Load")]
    public void LoadFromJson()
    {
        _jsonObject = JsonConvert.DeserializeObject<JsonObjectNewtonsoft>(File.ReadAllText(_jsonPath));
    }

    public List<Order> GetOrders()
    {
        LoadFromJson();
        return _jsonObject.Orders;
    }
}

[Serializable]
public class JsonObjectNewtonsoft : IDisposable
{
    [SerializeField] public List<Order> Orders;

    public int VisitorsCount;
    public int DishCount;
    public int Timer;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}