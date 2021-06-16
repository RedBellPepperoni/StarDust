using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Collectable : Interactable
{
    public QuestParent QuestRef;

    public override void ObjPicked () {

        
        base.ObjPicked ();

        if (canBePicked) 
        {
            if (pickableType == Type.Interactable) 
            {
                QuestRef.ProgressQuest ();

                Delete ();
            } 
            
            else if (pickableType == Type.Pickable)
            {
                if (Gamemanager.instance.CanCarryObject ()) 
                {

                    GetComponent<BoxCollider2D> ().enabled = false;
                    Gamemanager.instance.PickupObject (gameObject);

                   

                    //Delete ();
                }

                else 
                {
                    Debug.LogWarning ("You are already holding another Item");
                }

            }
        }


        else
            Debug.LogError ("Pickyissues");
    }


    public override void Dropped () {
        base.Dropped ();

        

            isPicked = false;
            canBePicked = true;
            GetComponent<BoxCollider2D> ().enabled = true;
           

        
    }
}
