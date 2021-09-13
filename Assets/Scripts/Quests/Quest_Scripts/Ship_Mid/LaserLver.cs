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

        foreach (Laser l in laserRefs) {
            l.DisableLaser ();
        }


        foreach (Laser l in laserRefs) {
            l.EnableLaser ();
        }
    }

    public override void ObjPicked () {
        if (!doOnce) 
        {
            base.ObjPicked ();
            LeverMethod ();

            doOnce = true;
        }
    }


    void LeverMethod() 
    {

        if (isOn) {

           
                foreach (Laser l in laserRefs) {
                    l.DisableLaser ();
                }

                
            
            audioRef.PlayOneShot (LeverNegative);
          
                isOn = false;
                Lever.flipY = false;
            
        } 
       
    }
}
