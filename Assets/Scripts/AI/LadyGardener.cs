using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyGardener : NPC_StaticBEhav
{
    public override void ObjPicked () {
        base.ObjPicked ();

        NPCDialogueMan.nextDialogue ();


        //if(NPCDialogueMan.)
    }
}
