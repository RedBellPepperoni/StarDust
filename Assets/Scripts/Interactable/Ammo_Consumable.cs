using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Consumable : Interactable
{

    public int ammoRefillCount = 20;

    


    public override void ObjPicked () {
        base.ObjPicked ();

        Gamemanager.instance.AddAmmo (ammoRefillCount);

        Destroy (gameObject);
    }
}
