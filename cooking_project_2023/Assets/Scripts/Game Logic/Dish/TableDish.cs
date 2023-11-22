using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableDish : MonoBehaviour
{
    [SerializeField] private DishEnum _dish;

    private void OnMouseDown()
    {
        ModuleLocator.GetModule<TableModule>().TakeAwayDish(_dish);
    }
}
