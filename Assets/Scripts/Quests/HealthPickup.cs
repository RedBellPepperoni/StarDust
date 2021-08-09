using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthPickup : Interactable
{
    public UnityEvent ObjPickedCall;


    // Start is called before the first frame update
    [SerializeField] float HealthValue = 20f;

    

    protected override void Start () {
        base.Start ();
    }


    public override void ObjPicked () {
        base.ObjPicked ();


        if ((Player_Damagable.instance.Getcurrhealth () < Player_Damagable.instance.Getmaxhealth ()) && canBePicked) {


            Gamemanager.instance.AddHealth (HealthValue);

            ObjPickedCall.Invoke ();

            Delete ();

        }

        if( HealthValue<0 ) 
        {
            Gamemanager.instance.AddHealth (HealthValue);
            ObjPickedCall.Invoke ();
            Delete ();
        }




    }
}


    
