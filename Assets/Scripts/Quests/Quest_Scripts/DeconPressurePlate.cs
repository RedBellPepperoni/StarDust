using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeconPressurePlate : PressurePlate
{

    [SerializeField] Animator anim;

    [SerializeField] Gate_Controller gateTermRef;
    [SerializeField] Gate_Controller FlipGateRef;

    private void Start () {
        anim = GetComponent<Animator> ();
    }




    protected override void OpenDoor () {
        base.OpenDoor ();


        anim.Play ("PPlatePush");

       gateTermRef.OpenSesame();
        FlipGateRef.CloseSesame ();

    }

    protected override void CloseDoor () {
        base.CloseDoor ();
        anim.Play ("PPlateIdle");
        gateTermRef.CloseSesame();
        FlipGateRef.OpenSesame ();
    }
}
