using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private Transform initialPosition;
    /// <summary>
    /// Customer's table position ranges from 0 to 3 (following table's count)
    /// Is set to -1 if no table position attached
    /// </summary>
   /* public int tablePosition
    {
        get { return this.tablePosition; }
        set 
        {
            if ((value > -1) && (value < 4))
            {
                Debug.Log($"Is in range- {value}");
                this.tablePosition = value;
            }    
            else
            {
                Debug.Log($"Is out of range- {value}");
                this.tablePosition = -1;
                //throw new Exception($"The position can't be out of [0;3] range (class {this.GetType()})");
            }
                
        }
    }*/

   /* public CustomerController(Transform initialPosition)
    {
        this.initialPosition = initialPosition;
    }*/

    private void Awake()
    {
        //this.tablePosition = -1;
        //this.transform.Translate(new Vector3(5f, 5f, 0));
    }

    private void Start()
    {
        
        
    }

    public bool IsOrderComplete()
    {
        return false;
    }

    public void GetReadyToOrder()
    {

    }

    private void ShowOrder()
    {

    }

    public IEnumerator MoveAlongPathCoroutine(Vector3[] path, float speed, Vector3 destinationPosition, bool isActiveAfterwards)
    {
        //var vectorPath = InitializeWaypoints(path);
        var sequence = DOTween.Sequence();
        sequence.SetEase(Ease.InOutSine);
        sequence.Append(this.transform.DOPath(path, speed, PathType.Linear));
        if (destinationPosition != Vector3.zero)
            sequence.Append(this.transform.DOMove(destinationPosition, speed));
        yield return sequence.WaitForCompletion();
        if (!isActiveAfterwards)
            Deactivate();
    }

    public void Deactivate()
    {
        this.gameObject.transform.position = initialPosition.position;
        this.gameObject.SetActive(false);
    }

    /*private Vector3[] InitializeWaypoints(Transform[] waypoints)
    {
        if (waypoints != null)
        {
            var vectors = new Vector3[waypoints.Length];
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (waypoints[i] != null)
                    vectors[i] = new Vector3(waypoints[i].transform.position.x, waypoints[i].transform.position.y, 0);
                else
                    throw new Exception("Not initialised waypoint is detected");
            }
            return vectors;
        }
        return null;
    }*/
}
