using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Interactable
{
    // Start is called before the first frame update
    [SerializeField] float HealthValue = 20f;

    public bool healthFull;

    protected override void Start () {
        base.Start ();
    }


    public override void ObjPicked () {
        base.ObjPicked ();


        if ((Player_Damagable.instance.Getcurrhealth () < Player_Damagable.instance.Getmaxhealth ()) && canBePicked) {


            Gamemanager.instance.AddHealth (HealthValue);
            Delete ();

        }


    }
}


    
