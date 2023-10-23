using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VisitorModel : IDisposable
{
    private List<OrderDish> _dishList;
    private float _time;
    //OnComplete дергать же надо, когда модель изменяется? А пока она никак не изменятется.
    //Будто нужно переносить RemoveDish из View в Presenter и оттуда менять модель?
    public event Action OnComplete;

    public VisitorModel()
    {
        ModuleLocator.GetModule<TableModule>().OnDishTakeAwayR += TakeAwayDish;
    }

    ~VisitorModel()
    {
        ModuleLocator.GetModule<TableModule>().OnDishTakeAwayR -= TakeAwayDish;
    }

    public List<OrderDish> DishList
    {
        get => _dishList;
    }

    private void ClearDishList()
    {
        foreach (var dish in _dishList)
        {
            dish.Remove();
        }
    }

    private void TakeAwayDish(Data data)
    {
        if (!data.Flag)
        {
            OrderDish temp = _dishList.Find((x) => x.DishType == data.Dish);
            if (temp != null)
            {
                //Debug.Log($"Seat - {temp.Seat}, Order - {String.Join(", ", temp.Order.Dishes.ToArray())}");
                _dishList.Remove(temp);
                if (_dishList.Count <= 0)
                {
                    OnComplete?.Invoke();
                }
            }
        }
    }

    //Зачем в модели Transform здесь? Нам же он во View нужен?
    public void SetUpOrder(Order order)//, Transform place)
    {
        var dishModule = ModuleLocator.GetModule<DishModule>();

        if (_dishList == null)
            _dishList = new();
        else
            ClearDishList();

        foreach (var dish in order.Dishes)
        {
            _dishList.Add(dishModule.GetColoredDish(dish));
        }
    }

    public void Dispose()
    {
        ClearDishList();
        OnComplete = null;
    }
}


public class VisitorView : MonoBehaviour
{
    [SerializeField] private GameObject _orderTable;

    public Order Order;
    public Transform Seat;

    private List<OrderDish> _dishList;

    private void Start()
    {
        HideOrder();
    }

    public void ResetVisitor()
    {
        Order = null;
        Seat = null;
        _dishList = null;
    }

    public void ShowOrder()
    {
        _orderTable.SetActive(true);
    }

    public void HideOrder()
    {
        _orderTable.SetActive(false);
    }

    public void StartMovingByPath(Transform[] path, Transform destination, Action action)
    {
        StartCoroutine(MoveVisitorAlongPath(path, destination, action));
    }

    private IEnumerator MoveVisitorAlongPath(Transform[] path, Transform destination, Action action)
    {
        float speed = 2.5f;
        var sequence = DOTween.Sequence();
        sequence.SetEase(Ease.InOutSine);
        Vector3[] fullPath = new Vector3[path.Length];

        for (int i = 0; i < fullPath.Length; i++)
        {
            fullPath[i] = new Vector3(path[i].position.x, path[i].position.y, path[i].position.z);
        }
        sequence.Append(this.transform.DOPath(fullPath, speed, PathType.Linear));
        if (destination != null)
        {
            sequence.Append(this.transform.DOMove(new Vector3(destination.position.x, destination.position.y, destination.position.z), speed));
        }
        yield return sequence.WaitForCompletion();
        this.ResetVisitor();
        action.Invoke();
    }

    //public void ShowOrderContent(OrderDish[] dishes)
    public void ShowOrderContent(List<OrderDish> dishes)
    {
        //_dishList = dishes.ToList();
        var padding = new Vector3(0, -0.8f, 0);
        foreach (var dish in dishes)
        {
            Debug.Log($"Dish - {dish.DishType}");
            dish.transform.parent = _orderTable.transform;
            //dish.transform.localScale *= 0.6f;
            dish.transform.position = _orderTable.transform.position + padding;
            padding.y += 0.8f;
        }
    }

    public void RemoveDish(DishEnum dish)
    {
        var removingObject = _dishList.Find((x) => x.DishType == dish);
        if (removingObject != null )
        {
            Debug.Log($"Found removing dish - {removingObject.DishType}");
            _dishList.Remove(removingObject);
            removingObject.Remove();
            Order.RemoveDish(dish);
        }
    }
}

