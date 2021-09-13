using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCall : MonoBehaviour
{
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;

    public bool callOnce;
    bool enterOnce;
    bool exitOnce;

  
    private void OnTriggerEnter2D (Collider2D collision)
    {
  

           if(callOnce) 
           { 
             if(!enterOnce) 
             {
                enterOnce = true;
                TriggerEnter.Invoke ();
               
             }
           }

           else
           TriggerEnter.Invoke ();

    }

    private void OnTriggerExit2D (Collider2D collision) 
    {
        if (callOnce) {
            if (!exitOnce) {
                exitOnce = true;
                TriggerExit.Invoke ();

            }
        } else
            TriggerExit.Invoke ();
    }
}
