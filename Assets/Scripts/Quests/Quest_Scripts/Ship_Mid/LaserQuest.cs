using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserQuest : QuestParent
{
    [SerializeField] Transform CameraLookAt;
    [SerializeField] NPC_StaticBEhav NPCRef;

    [SerializeField] Transform LeverCameraloc;

    bool isPassed = false;


    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag ("Player") && !isPassed) 
        {
            ShowCinematic ();
            isPassed = true;
        }
    }






    void ShowCinematic() 
    {
       

        Gamemanager.instance.cameraLookAt (CameraLookAt, 13);
        Invoke ("StartNPCDialogue", 1);
    }

    void StartNPCDialogue() 
    {
        

        Invoke ("NextDialogue",1);
        Invoke ("NextDialogue", 6);
        Invoke ("NextDialogue", 11);

        Invoke (nameof (setcameratoLever), 12);
        Invoke ("EndCinematic", 14);
    }

    void NextDialogue () 
    {
        NPCRef.ObjPicked ();
    }


    
    void setcameratoLever() 
    {
        SetCameratarget (LeverCameraloc, 10);
    }

    protected override void EndCinematic () 
    {
        base.EndCinematic ();
        
    }
}
