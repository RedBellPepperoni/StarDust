using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTreads : QuestParent
{

    [SerializeField] Dialogue_manager DiaMan;
    [SerializeField] GameObject NPCRef;
    [SerializeField] Transform Cameratarget;
    [SerializeField] Terminal_GateUnlocker PowerTerm;

    public bool switchPowerOn = false ;


    AudioSource source;

    Animator powerAnimator;


    [SerializeField] AudioClip[] SwitchSound;



    private void Awake () {
        source = GetComponent<AudioSource> ();
    }

    private void Start () {


        powerAnimator = GetComponent<Animator> ();
    }



    public override void StartQuest () {
        base.StartQuest ();

        UIManager.instance.HideCinematicUI ();


    }


    public void Playlightsound() 
    {

        source.PlayOneShot (SwitchSound[Random.Range (0, SwitchSound.Length - 1)]);


    }


     void EnableLights() 
    {
        UIManager.instance.ShowCinematicUI ();

        DiaMan.setCurrentDialogue (1);
        SetCameratarget (Cameratarget, 20);


        PowerTerm.SetPower (true);
        PowerTerm.AutoUnlock();
        Invoke ("LightUp", 2);

        
    }

    void LightUp() 
    {
        powerAnimator.SetBool ("PowerUp",true);


        Invoke ("setCameraback", 4);
    }


   

    public override void ProgressQuest () {

        EnableLights ();

        currentAmount++;

        isQuestComplete ();
        if (!ReturntoQuestGiver) { isQuestComplete (); }

    }



    protected override void QuestCompleted () {
        base.QuestCompleted ();



        DiaMan.setCurrentDialogue (1);
        
    }

   

}
