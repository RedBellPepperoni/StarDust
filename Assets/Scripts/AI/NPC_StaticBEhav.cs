using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_StaticBEhav : Interactable
{
    [SerializeField]protected Dialogue_manager NPCDialogueMan;

    [SerializeField]protected Animator NPCAnim;

    bool popUp = false;
  //  [SerializeField]protected bool isQuestGiver = false;
    [SerializeField]protected QuestGiver_Parent QuestGiverRef;

    [SerializeField] Animator PopUpAnim;

    public override void ObjPicked () {
        base.ObjPicked ();

        //  NPCDialogueMan.nextDialogue ();

        DisplayAnim.SetBool ("Open", false);


        if (QuestGiverRef != null) {
            switch (QuestGiverRef.GetQuestInfo ().currentState) {


                case QuestParent.QuestProgress.Disabled:

                    if (DialogueUIManager.instance.dialogueMan != null) { DialogueUIManager.instance.Nextsentence (); } 

                  //  else if (NPCDialogueMan)
                    
                    else {

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


                    if (NPCDialogueMan.Dialogref.reQuestSentences.Length > 0) {
                        if (!popUp) {
                            popUp = true;
                            PopUpAnim.SetBool ("PopUp", true);
                        }

                        NPCDialogueMan.nextDialogue ();
                    }
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
            if(!popUp) 
            {
                popUp = true;
                PopUpAnim.SetBool ("PopUp", true);
            }



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

        if (popUp) {
            popUp = false;
            PopUpAnim.SetBool ("PopUp", false);
        }
    }

}
