using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanelController : MonoBehaviour
{
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private List<Image> _dishes;
    [SerializeField] private Image _handleImageComponent;

    [SerializeField] private Gradient _gradient;

    private Dictionary<DishEnum, Image> _dishDict = new();
    private float _orderTime = 0.1f;
    private float _currentTime;
    private int _currentDish;

    public void ShowAllDishesInPanel(IReadOnlyList<OrderDish> orderList)
    {
        foreach(var dish in orderList)
        {
            AddDishToPanel(dish);
        }
    }

    public void AddDishToPanel(OrderDish data)
    {
        _dishes[_currentDish].gameObject.SetActive(true);
        _dishes[_currentDish].sprite = data.Sprite;
        _dishes[_currentDish].color = data.Color;
        _dishDict.Add(data.DishEnum, _dishes[_currentDish]);
        _currentDish++;
    }

    public void ResetPanel()
    {
        _dishes.ForEach(x => x.gameObject.SetActive(false));
        _currentDish = 0;
        _dishDict.Clear();
    }

    private void Awake()
    {
        _timerSlider = GetComponentInChildren<Slider>();
        _currentTime = _orderTime;
        SetUpGradient();
        _orderTime = 0.1f;
    }

    public void SwitchOffDish(DishEnum dishEnum)
    {
        if (_dishDict.ContainsKey(dishEnum))
        {
            _dishDict[dishEnum].gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
            ControlTimeScrollbar();
    }

    private void ControlTimeScrollbar()
    {
        // С константой работает, а с переменной нет. И мб тут корутину использовать?
        //_currentTime -= Time.deltaTime / _orderTime;
        //_currentTime -= Time.deltaTime * 0.1f;
        _timerSlider.value -= Time.deltaTime * _orderTime;
        //_timeScrollbar.size -= Time.deltaTime * 0.1f;
        _handleImageComponent.color = _gradient.Evaluate(_timerSlider.value);
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
        _timerSlider.value = 1f;
    }

}
