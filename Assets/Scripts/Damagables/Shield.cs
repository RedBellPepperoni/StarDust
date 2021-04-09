using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Damagable
{
    [SerializeField] CircleCollider2D colliderRef;

    bool canReshield = true;
    float ReshieldTime = 0f;
    [SerializeField]GameObject ShieldObj;

    public override void Die () {
        base.Die ();

        if (canReshield) {
            ShieldObj.SetActive (false);
            colliderRef.enabled = false;
            Invoke ("Reshield", ReshieldTime);
        } else { Delete (); }

    }


    public void StopReshield() 
    {
        canReshield = false;
    
    }

    void Reshield() 
    { 
       if(canReshield) {
            currentHealth = maxHealth;
            ShieldObj.SetActive (true);
            colliderRef.enabled = true;
            isDead = false;
          
       }
    
    }


    public void SetReShieldtime(float intime) 
    {
        ReshieldTime = intime;
    }

    public void SetStartValues() 
    {
        currentHealth = maxHealth;
    }

    
    
}
