using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits_Consumable : Interactable
{
    public int coinRefillCount = 10;

   

    public override void ObjPicked () {
        base.ObjPicked ();

        Gamemanager.instance.Addcoins (coinRefillCount);

        Destroy (gameObject);
    }
}
