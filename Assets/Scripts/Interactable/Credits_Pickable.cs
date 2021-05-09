using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits_Pickable : Interactable
{
    [SerializeField] int creditAmount = 20;

    private void Start () {
        DisplayAnim = DisplayUIRef.GetComponent<Animator> ();
    }



    public override void ObjPicked () {
        base.ObjPicked ();

        Gamemanager.instance.Addcoins (creditAmount);

        Destroy (gameObject);
    }

    public void setCreditAmount (int inCreditamt) 
    {
        creditAmount = inCreditamt;
    }
}
