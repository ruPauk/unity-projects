using System;
using System.Collections.Generic;
using UnityEngine;

public class TablePositionController : MonoBehaviour
{
    const int MAX_CUSTOMER_COUNT = 4;

    [SerializeField] private List<Transform> positions;
    private List<Transform> _customers;
    //True - table is free, false - table is busy
    private List<bool> _tablesAvailability;

    public TablePositionController()
    {
        _customers = new List<Transform>();
        _tablesAvailability = new List<bool>() { true, true, true, true };

        if (this.positions?.Count == 0)
            throw new Exception($"Positions are not set in {this.GetType()}");
    }

    /// <summary>
    /// Returns index of free position. If all positions
    /// are set with customers, it returns -1.
    /// </summary>
    public int nextFreePositionIndex => this._tablesAvailability.IndexOf(true);

    public Transform SetNextPositionBusy(Transform customerPosition)
    {
        var nextFreePosition = nextFreePositionIndex;
        if (nextFreePosition < 0)
            throw new Exception("All table positions are busy right now");
        this._tablesAvailability[nextFreePosition] = false;
        this._customers.Add(customerPosition);
        Debug.Log(string.Join(",", _tablesAvailability));
        return this.positions[nextFreePosition];
    }

    public void SetPositionFree(Transform customerPosition)
    {
        if (customerPosition != null)
        {
            var positionIndex = _customers.IndexOf(customerPosition);
            Debug.Log("PositionIndex = " + positionIndex);
            //Debug.Log(positions[0].gameObject.transform.position);
            //Debug.Log(position.gameObject.transform.position);
            //Debug.Log(position.gameObject.transform.position.Equals(positions[0].gameObject.transform.position));
            if (!this._tablesAvailability[positionIndex])
            {
                this._tablesAvailability[positionIndex] = true;
                Debug.Log(string.Join(",", this._tablesAvailability));
                return;
            }
            else
                throw new Exception("Invalid table position was set to be freed (the position is not set to the table)");
        }
        throw new Exception($"Invalid table position ({customerPosition.GetType()} = NULL) was set to be freed");
    }


}
