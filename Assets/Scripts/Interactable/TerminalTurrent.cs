using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalTurrent : Terminal_GateUnlocker
{

    public Turret_Manager turrentmanRwef;

    public override void Lock () {
        turrentmanRwef.Deactivate ();
    }

    protected override void Unlock () {
        turrentmanRwef.Activate ();
    }

    protected override void OnTriggerEnter2D (Collider2D collision) 
    {
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

            } else {
                SetPowerUI ();

                ScreenRef.color = PowerOffColor;
            }

            ShowDisplayUI ();

        }
    }

    protected override void OnTriggerExit2D (Collider2D collision) 
    {
        base.OnTriggerExit2D (collision);

        if (collision.gameObject.tag == "Player" && collision.gameObject.layer == LayerMask.NameToLayer ("Player")) {
            canUnlock = false;

            if (isLocked && !UnlockStayOpen) {

                if (hasKey) {
                    SetKeyUI ();
                } else Lock ();

            } else if (!isLocked) {
                Unlock ();

            }

            if (!hasPower) { ScreenRef.color = PowerOffColor; } else { ScreenRef.color = IdleColor; }

            HideDisplayUI ();
        }

    }

    public override void ObjPicked () {
        base.ObjPicked ();

        PlayerUnlock ();
        Debug.LogWarning ("Unlocked");
    }

   

}
