using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorInteractable : QuestInteractable
{
    public Animator anim;
    
    public AudioSource audioRef;
    public SpriteRenderer Lever;
    bool doOnce;

    private void Start () {


        if (DisplayUIRef != null)
            DisplayAnim = DisplayUIRef.GetComponent<Animator> ();

        anim.SetBool ("isOn", false);
        audioRef.Pause ();

    }

    public override void ObjPicked () {

        if (!doOnce) {
            base.ObjPicked ();



            anim.SetBool ("isOn", true);
            audioRef.UnPause ();
            
            Lever.flipY = true;

            doOnce = false;

        }
       
    }
}
