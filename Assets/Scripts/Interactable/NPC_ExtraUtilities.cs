using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ExtraUtilities : MonoBehaviour
{

    public AudioSource audiosrc;

    public AudioClip[] footstepClips;







    public void PlayFootStepSound () 
    {
        int selector = Random.Range (0, 3);

        audiosrc.PlayOneShot (footstepClips[selector]);
        
    }
}
