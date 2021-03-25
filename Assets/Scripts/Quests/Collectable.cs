using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Collider2D))]
public class Collectable : MonoBehaviour
{
    
     public enum Type {Collectable, Pickable, Consumable }
    [SerializeField]protected Type pickableType = Type.Collectable;

    protected bool canBePicked = false;

    protected virtual void OnTriggerEnter2D (Collider2D collision)     
    {
       if(collision.gameObject.tag == "Player" && collision.gameObject.layer == 11) 
       { switch(pickableType) 
         {

                case Type.Collectable:
                    canBePicked = true;
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

        }
    }

    protected virtual void ObjPicked() 
    { 
    



    
    }


    protected void Delete () {
        Destroy (this.gameObject);
    }

}
