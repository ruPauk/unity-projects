using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private OrdersModule _orders;

    public int VisitorsCount;
    public int DishCount;
    public int Timer;

    public TextAsset levelJson;

    [SerializeField] private DishSetter _dishSetter;
    [SerializeField] private TableSeats _tableSeats;
    [SerializeField] private UIModule _uiModule;

    void Start()
    {
        _orders = ModuleLocator.GetModule<OrdersModule>();
        _orders.SetUpLevel(JsonConvert.DeserializeObject<JsonObjectNewtonsoft>(levelJson.text),
            _dishSetter);
        _uiModule.OnResetButtonClick.AddListener(LevelReset);
    }

    private void LevelReset()
    {
        SceneManager.LoadScene(0);
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
