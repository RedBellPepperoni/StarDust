using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassFenceInteraction : MonoBehaviour
{
    
    [SerializeField] Animator GlassAnimator;

   

    public void Open() 
    {
        GlassAnimator.SetBool ("Start", true);
    }
}
