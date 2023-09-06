using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersCounter : MonoBehaviour
{
    private int _customersCount;
    private int _servedCustomers;

    public int customersCount => _customersCount;
    public int servedCustomers => _servedCustomers;

    private void Awake()
    {
        _customersCount = 0;
        _servedCustomers = 0;
    }
    private void Start()
    {
        
    }

    public void ServeCustomer()
    {
        if (IsThereAnyCustomerToServe())
        {
            _servedCustomers++;
            UpdateCounter();
        }
        else
        {
            // EXCEPTION?
            Debug.Log("Too many served customers!");
        }
    }

    // ACCESS??
    public void UpdateCounter()
    {
        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = servedCustomers + " / " + customersCount;
    }

    public bool IsThereAnyCustomerToServe()
    {
        return _customersCount > _servedCustomers;
    }
}
