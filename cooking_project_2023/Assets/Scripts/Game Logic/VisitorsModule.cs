using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Модуль Visitors - Создает посетителей(pool) + создает pool + берет заказ + куда идет?
public class VisitorsModule : MonoBehaviour
{
    [SerializeField] private Visitor _visitorPrefab;
    [SerializeField] private TableSeats _tableSeats;

    private ObjectPool<Visitor> _visitorsPool;
    private List<Visitor> _visitorsList;
    private OrdersModule _orders;
    private DishModule _dishModule;
    private TableModule _tableModule;

    // Для DoTween нужен путь из Vector'ов, а у меня Transform'ы
    [SerializeField] private Transform[] _incomingPath;
    [SerializeField] private Transform[] _outgoingPath;

    private void Start()
    {
        _visitorsPool = new ObjectPool<Visitor>(_visitorPrefab);
        _visitorsList = new List<Visitor>();
        _orders = FindObjectOfType<OrdersModule>();
        _dishModule = FindObjectOfType<DishModule>();
        _tableModule = FindObjectOfType<TableModule>();
        _tableModule.OnDishTakeAway += TakeAwayDishHandler;
        DOTween.Init(); 
    }

    //Вытаскиваем из пула посетителя, даем ему заказ и отправляем за стол
    public Visitor GetNewVisitor()
    {
        var order = _orders.GetOrder();
        if (order == null)
            return null;
        var place = _tableSeats.GetFreeSeat();
        if (place is not null)
        {
            var newVisitor = _visitorsPool.Spawn();
            newVisitor.Order = order;
            _visitorsList.Add(newVisitor);
            SendVisitorToHisPlace(newVisitor, place);
            return newVisitor;
        }
        return null;
    }

    //Отправляем посетителя домой с выполненным заказом
    public void UtilizeVisitor(Visitor visitor)
    {
        _tableSeats.SetSeatFree(visitor.Seat);
        visitor.HideOrder();
        _visitorsList.Remove(visitor);
        var sequence = MoveVisitorAlongPath(visitor, _outgoingPath, null, () => {
            visitor.ResetVisitor();
            _visitorsPool.Despawn(visitor);
        });
        StartCoroutine(sequence);
    }

    private void SendVisitorToHisPlace(Visitor visitor, Transform place)
    {
        visitor.transform.position = _incomingPath[0].position;
        var sequence = MoveVisitorAlongPath(visitor, _incomingPath, place, () => { 
            visitor.ShowOrder();
            visitor.ShowOrderContent(GetDishesArray(visitor));
        });
        StartCoroutine(sequence);
        visitor.Seat = place;
    }

    private IEnumerator MoveVisitorAlongPath(Visitor visitor, Transform[] path, Transform destination, Action action)
    {
        float speed = 2.5f;
        var sequence = DOTween.Sequence();
        sequence.SetEase(Ease.InOutSine);
        Vector3[] fullPath = new Vector3[path.Length];

        for (int i = 0; i< fullPath.Length; i++)
        {
            fullPath[i] = new Vector3(path[i].position.x, path[i].position.y, path[i].position.z);
        }
        sequence.Append(visitor.transform.DOPath(fullPath, speed, PathType.Linear));
        if (destination != null)
        {
            sequence.Append(visitor.transform.DOMove(new Vector3(destination.position.x, destination.position.y, destination.position.z), speed));
        }
        yield return sequence.WaitForCompletion();
        action.Invoke();
    }

    private OrderDish[] GetDishesArray(Visitor visitor)
    {
        List<OrderDish> tmp = new List<OrderDish>();
        foreach (var dish in visitor.Order.Dishes)
        {
            tmp.Add(_dishModule.GetColoredDish(dish));
        }
        return tmp.ToArray();
    }

    private void TakeAwayDishHandler(DishEnum dish)
    {
        if (_visitorsList.Count > 0)
        {
            Visitor tmp = _visitorsList.Find((x) => x.Order.Dishes.Contains(dish));
            if (tmp != null)
            {
                Debug.Log($"Found ID - {tmp.Id}, Seat - {tmp.Seat}, Order - {String.Join(", ", tmp.Order.Dishes.ToArray())}");
                tmp.RemoveDish(dish);
                if (tmp.Order.Dishes.Count <= 0)
                {
                    UtilizeVisitor(tmp);
                }
            }
        }  
    }

    // Methods for buttons
    public void AddVisitor()
    {
        GetNewVisitor();
    }

    public void DeleteVisitor()
    {
        if ( _visitorsList.Count > 0 )
        {
            var lastVisitor = _visitorsList[0];
            UtilizeVisitor(lastVisitor);
        }        
    }

    public void FindDish()
    {
        Visitor tmp = _visitorsList.Find((x) => x.Order.Dishes.Contains(DishEnum.Yellow));
        if ( tmp != null )
        {
            Debug.Log($"Found ID - {tmp.Id}, Seat - {tmp.Seat}, Order - {String.Join(", ", tmp.Order.Dishes.ToArray())}");
            tmp.RemoveDish(DishEnum.Yellow);
        }
    }
}
