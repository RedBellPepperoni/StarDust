using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButtonCargo : PressurePlate
{

    [SerializeField] Animator anim;

    [SerializeField] Gate_Controller gateTermRef;

    private void Start () {
        anim = GetComponent<Animator> ();
    }
    protected override void OpenDoor () {
        base.OpenDoor ();


        anim.Play ("PPlatePush");

       gateTermRef.OpenSesame();


    }

    protected override void CloseDoor () {
        base.CloseDoor ();
        anim.Play ("PPlateIdle");
        gateTermRef.CloseSesame();
    }
}
