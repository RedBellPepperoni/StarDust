using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class NPC_StaticBEhav : Interactable
{
    
    public UnityEvent DidnotInteract;
    public UnityEvent Interacted;

    LayerMask Interactionlayer = 1 << 13;

    [SerializeField]protected Dialogue_manager NPCDialogueMan;

    [SerializeField]protected Animator NPCAnim;

    bool popUp = false;
  //  [SerializeField]protected bool isQuestGiver = false;
    [SerializeField]protected QuestGiver_Parent QuestGiverRef;

    [SerializeField] Animator PopUpAnim;
    [SerializeField] GameObject CharNameText;
    [SerializeField] TextMeshPro charname;


    private void Awake () {
        ShowCharname ();
        charname.text = NPCDialogueMan.charName;
    }

    public override void ObjPicked () {
        
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


                     if (NPCDialogueMan.Dialogref.reQuestSentences.Length > 0) 
                    {
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

                case QuestParent.QuestProgress.Rewarded:
                    NPCDialogueMan.RandomDialogue ();

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



            if (NPCDialogueMan.Dialogref.reQuestSentences.Length > 0) 
            {
                

                NPCDialogueMan.nextDialogue ();
               
            }

            else 
            { NPCDialogueMan.RandomDialogue(); }



        }
        
    }



    protected override void OnTriggerEnter2D (Collider2D collision) {


        base.OnTriggerEnter2D (collision);

        if (collision.CompareTag ("Player")) { HideCharName (); }


        
        
    }

    protected override void OnTriggerExit2D (Collider2D collision) {
        base.OnTriggerExit2D (collision);

        DialogueUIManager.instance.SetDialoguemanagerReference (null);
        NPCDialogueMan.clearDialogueText ();



        if (popUp) {
            popUp = false;
            PopUpAnim.SetBool ("PopUp", false);

        }

        ShowCharname ();
    }


    void ShowCharname() 
    {
        CharNameText.SetActive(true);
    }

    void HideCharName() 
    {
        CharNameText.SetActive (false);
    }


    public void UseNearbyInteractables()
    {
       Collider2D[] hitinteractables = Physics2D.OverlapCircleAll (transform.position, 10,Interactionlayer);

        foreach(Collider2D c in hitinteractables) 
        { 
        
                if(c.gameObject.GetComponent<HealthPickup>()) 
                {
                Destroy (c.gameObject); 
                }

                else if (c.gameObject.GetComponent<LootBox>()) 
                {
                  c.gameObject.GetComponent<LootBox> ().ObjPicked ();
                }


           

        }




        if (hitinteractables == null || hitinteractables.Length == 0) {

            DidnotInteract.Invoke ();
        } else {

            Interacted.Invoke ();

           

        }

    }
}
