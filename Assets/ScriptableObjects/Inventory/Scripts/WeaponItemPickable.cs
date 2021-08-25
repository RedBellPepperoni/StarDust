using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemPickable : GroundItem
{
    bool doOnce = false;


    protected override void customFunc () {
        base.customFunc ();

        if (!doOnce) {

            Gamemanager.instance.AddWeapontoInventory (this);
           
            Gamemanager.instance.PickWeapon (this);
            
            doOnce = true;
        }

        Destroy (gameObject);

            
    }
}
