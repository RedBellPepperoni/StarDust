using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Collectable : Collectable
{
    QuestParent QuestRef;

    protected override void ObjPicked () 
    {

        base.ObjPicked ();

        if (canBePicked) 
        {
            if (pickableType == Type.Collectable) 
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
