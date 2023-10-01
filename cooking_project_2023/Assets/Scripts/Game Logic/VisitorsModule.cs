using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ Visitors - ������� �����������(pool) + ������� pool + ����� ����� + ���� ����?
public class VisitorsModule : MonoBehaviour
{
    [SerializeField] private Visitor _visitorPrefab;
    [SerializeField] private TableSeats _tableSeats;

    private ObjectPool<Visitor> _visitorsPool;
    private List<Visitor> _visitorsList;
    private Orders _orders;

    // ��� DoTween ����� ���� �� Vector'��, � � ���� Transform'�
    //[SerializeField] private Vector2[] _incomingPath;
    //[SerializeField] private Vector2[] _outgoingPath;

    private void Start()
    {
        _visitorsPool = new ObjectPool<Visitor>(_visitorPrefab);
        _visitorsList = new List<Visitor>();
        _orders = FindObjectOfType<Orders>();
        //_orders.SetUpLevel();
    }

    public Visitor GetNewVisitor()
    {
        var place = _tableSeats.GetFreeSeat();
        if (place is not null)
        {
            var newVisitor = _visitorsPool.Spawn();
            _visitorsList.Add(newVisitor);
            SendVisitorToHisPlace(newVisitor, place);
            return newVisitor;
        }
        return null;
    }

    public void UtilizeVisitor(Visitor visitor)
    {
        //��� �� ������ ��� ������������ transform
        _tableSeats.SetSeatFree(visitor.Seat);
        visitor.ResetVisitor();
        _visitorsList.Remove(visitor);
        _visitorsPool.Despawn(visitor);
    }

    public void SendVisitorToHisPlace(Visitor visitor, Transform place)
    {
        visitor.Seat = place;
        visitor.transform.position = place.position;
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
