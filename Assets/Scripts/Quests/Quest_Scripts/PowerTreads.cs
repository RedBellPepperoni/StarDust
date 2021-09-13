using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTreads : QuestParent
{

    [SerializeField] Dialogue_manager DiaMan;
    [SerializeField] GameObject NPCRef;
    [SerializeField] Transform Cameratarget;
    [SerializeField] Terminal_GateUnlocker PowerTerm;
    
    public NPCMovement mover;

    int currentMarkerProgress = 0;

    public bool switchPowerOn = false ;
    bool doOnce;

    AudioSource source;

    Animator powerAnimator;
    bool enablelights;

    [SerializeField] AudioClip[] SwitchSound;



    private void Awake () {
        source = GetComponent<AudioSource> ();
        removeallmarkers ();
    }

    private void Start () {


        powerAnimator = GetComponent<Animator> ();

       
    }



    public override void StartQuest () {
        base.StartQuest ();

       UIManager.instance.HideCinematicUI ();

        Debug.LogError ("PowertreadStarted");
       

       


        ShowNextmarker (currentMarkerProgress);
         currentMarkerProgress++;
    }


    public void Playlightsound() 
    {

        source.PlayOneShot (SwitchSound[Random.Range (0, SwitchSound.Length - 1)]);


    }


     public void EnableLights() 
    {
        if (!enablelights) {
            UIManager.instance.ShowCinematicUI ();

            DiaMan.setCurrentDialogue (1);
            SetCameratarget (Cameratarget, 20);


            
            Invoke ("LightUp", 2);
            enablelights = true;
        }

        
    }

    void LightUp() 
    {
        powerAnimator.SetBool ("PowerUp",true);


        Invoke ("setCameraback", 4);
        PowerTerm.SetPower (true);
        PowerTerm.AutoUnlock ();
    }


   

    public override void ProgressQuest () {

        
        
        currentAmount++;
      

        Debug.LogError ("CurrentAmount" + currentAmount);

        isQuestComplete ();
        if (!ReturntoQuestGiver) { isQuestComplete (); }

    }

    public void termialProgress() 
    { 
          if(!doOnce) 
          {
            ProgressQuest ();
            doOnce = true;
          }
    }



    protected override void QuestCompleted () {
        base.QuestCompleted ();



        DiaMan.setCurrentDialogue (1);
        
    }

    

    public void QuestPrestart() 
    {
        Vector2 playerposi = PlayerController.instance.transform.position + new Vector3 (2, 2, 0);

        mover.SetWaypoint (0, playerposi);
        UIManager.instance.ShowCinematicUI ();
        mover.MoveNPC (0.1f);


    }



}
