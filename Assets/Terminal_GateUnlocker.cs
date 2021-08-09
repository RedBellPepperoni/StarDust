using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Terminal_GateUnlocker : Interactable
{
    protected bool canUnlock;
    public UnityEvent UnlockDoor;

    [SerializeField] protected GameObject lockUIRef;
    [SerializeField] protected GameObject unlockUIRef;
    [SerializeField] protected GameObject keyUIRef;
    [SerializeField] protected GameObject powerUIRef;

    [SerializeField]protected bool hasKey;
    [SerializeField] protected bool UnlockStayOpen;

    [SerializeField] protected bool KeyRequired = false;

    [SerializeField] protected bool isLocked = false;
    [SerializeField] protected SpriteRenderer ScreenRef;
    protected Animator anim;
    [SerializeField] protected Color IdleColor;
    [SerializeField] protected Color PowerOffColor;
    [SerializeField] protected Color AccessDoorColor;

    [SerializeField] protected bool hasPower = true;


    [SerializeField] Gate_Controller[] GateRef;

    protected void Start () {
        DisplayAnim =  DisplayUIRef.GetComponent<Animator> ();

        if (isLocked) {
            Lock ();

           

        } else Unlock ();


        if(hasKey) 
        {
            SetKeyUI ();
        }


        if(!hasPower) 
        { ScreenRef.color = PowerOffColor; }

    }


    

    protected override void OnTriggerEnter2D (Collider2D collision) {
        base.OnTriggerEnter2D (collision);

        if (collision.gameObject.tag == "Player" && collision.gameObject.layer == LayerMask.NameToLayer ("Player")) {


            canUnlock = true;

            ScreenRef.color = AccessDoorColor;

            if (hasPower) {
                if (isLocked) {
                    Lock ();

                    if (hasKey) {
                        SetKeyUI ();
                    }

                } else Unlock ();


                ScreenRef.color = AccessDoorColor;

            }

            else 
            {
                SetPowerUI ();

                 ScreenRef.color = PowerOffColor;
            }

            ShowDisplayUI ();

        }

        


    }




    protected override void OnTriggerExit2D (Collider2D collision) {
        base.OnTriggerExit2D (collision);

        if (collision.gameObject.tag == "Player"&&collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            canUnlock = false;

            if (isLocked&&!UnlockStayOpen) {

                if (hasKey) {
                    SetKeyUI ();
                }

                else Lock ();

            }

            else if(!isLocked)
            
            {
                Unlock ();
            
            }

            if(!hasPower) { ScreenRef.color = PowerOffColor; }

            else { ScreenRef.color = IdleColor; }

            HideDisplayUI ();
        }
        
    }


    public void PlayerUnlock () {
        if (KeyRequired) {
            if (canUnlock && hasKey) {

                Unlock ();

                isLocked = false;
                if(UnlockStayOpen) 
                {
                    UnlockAndStayOpen ();
                }

            }
        } else {
            Unlock ();
        }

    }




    protected virtual void Unlock () {

        if (hasPower) {
            foreach (Gate_Controller g in GateRef) { g.UnlockDoor (); }
            ScreenRef.color = IdleColor;


            UnlockDoor.Invoke ();

            isLocked = false;
            SetUlockUI ();
        }
        else 
        { }
    }


    public void AutoUnlock() 
    {

        KeyRequired = false;
        Unlock ();
    }


    public virtual void Lock () {

        
            foreach (Gate_Controller g in GateRef) { g.LockDoor (); }
           

            isLocked = true;

            SetLockUI ();
        
        
    }


    public void SetKey () {
        hasKey = true;

    }

    public void UnlockAndStayOpen ()
    {
    foreach(Gate_Controller g in GateRef) 
    {

            g.OpenSesame ();
    }
    
    }

    public override void ObjPicked () {
        base.ObjPicked ();

        PlayerUnlock ();
        Debug.LogWarning ("Unlocked");
    }


    protected void SetKeyUI() 
    {

        keyUIRef.SetActive (true);
        DisplayUIRef = keyUIRef;
        DisplayAnim = DisplayUIRef.GetComponent<Animator> ();
        powerUIRef.SetActive (false);
        unlockUIRef.SetActive (false);
        lockUIRef.SetActive (false);
    }

    protected void SetUlockUI() 
    {
        unlockUIRef.SetActive (true);
        DisplayUIRef = unlockUIRef;
        DisplayAnim = DisplayUIRef.GetComponent<Animator> ();
        keyUIRef.SetActive (false);
        lockUIRef.SetActive (false);
        powerUIRef.SetActive (false);
    }

    protected void SetLockUI() 
    {
        lockUIRef.SetActive (true);
        DisplayUIRef = lockUIRef;
        DisplayAnim = DisplayUIRef.GetComponent<Animator> ();
        unlockUIRef.SetActive (false);
        powerUIRef.SetActive (false);
        keyUIRef.SetActive (false);
    }

    protected void SetPowerUI () {

        powerUIRef.SetActive (true);
        lockUIRef.SetActive (false);
        DisplayUIRef = powerUIRef;
        DisplayAnim = DisplayUIRef.GetComponent<Animator> ();
        unlockUIRef.SetActive (false);

        keyUIRef.SetActive (false);
    }


    public void SetPower(bool inState)
    {
        hasPower = inState;
    
    }


}
