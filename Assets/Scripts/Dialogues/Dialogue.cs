using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable]

public class Dialogue : MonoBehaviour
{

    public UnityEvent DialogueEnd;
   
    public string[] randomsentences;
    public string[] questSentences;
    public string[] reQuestSentences;
   




    public string SayDialoguebyindex(int index)
    {

        return questSentences[index];
    }

    public string SayRandomDialogue() 
    {
        return randomsentences[Random.Range (0, randomsentences.Length)];
    }



    public string ReQuestDialogue(int index) 
    { return reQuestSentences[index]; }


    public void DialogueEndQuest () 
    {
        DialogueEnd.Invoke ();
    }
}
