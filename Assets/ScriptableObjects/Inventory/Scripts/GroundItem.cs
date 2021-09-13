using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundItem : Interactable
{
    public UnityEvent Pick;
    public ItemObject item;

    public override void ObjPicked () {
        base.ObjPicked ();

        Pick.Invoke ();
        customFunc ();
        
    }

    protected virtual void customFunc() 
    { 
    
    }
}
