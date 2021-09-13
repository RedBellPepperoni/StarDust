using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo_Pickable : Interactable
{

    [SerializeField]int ammoRefillCount = 20;
    [SerializeField] TextMeshPro dispText;
    public Color baseColor;
    public TextDataPop ammoText;
    public GameObject objRoot;

    protected override void Start () {
        base.Start ();
        ammoText.UpdateText (string.Concat ("+", ammoRefillCount));
    }
    public override void ObjPicked () {
        base.ObjPicked ();


        if (Gamemanager.instance.GetCurrentAmmo () <= Gamemanager.instance.GetmaxAmmo ()) { Gamemanager.instance.AddAmmo (ammoRefillCount); } 
        else 
        { dispText.text = "Ammo Full";
            dispText.color = Color.red;
        }

        ammoText.PopText ();
        objRoot.SetActive (false);
        Invoke ("Delete", 2);

        
    }

    protected override void OnTriggerExit2D (Collider2D collision) 
    {
        base.OnTriggerExit2D (collision);

        dispText.color = baseColor;
    }


}

