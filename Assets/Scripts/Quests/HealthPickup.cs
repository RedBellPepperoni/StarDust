using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Collectable
{
    // Start is called before the first frame update
    [SerializeField] float HealthValue = 20f;

    protected override void OnTriggerEnter2D (Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.layer == 11) {
           

           if(Gamemanager.instance.IsHealthBagEmpty()) 
           {
                ObjPicked ();
           }
            
        }
    }

    protected override void ObjPicked () {
        base.ObjPicked ();

        Gamemanager.instance.AddHealthObjtoBag (this.gameObject);

        Delete (); 
    }

}
