using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorJamQuest : QuestParent
{

    [SerializeField]DoorShieldTerminal TerminalRef;
    [SerializeField] Terminal_GateUnlocker TerminalOpenRef;


    [SerializeField] Transform[] CameraLook;

    [SerializeField] GameObject[] Markers;

    int currentMarkerProgess = 0;
    public bool doorOpened;

    private void Start () {
        removeallmarkers ();
    }

    public void StartQuestDoor() 
    {
        currentState = QuestProgress.Started;
        

        QuestManager.instance.AddActiveQuest (this);

        QuestManager.instance.SetCurrentQuest (this);
        QuestManager.instance.SetQuestUI ();


    }

    public override void StartQuest () {
        base.StartQuest ();

        
        TerminalRef.SetKey ();
        ShowNextmarker (currentMarkerProgess);


        SetCameratarget (CameraLook[0], 13);
        Invoke ("SetCameraToDoor", 2);

    }

    protected override void giveReward () 
    {
        Gamemanager.instance.Addcoins (coinReward);
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


    void ShowNextmarker(int state) 
    {

        if (state <= Markers.Length - 1) 
        {
            removeallmarkers ();

            Markers[state].SetActive (true);
        }
    }

   void removeallmarkers() 
   {
        foreach (GameObject g in Markers) {
            g.SetActive (false);
        }

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
