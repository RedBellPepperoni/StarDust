using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver_Parent : MonoBehaviour
{
    [SerializeField] QuestParent Questref;





    public QuestParent GetQuestInfo() 
    {
        return Questref;
    }

    public void GiveQuest() 
    {
        if (Questref.currentState == QuestParent.QuestProgress.Disabled) {

            Questref.StartQuest ();

        } else if (Questref.currentState == QuestParent.QuestProgress.Started) {

            //Repeatsome lineshere

        } else if (Questref.currentState == QuestParent.QuestProgress.Finished && Questref.ReturntoQuestGiver) {

            //GiveReward
            Debug.LogError ("Q U E S T Rewarded");

            Questref.currentState = QuestParent.QuestProgress.Rewarded;

        }
    }
}
