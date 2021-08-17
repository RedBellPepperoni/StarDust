using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecCard_Pickable : Interactable
{
    [SerializeField] List<Terminal_GateUnlocker> gateUnlocker;
    [SerializeField] int securityLevel = 1;

    public override void ObjPicked () {
        base.ObjPicked ();

        UnlockGate ();

    }

    void UnlockGate () {

        foreach (Terminal_GateUnlocker g in gateUnlocker) { g.SetKey (); }


        Gamemanager.instance.SetSecurityAccessDetail (securityLevel);

        Delete ();
    }

    
}
