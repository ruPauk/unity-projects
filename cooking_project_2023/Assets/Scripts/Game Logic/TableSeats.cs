using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSeats : MonoBehaviour
{
    [SerializeField] private List<Transform> _seats;
    private Status[] _seatsStatus;

    void Start()
    {
        _seatsStatus = new Status[4];
    }

    public Transform GetFreeSeat()
    {
        for (int i = 0; i < _seatsStatus.Length; i++)
        {
            if (_seatsStatus[i] == Status.Free)
            {
                _seatsStatus[i] = Status.Occupied;
                return _seats[i];
            }
        }
        return null;
    }

    public void SetSeatFree(Transform seat)
    {
        // Как тут лучше проверять место?
        var tmp = _seats.IndexOf(seat);
        //Debug.Log($"Index = {tmp}");
       // if (tmp >= 0 && tmp < _seats.Count)
        //{
            _seatsStatus[tmp] = Status.Free;
        //}
    }
}

public enum Status
{
    Free = 0,
    Occupied
}
