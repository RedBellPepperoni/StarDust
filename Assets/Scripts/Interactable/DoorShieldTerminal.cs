using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorShieldTerminal : TerminalTurrent
{

    [SerializeField] DoorShieldMissionNPC NPCRef;
    

    bool unlocked;


     void Start () {
        if (DisplayUIRef != null)
            DisplayAnim = DisplayUIRef.GetComponent<Animator> ();

        turrentmanRwef.Activate ();
    }
    protected override void Unlock () {

        if (!unlocked) {

            if(turrentmanRwef.isActiveAndEnabled)
            turrentmanRwef.Deactivate ();

            
           // NPCRef.TakeCover ();
            unlocked = true;
        }
       
    }

    
}
