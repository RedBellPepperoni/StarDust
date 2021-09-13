using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Damagable
{
    [SerializeField] CircleCollider2D colliderRef;

    
    [SerializeField]GameObject ShieldObj;

    public override void Die () {
        
        if (deathEffect != null) {
            GameObject g = Instantiate (deathEffect, DeathEffectTransform);
            g.transform.parent = null;
        }


        Delete (); 

    }


 

    public void SetStartValues() 
    {
        currentHealth = maxHealth;
    }

    
    
}
