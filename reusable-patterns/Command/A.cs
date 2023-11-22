using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A
{
    public B obj;

    public A(B Bobj) { 
        obj = Bobj;
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //obj.ShowMessage();
            var tmp = new TestCommad();
            tmp.AObj = this;
            tmp.BObj = obj;
            tmp.Execute();
        }
    }*/

    public void Test()
    {
        obj.ShowMessage();
    }
}


public interface ICommand
{
    void Execute();
}

public struct TestCommad : ICommand
{
    public A AObj;
    public B BObj;
    public void Execute()
    {
        BObj.ShowMessage();
        BObj.Method();

    }
}