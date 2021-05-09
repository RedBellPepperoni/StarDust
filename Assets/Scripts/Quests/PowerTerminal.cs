using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTerminal : Interactable
{
    public Terminal_GateUnlocker TerminaltoPower;

    bool haskey = false;




    public override void ObjPicked () {
        base.ObjPicked ();

        if (haskey) 
        { TerminaltoPower.SetPower (true); }


    }

    public void SetKey()
    {
        haskey = true;
    
    }



}
