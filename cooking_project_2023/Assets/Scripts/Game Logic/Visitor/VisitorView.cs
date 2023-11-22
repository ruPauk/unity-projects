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
    [SerializeField] private Transform _pivotPoint;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private OrderPanelController _orderTable;
    public Transform PivotOrderTable => _pivotPoint;
    public Transform Seat;

    private List<OrderDish> _dishList;

    public OrderPanelController OrderTable => _orderTable;

    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = _sprites[UnityEngine.Random.Range(0, _sprites.Count)];
    }

    public void SetOrderTable(OrderPanelController orderTable)
    {
        _orderTable = orderTable;
    }

    public void ShowOrder()
    {
        _orderTable.gameObject.SetActive(true);
    }

    public void HideOrder()
    {
        _orderTable.gameObject.SetActive(false);
    }

    public void StartMovingByPath(Transform[] path, Transform destination, Action action)
    {
        StartCoroutine(MoveVisitorAlongPath(path, destination, action));
        Seat = destination;
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
        action.Invoke();
    }
}

