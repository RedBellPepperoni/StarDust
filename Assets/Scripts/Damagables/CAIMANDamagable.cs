using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAIMANDamagable : Quest_Damagable
{

    public List<GameObject> Damageparts;
    public List<int> HealthDamagePercent;

    int i = 0;

    protected override void SetHealth () {
        base.SetHealth ();



        if (Damageparts.Count > 0) 
        {
            if (currentHealth <= maxHealth * HealthDamagePercent[i]) 
            {
                Damageparts[i].SetActive (true);
            }

            if (i < Damageparts.Count)
                i++;
        }
    }



}
