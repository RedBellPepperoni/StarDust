using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem bloodParticles;

    private void Start () {
        Splatter ();
        Destroy (gameObject,1);
    }


    public void Splatter()
    {
        bloodParticles.Play ();
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
