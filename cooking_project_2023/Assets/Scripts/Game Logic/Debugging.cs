using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging : MonoBehaviour
{
    private bool isPaused = false;
    // Methods for buttons
    public void AddVisitor()
    {
        //Debug.Log("Adding Visitor through the button");
        //ModuleLocator.GetModule<VisitorsModule>().GetNewVisitor();
       
        
        /*if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }     */ 
    }

    public void DeleteVisitor()
    {
       // ModuleLocator.GetModule<VisitorsModule>().DeleteFirstVisitor();

        /*if (_visitorsList.Count > 0)
        {
            var lastVisitor = _visitorsList[0];
            UtilizeVisitor(lastVisitor);
        }*/
    }

    public void FindDish()
    {
        /*
        VisitorView tmp = _visitorsList.Find((x) => x.Order.Dishes.Contains(DishEnum.Yellow));
        if (tmp != null)
        {
            //Debug.Log($"Found ID - {tmp.Id}, Seat - {tmp.Seat}, Order - {String.Join(", ", tmp.Order.Dishes.ToArray())}");
            tmp.RemoveDish(DishEnum.Yellow);
        }*/
    }

}
