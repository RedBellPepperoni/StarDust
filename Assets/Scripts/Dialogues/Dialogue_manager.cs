using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Dialogue_manager : MonoBehaviour
{
    public TextMeshPro  dialogue;
    [SerializeField] protected bool isQuestDialogue = false;
    [SerializeField] Dialogue[] Dialogues;
    protected int currDialogueindex = 0;


   
    public Dialogue Dialogref;
    public string charName = "";

    [SerializeField]QuestGiver_Parent questGiver;
    private int currentIndex = 0;
    string currenttext = "";



    

    private void Start () {
        setCurrentDialogue (0);
    }

    public void setCurrentDialogue (int index)
    {
        Dialogref = Dialogues[index];
        
    
    }
    private void UpdateDialogueText () 
    {
        dialogue.text = currenttext;
        
    }


    public void nextDialogue() 
    {
        if (currentIndex <= Dialogref.reQuestSentences.Length - 1) 
        {

            currenttext = Dialogref.ReQuestDialogue (currentIndex);
            UpdateDialogueText ();
            currentIndex++;
        }
        else 
        { 
            currentIndex = 0;
           // clearDialogueText ();
        }
    }

    public void clearDialogueText() 
    {
        currenttext = "";
        UpdateDialogueText ();

    }


    public void RandomDialogue()
    {
        if (Dialogref.randomsentences.Length > 0) {
            currenttext = Dialogref.SayRandomDialogue ();
            UpdateDialogueText ();
        }

    }

    public void SaySpecificDialogue(int index) 
    {
        currenttext = Dialogref.SayDialoguebyindex (index);
        UpdateDialogueText ();
    }


    
    public virtual void QuestDialogueCompleted() 
    {
        
        currDialogueindex++;
        Dialogref.DialogueEndQuest ();

         if (currDialogueindex < Dialogues.Length) 
          { setCurrentDialogue (currDialogueindex); }

       

      /*  if(isQuestDialogue) 
        {

            if (questGiver != null) { questGiver.GiveQuest (); }
            isQuestDialogue = false;

            
        
        }*/
    }

    
    
    public bool IsQuestFinished() 
    {
        Debug.LogWarning (questGiver.GetQuestInfo ().currentState);


        return (questGiver.GetQuestInfo ().currentState == QuestParent.QuestProgress.Finished) || (questGiver.GetQuestInfo ().currentState == QuestParent.QuestProgress.Rewarded);
    }
   
}
