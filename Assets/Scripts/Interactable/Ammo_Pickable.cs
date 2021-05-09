using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Pickable : Interactable
{

    [SerializeField]int ammoRefillCount = 20;

    private void Start () {
        DisplayAnim = DisplayUIRef.GetComponent<Animator> ();
    }



    public override void ObjPicked () {
        base.ObjPicked ();

        Gamemanager.instance.AddAmmo (ammoRefillCount);

        Destroy (gameObject);
    }


    

}

