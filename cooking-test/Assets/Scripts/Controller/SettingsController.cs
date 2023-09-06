using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SettingsController : MonoBehaviour
{
    public Settings settings;
    public bool useCustomLevel = false;
    public List<OrderModel> orders;
    private string configPath = Application.streamingAssetsPath + "/Configuration.json";

    [ContextMenu("Load Settings")]
    public void LoadSettings()
    {
        settings = JsonUtility.FromJson<Settings>(File.ReadAllText(configPath));
    }
    
    [ContextMenu("Save Settings")]
    public void SaveSettings()
    {
        File.WriteAllText(configPath, JsonUtility.ToJson(settings));
    }
}
