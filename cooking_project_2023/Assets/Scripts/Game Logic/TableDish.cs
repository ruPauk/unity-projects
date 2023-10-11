using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableDish : MonoBehaviour
{
    [SerializeField] private DishEnum _dish;
    [SerializeField] private TableModule _table;

    private void OnMouseDown()
    {
        _table.TakaAwayDish(_dish);
    }

    private void Start()
    {
        if (_table == null)
        {
            _table = this.GetComponentInParent<TableModule>();
        }
    }
}
