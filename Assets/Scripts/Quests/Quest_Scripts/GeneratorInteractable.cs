using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorInteractable : QuestInteractable
{
    public Animator anim;
    bool isOn = false;
    public AudioSource audioRef;
    public SpriteRenderer Lever;

    private void Start () {


        if (DisplayUIRef != null)
            DisplayAnim = DisplayUIRef.GetComponent<Animator> ();

        anim.SetBool ("isOn", false);
        audioRef.Pause ();

    }

    public override void ObjPicked () {
        base.ObjPicked ();

        if(!isOn) 
        {
            anim.SetBool ("isOn", true);
            audioRef.UnPause ();
            isOn = true;
            Lever.flipY = true;

        }

        else 
        {
            anim.SetBool ("isOn", false);
            audioRef.Pause ();
            isOn = false;
            Lever.flipY = false ;
        }

       
       
    }
}
