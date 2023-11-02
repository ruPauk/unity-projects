using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VisitorView : MonoBehaviour
{
    [SerializeField] private GameObject _orderTable;
    [SerializeField] private Canvas _orderPanelCanvas { get; set; }

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
            Debug.Log($"Dish - {dish.DishEnum}");
            dish.transform.parent = _orderTable.transform;
            //dish.transform.localScale *= 0.6f;
            dish.transform.position = _orderTable.transform.position + padding;
            padding.y += 0.8f;
        }
    }

    public void RemoveDish(DishEnum dish)
    {
        var removingObject = _dishList?.Find((x) => x.DishEnum == dish);
        if (removingObject != null )
        {
            Debug.Log($"Found removing dish - {removingObject.DishType}");
            _dishList.Remove(removingObject);
            removingObject.Remove();
            Order.RemoveDish(dish);
        }
    }
}

