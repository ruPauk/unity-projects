using System.Collections;
using System.Collections.Generic;
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

    private float _orderTime = 0.1f;
    private float _currentTime;

    public OrderPanelController(OrderDish orderDish)
    {

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
