using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTreads : QuestParent
{

    [SerializeField] Dialogue_manager DiaMan;
    [SerializeField] GameObject NPCRef;
    [SerializeField] Transform Cameratarget;
    [SerializeField] Terminal_GateUnlocker PowerTerm;
    [SerializeField] GameObject[] Markers;

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
        ShowNextmarker (currentMarkerProgress);
        currentMarkerProgress++;

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

    void ShowNextmarker (int state) {

        if (state <= Markers.Length - 1) {
            removeallmarkers ();

            
            Markers[state].SetActive (true);
        }
    }

    void removeallmarkers () {
        foreach (GameObject g in Markers) {
            g.SetActive (false);
        }

    }



}
