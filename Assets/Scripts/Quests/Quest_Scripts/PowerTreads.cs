using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTreads : QuestParent
{

    [SerializeField] Dialogue_manager DiaMan;
    [SerializeField] GameObject NPCRef;
    [SerializeField] Transform Cameratarget;

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
        Debug.LogWarning ("BulbQuest Started");
       
       

    }


    public void Playlightsound() 
    {

        source.PlayOneShot (SwitchSound[Random.Range (0, SwitchSound.Length - 1)]);


    }


    public void EnableLights() 
    {
        

        DiaMan.setCurrentDialogue (1);
        Gamemanager.instance.cameraLookAt (Cameratarget,20);

        Invoke ("LightUp", 2);

        
    }

    void LightUp() 
    {
        powerAnimator.SetBool ("PowerUp",true);

        Invoke ("setCameraback", 4);
    }


    void setCameraback() 
    {
        Gamemanager.instance.cameraLookAt (PlayerController.instance.transform,13);
    }

    public override void ProgressQuest () {

       


        if (!ReturntoQuestGiver) { isQuestComplete (); }

    }



    protected override void QuestCompleted () {
        base.QuestCompleted ();



        DiaMan.setCurrentDialogue (1);
        
    }

    private void OnTriggerEnter2D (Collider2D collision) {

        EnableLights ();

    }

}
