using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_StaticBEhav : Interactable
{
    [SerializeField]protected Dialogue_manager NPCDialogueMan;

    


  //  [SerializeField]protected bool isQuestGiver = false;
    [SerializeField]protected QuestGiver_Parent QuestGiverRef;

    

    public override void ObjPicked () {
        base.ObjPicked ();

        //  NPCDialogueMan.nextDialogue ();

        DisplayAnim.SetBool ("Open", false);


        if (QuestGiverRef != null) {
            switch (QuestGiverRef.GetQuestInfo ().currentState) {


                case QuestParent.QuestProgress.Disabled:

                    if (DialogueUIManager.instance.dialogueMan != null) { DialogueUIManager.instance.Nextsentence (); } else {

                        DialogueUIManager.instance.SetDialoguemanagerReference (NPCDialogueMan);
                        DialogueUIManager.instance.Nextsentence ();
                    }


                    break;

                case QuestParent.QuestProgress.Started:
                    
                    if (QuestGiverRef.GetQuestInfo ().IsReached()) 
                    {
                        NPCDialogueMan.setCurrentDialogue (1);
                    }
                        QuestGiverRef.GetQuestInfo ().isQuestComplete ();

                    NPCDialogueMan.nextDialogue ();
                    break;

                case QuestParent.QuestProgress.Finished:

                    

                    if (DialogueUIManager.instance.dialogueMan != null) { DialogueUIManager.instance.Nextsentence (); } else {

                        DialogueUIManager.instance.SetDialoguemanagerReference (NPCDialogueMan);
                        DialogueUIManager.instance.Nextsentence ();
                    }
                    break;
            }

        }
        else 
        {
            if (NPCDialogueMan.Dialogref.reQuestSentences.Length > 0) { NPCDialogueMan.nextDialogue (); }

            else 
            { NPCDialogueMan.RandomDialogue(); }



        }
        
    }



    protected override void OnTriggerEnter2D (Collider2D collision) {
        base.OnTriggerEnter2D (collision);
        
    }

    protected override void OnTriggerExit2D (Collider2D collision) {
        base.OnTriggerExit2D (collision);

        DialogueUIManager.instance.SetDialoguemanagerReference (null);
        NPCDialogueMan.clearDialogueText ();
    }

}
