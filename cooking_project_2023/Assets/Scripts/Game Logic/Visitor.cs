using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visitor : MonoBehaviour
{
    [SerializeField] private GameObject _orderTable;

    public int Id;
    public Order Order;
    public Transform Seat;

    private void Start()
    {
        HideOrder();
    }

    public void ResetVisitor()
    {
        Id = 0;
        Order = null;
    }

    public void ShowOrder()
    {
        _orderTable.SetActive(true);
    }

    public void HideOrder()
    {
        _orderTable.SetActive(false);
    }

    public void ShowOrderContent(GameObject[] dishes)
    {
        var padding = new Vector3(0, -0.8f, 0);
        foreach (var dish in dishes)
        {
            //_gridLayout.
            dish.transform.parent = _orderTable.transform;
            dish.transform.localScale *= 0.6f;
            dish.transform.position = _orderTable.transform.position + padding;
            padding.y += 0.8f;
        }
    }
}
