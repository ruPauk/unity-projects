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

    // Для DoTween нужен путь из Vector'ов, а у меня Transform'ы
    [SerializeField] private Transform[] _incomingPath;
    [SerializeField] private Transform[] _outgoingPath;

    private void Start()
    {
        _visitorsPool = new ObjectPool<Visitor>(_visitorPrefab);
        _visitorsList = new List<Visitor>();
        _orders = FindObjectOfType<OrdersModule>();
        _dishModule = FindObjectOfType<DishModule>();
        DOTween.Init();

    }

    public Visitor GetNewVisitor()
    {
        var place = _tableSeats.GetFreeSeat();
        if (place is not null)
        {
            var newVisitor = _visitorsPool.Spawn();
            newVisitor.Order = _orders.GetOrder();
            _visitorsList.Add(newVisitor);
            SendVisitorToHisPlace(newVisitor, place);
            return newVisitor;
        }
        return null;
    }

    public void UtilizeVisitor(Visitor visitor)
    {
        _tableSeats.SetSeatFree(visitor.Seat);

        var sequence = MoveVisitorAlongPath(visitor, _outgoingPath, null);
        StartCoroutine(sequence);

        //visitor.ResetVisitor();
        //_visitorsList.Remove(visitor);
        //_visitorsPool.Despawn(visitor);
    }

    public void SendVisitorToHisPlace(Visitor visitor, Transform place)
    {
        visitor.transform.position = _incomingPath[0].position;
        var sequence = MoveVisitorAlongPath(visitor, _incomingPath, place);
        StartCoroutine(sequence);
        visitor.Seat = place;
        //visitor.transform.position = place.position;
        visitor.ShowOrderContent(GetDishesArray(visitor));
        
    }

    private IEnumerator MoveVisitorAlongPath(Visitor visitor, Transform[] path, Transform destination)
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
        visitor.ShowOrder();

    }

    private GameObject[] GetDishesArray(Visitor visitor)
    {
        List<GameObject> tmp = new List<GameObject>();
        foreach (var dish in visitor.Order.Dishes)
        {
            tmp.Add(_dishModule.GetColoredDish(dish));
        }
        return tmp.ToArray();
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
}
