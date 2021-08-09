using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestInteractable : Interactable
{
   

    [SerializeField]bool CanProgress = false;


    public UnityEvent ObjPickedCall;

    bool doOnce = false;

    

    public override void ObjPicked () {
        base.ObjPicked ();

        
        
            ObjPickedCall.Invoke ();
            doOnce = true;
        
    }

    


    public void SetCanProgress() 
    {
        CanProgress = true;
        
    }

}
