using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    [SerializeField] protected string ActiveateObjname;

    List<GameObject> ObjectsonPlate = new List<GameObject>();


   protected void OnTriggerEnter2D (Collider2D collision) {
       
        
        
        if(collision.gameObject.GetComponent<ItemIdentifier>()&&collision.gameObject.tag=="MiscItem") 
        { 
          if(collision.gameObject.GetComponent<ItemIdentifier>().GetItemname() == ActiveateObjname) 
          {


                ObjectsonPlate.Add (collision.gameObject);

                OpenDoor ();
            
          }
        
        }


        else if(collision.gameObject.tag=="Player"&&collision.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            


                ObjectsonPlate.Add (collision.gameObject);

                OpenDoor ();

            

        }
    }


     protected void OnTriggerExit2D (Collider2D collision) 
     {
        if (collision.gameObject.GetComponent<ItemIdentifier> () && collision.gameObject.tag == "MiscItem") {
            if (collision.gameObject.GetComponent<ItemIdentifier> ().GetItemname () == ActiveateObjname) {
                ObjectsonPlate.Remove (collision.gameObject);


                if (ObjectsonPlate.Count == 0) { CloseDoor (); }

            }

        } else if (collision.gameObject.tag == "Player" && collision.gameObject.layer == LayerMask.NameToLayer ("Player")) {



            ObjectsonPlate.Remove (collision.gameObject);

            if (ObjectsonPlate.Count == 0) { CloseDoor (); }



        }
    }


    protected virtual void OpenDoor() 
    { 
    
    }

    protected virtual void CloseDoor() 
    { 
    
    }
}
