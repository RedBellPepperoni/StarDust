using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HealthPickup : Interactable
{
    public UnityEvent ObjPickedCall;


    // Start is called before the first frame update
    [SerializeField] float HealthValue = 20f;
    public TextDataPop healText;
    public GameObject objRoot;
    public TextMeshPro dispText;

    protected override void Start () {
        base.Start ();

        healText.UpdateText (string.Concat("+",HealthValue));
    }


    public override void ObjPicked () {
        base.ObjPicked ();


        if ((Player_Damagable.instance.Getcurrhealth () < Player_Damagable.instance.Getmaxhealth ()) && canBePicked) {


            Gamemanager.instance.AddHealth (HealthValue);

            ObjPickedCall.Invoke ();

            healText.PopText ();
            objRoot.SetActive (false);
            Invoke("Delete",2);

        }

        if( HealthValue<0 ) 
        {
            Gamemanager.instance.AddHealth (HealthValue);

            healText.PopText ();

            ObjPickedCall.Invoke ();
            objRoot.SetActive (false);
            Invoke ("Delete", 2);
        } 
        
        else {
            dispText.text = "Health Full";
            dispText.color = Color.red;
        }




    }
}


    
