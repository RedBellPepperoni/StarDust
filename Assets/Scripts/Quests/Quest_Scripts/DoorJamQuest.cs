using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorJamQuest : QuestParent
{

   
    [SerializeField] Terminal_GateUnlocker TerminalOpenRef;


    [SerializeField] Transform[] CameraLook;

    

    int currentMarkerProgess = 0;
    public bool doorOpened;

    private void Start () {
        removeallmarkers ();
    }

    public void StartQuestDoor() 
    {
        currentState = QuestProgress.Started;


        if (isMainQuest) {
            QuestManager.instance.AddActiveMainQuest (this);

            QuestManager.instance.SetCurrentMainQuest (this);

        } else {
            QuestManager.instance.AddActivesideQuest (this);

            QuestManager.instance.SetCurrentSideQuest (this);
        }


    }

    public override void PreStartQuest () {
        
        ShowNextmarker (currentMarkerProgess);


        SetCameratarget (CameraLook[0], 13);
        Invoke ("SetCameraToDoor", 2);

        base.PreStartQuest ();
    }


    public override void StartQuest () {

       



        base.StartQuest ();

    }

    void baseStart() 
        { }

    protected override void giveReward () 
    {
        base.giveReward ();
        removeallmarkers ();
        TerminalOpenRef.SetKey ();

    }

    public override void ProgressQuest () {
        base.ProgressQuest ();

        currentMarkerProgess ++;
        CheckProgressCount ();
        ShowNextmarker (currentMarkerProgess);


    }

    void KillallinRoom() 
    {

        if (!doorOpened) 
        { Debug.LogError ("All Dead"); } 
       

    
    }


    

   

    void CheckProgressCount() 
    { 
       switch(currentAmount) 
       {
            

                
              

            case 2: SetCameratarget (CameraLook[2], 20);
                Invoke ("setCameraback", 3);
                break;
       }
    }




    void SetCameraToDoor() 
    {
        SetCameratarget (CameraLook[1], 13);
        Invoke ("setCameraback",1);
    }


    protected override void setCameraback () {
        base.setCameraback ();

        UIManager.instance.HideCinematicUI ();
    }

}
