using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class VisitorsModuleR : IModule
{
    private IObjectPool<VisitorView> _visitorsPool;
    private List<VisitorPresenter> _visitorsList;
    private TableSeats _tableSeats;

    public VisitorsModuleR(IObjectPool<VisitorView> visitorsPool)
    {
        _visitorsPool = visitorsPool;
        _visitorsList = new ();
    }

    public void SetUp(TableSeats tableSeats)
    {
        _tableSeats = tableSeats;
    }

    public void GetNewVisitor()
    {
        var order = ModuleLocator.GetModule<OrdersModule>().GetOrder();
        var place = _tableSeats.GetFreeSeat();
        if (place is not null)
        {
            var visitor = new VisitorPresenter(_visitorsPool, new VisitorModel());
            
            //newVisitor.Order = order;
            _visitorsList.Add(visitor);
            visitor.SendVisitorToHisPlace(place, _tableSeats.GetIncomingPath);
            //return visitor;
        }
        //return null;
    }

    
    public void UtilizeVisitor(VisitorPresenter visitor, Transform seat)
    {
        //Поменял здесь входной параметр с VisitorView на VisitorR -> надо все менять теперь везде?
        //Мы же будем с Visitor общаться только через presenter вовне? Тогда надо дописывать еще управление в presenter?
        _tableSeats.SetSeatFree(seat);
        visitor.GoAwayFromScene(_tableSeats.GetOutgoingPath);
    }
    /*
    public void TakeAwayDishHandler(DishEnum dish)
    {
        if (_visitorsList.Count > 0)
        {
            VisitorView tmp = _visitorsList.Find((x) => x.Order.Dishes.Contains(dish));
            if (tmp != null)
            {
                Debug.Log($"Seat - {tmp.Seat}, Order - {String.Join(", ", tmp.Order.Dishes.ToArray())}");
                tmp.RemoveDish(dish);
                if (tmp.Order.Dishes.Count <= 0)
                {
                    UtilizeVisitor(tmp);
                }
            }
        }
    }
    */

}





// Модуль Visitors - Создает посетителей(pool) + создает pool + берет заказ + куда идет?
public class VisitorsModule : MonoBehaviour
{
    [SerializeField] private VisitorView _visitorPrefab;
    [SerializeField] private TableSeats _tableSeats;

    [SerializeField] private Transform[] _incomingPath;
    [SerializeField] private Transform[] _outgoingPath;

    private ObjectPool<VisitorView> _visitorsPool;
    private List<VisitorView> _visitorsList;
    private OrdersModule _orders;
    private DishModule _dishModule;

    private void Start()
    {
        _visitorsPool = new ObjectPool<VisitorView>(_visitorPrefab);
        _visitorsList = new List<VisitorView>();
        _orders = FindObjectOfType<OrdersModule>();
        _dishModule = FindObjectOfType<DishModule>();
       // _tableModule = FindObjectOfType<TableModule>();
        ModuleLocator.GetModule<TableModule>().OnDishTakeAway += TakeAwayDishHandler;
        DOTween.Init(); 
    }

    //Вытаскиваем из пула посетителя, даем ему заказ и отправляем за стол
    public VisitorView GetNewVisitor()
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
    public void UtilizeVisitor(VisitorView visitor)
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

    /*
    private void SendVisitorToHisPlace(VisitorView visitor, Transform place)
    {
        visitor.transform.position = _incomingPath[0].position;
        var sequence = MoveVisitorAlongPath(visitor, _incomingPath, place, () => { 
            visitor.ShowOrder();
            visitor.ShowOrderContent(GetDishesArray(visitor));
        });
        StartCoroutine(sequence);
        visitor.Seat = place;
    }
    */

    private IEnumerator MoveVisitorAlongPath(VisitorView visitor, Transform[] path, Transform destination, Action action)
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

    private OrderDish[] GetDishesArray(VisitorView visitor)
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
            VisitorView tmp = _visitorsList.Find((x) => x.Order.Dishes.Contains(dish));
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
        VisitorView tmp = _visitorsList.Find((x) => x.Order.Dishes.Contains(DishEnum.Yellow));
        if ( tmp != null )
        {
            Debug.Log($"Found ID - {tmp.Id}, Seat - {tmp.Seat}, Order - {String.Join(", ", tmp.Order.Dishes.ToArray())}");
            tmp.RemoveDish(DishEnum.Yellow);
        }
    }
}
