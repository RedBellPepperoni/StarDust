using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDamagable : Damagable
{
    

    [SerializeField] DoorJamQuest Quest;
   

    public override void Die () {



        if(Quest.currentState == QuestParent.QuestProgress.Disabled) 
        {
            Quest.StartQuestDoor ();
        }

        Quest.ProgressQuest ();

        Quest.doorOpened = true;

       


       
        base.Die ();
    }

}
