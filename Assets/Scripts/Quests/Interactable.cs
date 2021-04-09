using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Collider2D))]
public class Interactable : MonoBehaviour
{
    
     public enum Type {Interactable, Pickable, Consumable }
    [SerializeField]protected Type pickableType = Type.Interactable;

    protected bool canBePicked = false;

    protected virtual void OnTriggerEnter2D (Collider2D collision)     
    {
       if(collision.gameObject.tag == "Player" && collision.gameObject.layer == 11) 
       { switch(pickableType) 
         {

                case Type.Interactable:
                    canBePicked = true;

                    Gamemanager.instance.SetintObjRef (this.gameObject);
                    Action_Manager.instance.SetMultiButtonFunc (Action_Manager.MultibtnState.Interact);
                   
                    break;


                case Type.Consumable:
                    ObjPicked ();

                    
                    break;
            
            
        }
    }
    }

    protected void OnTriggerExit2D (Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.layer == 11) {
            canBePicked = false;
            Gamemanager.instance.SetintObjRef (null);
            Action_Manager.instance.SetMultiButtonFunc (Action_Manager.MultibtnState.Interact);


        }
    }

    public virtual void ObjPicked() 
    { 
    



    
    }


    protected void Delete () {
        Destroy (this.gameObject);
    }

}
