using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Visitor : MonoBehaviour
{
    [SerializeField] private GameObject _orderTable;

    public int Id;
    public Order Order;
    public Transform Seat;

    private List<OrderDish> _dishList;

    private void Start()
    {
        HideOrder();
    }

    public void ResetVisitor()
    {
        Id = 0;
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

    public void ShowOrderContent(OrderDish[] dishes)
    {
        _dishList = dishes.ToList();
        var padding = new Vector3(0, -0.8f, 0);
        foreach (var dish in _dishList)
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
