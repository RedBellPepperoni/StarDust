using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Collectable : Interactable
{
    QuestParent QuestRef;

    public override void ObjPicked () 
    {

        base.ObjPicked ();

        if (canBePicked) 
        {
            if (pickableType == Type.Interactable) 
            {
                QuestRef.ProgressQuest ();

                Delete ();
            } 
            
            else
            {
                if (Gamemanager.instance.CanCarryObject ()) 
                {
                    Gamemanager.instance.PickupObject (this.gameObject);

                    Delete ();
                }

                else 
                {
                    Debug.LogWarning ("You are already holding another Item");
                }

            }
        }
    }
}
