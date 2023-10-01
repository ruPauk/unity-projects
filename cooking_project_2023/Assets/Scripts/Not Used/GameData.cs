using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = nameof(GameData), menuName = "Data/" + nameof(GameData))]
public class GameData : ScriptableObject
{
    [SerializeField] private int _visitiorsCount;
    [SerializeField] private int _dishCount;
    [SerializeField] private int _timer;
    
    public int GetVisitorsCount => _visitiorsCount;

    [ContextMenu("Save")]
    public void SaveToJson()
    {
        JsonObject jsonObject = new JsonObject();
        jsonObject.VisitorsCount = _visitiorsCount;
        jsonObject.DishCount = _dishCount;
        jsonObject.Timer = _timer;
        
        string result = JsonUtility.ToJson(jsonObject);
        File.WriteAllText(Application.streamingAssetsPath + "/jsonObject.json", result);
    }

    [ContextMenu("Load")]
    public void LoadFromJson()
    {
        string result = File.ReadAllText(Application.streamingAssetsPath + "/jsonObject.json");
        var jsonObject = JsonUtility.FromJson<JsonObject>(result);

        _visitiorsCount = jsonObject.VisitorsCount;
        _dishCount = jsonObject.DishCount;
        _timer = jsonObject.Timer;

    }
}

[Serializable]
public class JsonObject
{
    public int VisitorsCount;
    public int DishCount;
    public int Timer;
}