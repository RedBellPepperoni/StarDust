using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerminalPower : Interactable
{
    public UnityEvent Unlocked;

    [SerializeField]QuestParent PowerQuest;
    bool doOnce = false;
    public override void ObjPicked () {




        if (PowerQuest.currentState == QuestParent.QuestProgress.Started)
        {
            if (doOnce == false) {
                base.ObjPicked ();

                Unlocked.Invoke ();

                PowerQuest.ProgressQuest ();
                doOnce = true;
            }
        }
    }
}
