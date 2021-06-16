using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorShieldTerminal : TerminalTurrent
{

    [SerializeField] DoorShieldMissionNPC NPCRef;
    [SerializeField] QuestParent Quest;

    bool unlocked;
    protected override void Unlock () {

        if (!unlocked) {
            turrentmanRwef.Activate ();

            Quest.ProgressQuest ();
            NPCRef.TakeCover ();
            unlocked = true;
        }
       
    }

    
}
