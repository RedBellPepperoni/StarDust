using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]

public class Dialogue : MonoBehaviour
{


   
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

}
