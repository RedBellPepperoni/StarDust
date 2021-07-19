using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : Damagable
{
    [SerializeField] float timetoRespawn = 3f;
    [SerializeField] float shieldHealth;
    [SerializeField] Shield shieldRef;


    private void Start () 
    {
        SetShieldValues ();


    }



    public override void Die () {
        base.Die ();


        shieldRef.StopReshield ();

        Invoke ("Delete",2);

    }

    public void Reshield()
    {
     
    
    }


    void SetShieldValues() 
    {
        if (shieldRef) 
        {
            shieldRef.SetMaxHealth (shieldHealth);

            shieldRef.SetReShieldtime (timetoRespawn);
            shieldRef.SetStartValues ();
        }
    }
}
