using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanelController : MonoBehaviour
{
    [SerializeField] private Scrollbar _timeScrollbar;
    [SerializeField] private List<Image> _dishes;
    [SerializeField] private Image _handleImageComponent;

    [SerializeField] private Gradient _gradient;
    //[SerializeField] private List<Color> _scrollbarColors;
    //Как тут лучше организовать соответствие между раскраской _dishes и заказом?

    private Dictionary<DishEnum, Image> _dishDict = new();
    private float _orderTime = 0.1f;
    private float _currentTime;

    public OrderPanelController(OrderDishOld orderDish)
    {

    }

    public void AddDishToPanel(OrderDish data)
    {
        var nextDish = _dishes.FirstOrDefault(x => x.gameObject.activeSelf == false);
        if (nextDish != null)
        {
            (Color color, Sprite sprite) = data;
            nextDish.color = color;
            nextDish.sprite = sprite;
            nextDish.gameObject.SetActive(true);
            _dishDict.Add(data.DishEnum, nextDish);
        }
        else
        {
            Debug.Log("You are trying to add a dish to a panel even though all dishes are already set. Need a check on what's going on.");
        }

    }

    public void ResetPanel()
    {
        _dishes.ForEach(x => x.gameObject.SetActive(false));
        _dishDict.Clear();
    }

    private void Awake()
    {
        _timeScrollbar = GetComponentInChildren<Scrollbar>();
        _currentTime = _orderTime;
        SetUpGradient();
        _orderTime = 0.1f;

    }

    private void LateUpdate()
    {
       // if (_orderTime > 0)
       // {
            ControlTimeScrollbar();
        //}
        
    }

    private void ControlTimeScrollbar()
    {
        // С константой работает, а с переменной нет. И мб тут корутину использовать?
        //_currentTime -= Time.deltaTime / _orderTime;
        //_currentTime -= Time.deltaTime * 0.1f;
        _timeScrollbar.size -= Time.deltaTime * _orderTime;
        //_timeScrollbar.size -= Time.deltaTime * 0.1f;
        _handleImageComponent.color = _gradient.Evaluate(_timeScrollbar.size);

    }

    private void SetUpGradient()
    {
        _gradient = new Gradient();
        var colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Color.red, 0.0f);
        colors[1] = new GradientColorKey(Color.green, 1.0f);
        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 1.0f);
        alphas[1] = new GradientAlphaKey(1.0f, 1.0f);
        _gradient.SetKeys(colors, alphas);
    }

    private void ResetOrderPanelController()
    {
        _timeScrollbar.size = 1f;
    }

}
