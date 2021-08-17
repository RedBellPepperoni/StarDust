using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCall : MonoBehaviour
{
    public UnityEvent Triggercall;

    public bool callOnce;
    bool doOnce;

  
    private void OnTriggerEnter2D (Collider2D collision)
    {
  

           if(callOnce) 
           { 
             if(!doOnce) 
             {
                doOnce = true;
                Triggercall.Invoke ();
               
             }
           }

           else
           Triggercall.Invoke ();

    }
}
