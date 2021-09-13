using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNPCSounds : MonoBehaviour
{

    public AudioSource audiosrc;

    public List<AudioClip> footsteps;





    public void PlayFootStepSound () {
        int selector = Random.Range (0, footsteps.Count);
        
                audiosrc.PlayOneShot (footsteps[selector]);
 
    }
}
