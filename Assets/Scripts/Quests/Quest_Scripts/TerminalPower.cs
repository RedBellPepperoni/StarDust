using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalPower : Interactable
{
    [SerializeField]QuestParent PowerQuest;
    bool doOnce = false;
    public override void ObjPicked () {




        if (PowerQuest.currentState == QuestParent.QuestProgress.Started)
        {
            if (doOnce == false) {
                base.ObjPicked ();

                PowerQuest.ProgressQuest ();
                doOnce = true;
            }
        }
    }
}
