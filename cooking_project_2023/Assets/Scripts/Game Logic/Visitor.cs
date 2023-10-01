using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visitor : MonoBehaviour
{
    public int Id;
    public Order Order;
    public Transform Seat;

    public void ResetVisitor()
    {
        Id = 0;
        Order = null;
    }
}
