using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Collectable
{
    // Start is called before the first frame update
    [SerializeField] float HealthValue = 20f;

    

    protected override void ObjPicked () {
        base.ObjPicked ();

        Gamemanager.instance.AddHealthObjtoBag (this.gameObject);

       
        Delete (); 
    }

}
