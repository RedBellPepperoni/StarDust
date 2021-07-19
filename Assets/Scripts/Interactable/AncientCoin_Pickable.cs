using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientCoin_Pickable : Interactable
{
    [SerializeField] int AncientCoinAmount = 20;

    private void Start () {
        DisplayAnim = DisplayUIRef.GetComponent<Animator> ();
    }



    public override void ObjPicked () {
        base.ObjPicked ();

        Gamemanager.instance.AddAncientCoins (AncientCoinAmount);

        Destroy (gameObject);
    }

    public void setCreditAmount (int inCreditamt) 
    {
        AncientCoinAmount = inCreditamt;
    }
}
