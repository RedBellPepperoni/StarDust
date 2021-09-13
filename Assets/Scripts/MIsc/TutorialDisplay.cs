using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour
{
    public GameObject parent;

    public GameObject Root;
    public Animator anim;
    
    public void ShowTutorial() 
    {
        Root.SetActive (true);
    }

    public void HideTutorial () {

        anim.SetTrigger ("FadeOut");

        Destroy (gameObject, 4);
    }

    
    
}
