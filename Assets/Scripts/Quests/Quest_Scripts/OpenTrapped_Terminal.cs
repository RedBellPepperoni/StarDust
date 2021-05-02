using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTrapped_Terminal : Terminal_GateUnlocker
{

  [SerializeField] Gate_Controller LockGateTerminal;
    public override void ObjPicked () {
        base.ObjPicked ();

        LockGateTerminal.LockDoor ();

    }
}
