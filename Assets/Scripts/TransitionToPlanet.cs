using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionToPlanet : MonoBehaviour
{
    [SerializeField] QuestParent finalQuest;

    public UnityEvent ObjPickedCall;

    [SerializeField] Animator PodAnimator;
    public Transform seatloc;

    private void Start () {
        
    }
    private void OnTriggerEnter2D (Collider2D collision) {
        

        if(collision.CompareTag("Player")) 
        {
            PlayerController.instance.transform.position = seatloc.position;

            closePodDoor();
        
        }
    }


    public void closePodDoor() 
    {

        PodAnimator.SetBool ("Escape", true);

        ObjPickedCall.Invoke ();


    
    }
}
