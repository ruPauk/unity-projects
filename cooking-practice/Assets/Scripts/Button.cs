using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Button : MonoBehaviour
{
    
    [SerializeField] ButtonTypes buttonTypes = ButtonTypes.Fries;
    UnityEvent buttonClick;

    void Start()
    {
        if (buttonClick == null)
            buttonClick = new UnityEvent();

        buttonClick.AddListener(Notify);
    }

    void Notify()
    {
        Debug.Log(buttonTypes + " was pressed");
        return;
    }

    private void OnMouseDown()
    {
        buttonClick.Invoke();
    }

    void Update()
    {
        
    }
}
