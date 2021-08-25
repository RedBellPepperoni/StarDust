using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserQuest : QuestParent
{
    [SerializeField] Transform CameraLookAt;
    [SerializeField] NPC_StaticBEhav NPCRef;

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


        Invoke ("EndCinematic", 13);
    }

    void NextDialogue () 
    {
        NPCRef.ObjPicked ();
    }

    void EndCinematic () 
    {
        setCameraback ();
        UIManager.instance.HideCinematicUI ();
    }
}
