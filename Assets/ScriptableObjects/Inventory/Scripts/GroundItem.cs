using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundItem : MonoBehaviour
{
    public UnityEvent Pick;
    public ItemObject item;

    protected void OnTriggerEnter2D (Collider2D collision) 
    {
        if(collision.CompareTag("Player")) 
        {
            customFunc ();
            Pick.Invoke ();
            
        }
    }

    protected virtual void customFunc() 
    { 
    
    }
}
