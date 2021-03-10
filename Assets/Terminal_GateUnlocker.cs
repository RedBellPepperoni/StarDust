using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal_GateUnlocker : MonoBehaviour
{
    bool canUnlock;
    bool hasKey;


    [SerializeField]bool KeyRequired = false;

    [SerializeField]bool isLocked = false;
    [SerializeField]SpriteRenderer ScreenRef;
     Animator anim;
    [SerializeField]Color LockColor;
    [SerializeField]Color UnlockColor;
    [SerializeField]Color AccessDoorColor;


    [SerializeField]Gate_Controller GateRef;

    private void Start () {
        if (isLocked) {
            Lock ();

        } 
        else Unlock ();
    }


    private void OnTriggerEnter2D (Collider2D collision) 
    {
        

        if(collision.gameObject.tag == "Player") 
        {
            canUnlock = true;

            ScreenRef.color = AccessDoorColor;

        }


    }


    private void OnTriggerExit2D (Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player") {
            canUnlock = false;

            if (isLocked) {
                Lock ();

            } else Unlock ();


        }
    }


    public void PlayerUnlock () 
    {
        if (KeyRequired) 
        {
            if (canUnlock && hasKey) {

                Unlock ();

            }
        }

        else 
        {
            Unlock ();
        }

    }




    private void Unlock () 
    {
            GateRef.UnlockDoor ();
            ScreenRef.color = UnlockColor;
    }


    public void Lock() 
    {
        GateRef.LockDoor ();
        ScreenRef.color = LockColor;
    }
}
