using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueueContoller : MonoBehaviour
{
    const int INITIAL_POOL_SIZE = 4;

    [SerializeField] private CustomerController customerPrefab;
    [SerializeField] private OrderController orderPrefab;
    [SerializeField] private Transform[] wayInPath;
    [SerializeField] private Transform[] wayOutPath;
    [SerializeField] private float comingInDuration;
    [SerializeField] private float comingOutDuration;
    [SerializeField] private TablePositionController tablePositionController;
    [SerializeField] private Transform initialCustomerPosition;

    private Pool<CustomerController> _customerPool;
    //private Pool<OrderController> _orderPool;

    private List<CustomerController> _currentCustomers;

    private Vector3[] inVectorPath;
    private Vector3[] OutVectorPath;

    private void Awake()
    {
        tablePositionController = FindObjectOfType<TablePositionController>();

        _currentCustomers = new List<CustomerController>();
        _customerPool = new Pool<CustomerController>(customerPrefab, INITIAL_POOL_SIZE, this.transform, true);
        //_orderPool = new Pool<OrderController>(orderPrefab, INITIAL_POOL_SIZE, true);
    }

    private void Start()
    {
        
        inVectorPath = InitializeWaypoints(wayInPath);
        OutVectorPath = InitializeWaypoints(wayOutPath);
        //var customer = GameObject.Instantiate(customerPrefab, wayInPath[0].position, Quaternion.identity);
        //customer.MoveAlongPath(path, duration, tablePositionController.SetNextPositionBusy().position);
        //Debug.Log("Customer has ben created!");

    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            //var newCustomer = GameObject.Instantiate(customerPrefab, wayInPath[0].position, Quaternion.identity, this.transform);


            if (_currentCustomers.Count < INITIAL_POOL_SIZE)
            {
                var newCustomer = _customerPool.GetFreeElement();
                var tablePosition = tablePositionController.SetNextPositionBusy(newCustomer.transform).position;

                _currentCustomers.Add(newCustomer);
                StartCoroutine(newCustomer.MoveAlongPathCoroutine(inVectorPath, comingInDuration, tablePosition, true));
            }
           
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (_currentCustomers.Count > 0)
            {
                var removableCustomer = _currentCustomers[0];
                tablePositionController.SetPositionFree(removableCustomer.transform);
                _currentCustomers.Remove(removableCustomer);
                StartCoroutine(removableCustomer.MoveAlongPathCoroutine(OutVectorPath, comingOutDuration, OutVectorPath[OutVectorPath.Length - 1], false));
            }
            
        }
        
    }

    private Vector3[] InitializeWaypoints(Transform[] waypoints)
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
    }
}
