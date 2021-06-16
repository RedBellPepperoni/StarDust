using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_LabScientistDoorShieldQuest : NPC_StaticBEhav
{
   [SerializeField] Terminal_GateUnlocker CargoBayTerminal;
    [SerializeField] QuestParent Quest;

    public override void ObjPicked () {
        

        //  NPCDialogueMan.nextDialogue ();

        DisplayAnim.SetBool ("Open", false);

        if (Quest.currentState == QuestParent.QuestProgress.Started) 
        {
            




            if (DialogueUIManager.instance.dialogueMan == NPCDialogueMan) {


                
                checkQuestProgress ();
                DialogueUIManager.instance.Nextsentence ();



            } else {
                

                DialogueUIManager.instance.SetDialoguemanagerReference (NPCDialogueMan);

                checkQuestProgress ();
                DialogueUIManager.instance.Nextsentence ();
            }

        } 

        



    }

    void checkQuestProgress() 
    {
        if (DialogueUIManager.instance.CheckDialogueEnd ()) {
            Quest.ProgressQuest ();

            Invoke ("UnlockDoor", 2);

        }
    }

    void UnlockDoor() 
    {
        CargoBayTerminal.PlayerUnlock ();
        CargoBayTerminal.UnlockAndStayOpen ();
    }
}
