using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalQuest : QuestParent
{
    public bool isGeneratorOn = false ;
    public Transform[] CameraLook;


    public bool doOnce;

    public override void StartQuest () {
        base.StartQuest ();

        if(isGeneratorOn) 
        { ProgressQuest (); }
       
        EndCinematic ();
    }
    public override void ProgressQuest () {

        
        base.ProgressQuest ();
        CheckProgressCount ();

    }


    

    public void ControlPanelStart() 
    { 
           if(isGeneratorOn&& !doOnce) 
           { ProgressQuest ();
            doOnce = true;
           }
    }

    public void SetGeneratorOn() 
    {
        isGeneratorOn = true;
    }

    void CheckProgressCount () {
        

        switch (currentAmount) {





            case 2:
                SetCameratarget (CameraLook[0], 20);
                Invoke ("setCameraback", 3);
                break;
        }
    }

}
