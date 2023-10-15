using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _pivotPoint;

    private void LateUpdate()
    {
        var position = Camera.main.WorldToScreenPoint(_pivotPoint.position);
        _transform.position = position;
    }


}
