using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestInteractable : Interactable
{
   

    


    public UnityEvent ObjPickedCall;

    protected bool doOnce = false;

    

    public override void ObjPicked () {
        base.ObjPicked ();

        
        
            ObjPickedCall.Invoke ();
           
        
    }

    


    

}
