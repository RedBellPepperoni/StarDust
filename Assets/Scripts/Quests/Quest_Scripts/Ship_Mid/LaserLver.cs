using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLver : QuestInteractable
{
    public AudioSource audioRef;
    public SpriteRenderer Lever;

    public AudioClip LeverPositive;
    public AudioClip LeverNegative;

    [SerializeField]List<Laser> laserRefs;

    bool isOn = true;

    protected override void Start () {
        base.Start ();

        Lever.flipY = true;

    }

    public override void ObjPicked () {
        base.ObjPicked ();


        if(Quest!=null && Quest.currentState != QuestParent.QuestProgress.Finished) 
        {


            Quest.ProgressQuest ();

        
        }

        LeverMethod ();
    }


    void LeverMethod() 
    {

        if (!isOn) {

            foreach (Laser l in laserRefs) {
                l.EnableLaser ();
            }

            audioRef.PlayOneShot (LeverPositive);
            isOn = true;
            Lever.flipY = true;

        } else {

            audioRef.PlayOneShot (LeverNegative);
            foreach (Laser l in laserRefs) {
                l.DisableLaser ();
            }
            isOn = false;
            Lever.flipY = false;
        }
    }
}
