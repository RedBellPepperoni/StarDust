using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentInteratable : Interactable
{
    public GameObject vent_Closed;
    public GameObject vent_Opened;

    [SerializeField] Transform TeleportPosi;
    bool isOpen = false ;

    public override void ObjPicked () {
        base.ObjPicked ();

        if(!isOpen) 
        {
            OpenVent ();
            isOpen = true;


        }


        Invoke ("CloseVent", 0.1f);


    }

    void OpenVent() 
    {
        vent_Closed.SetActive (false);
        vent_Opened.SetActive (true);

    }void CloseVent() 
    {
        PlayerController.instance.gameObject.transform.position = TeleportPosi.position;


        vent_Closed.SetActive (true);
        isOpen = false;
        vent_Opened.SetActive (false);

    }



}
