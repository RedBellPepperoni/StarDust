using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteractable : Interactable
{
    public QuestParent Quest;
    public override void ObjPicked () {
        base.ObjPicked ();

        if(Quest!=null) 
        {
            Quest.ProgressQuest();
        }
    }
}
