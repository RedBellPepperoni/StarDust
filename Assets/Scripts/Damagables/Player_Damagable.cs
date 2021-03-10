using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damagable : Damagable
{


    private void Start () {

        Gamemanager.instance.setUIPlayervalues (currentHealth, maxHealth);


    }
    public override void TakeDamage (float inphyDmg, float inPlasmaDmg, float infireDmg, float iniceDmg, float inelecDmg) {
        base.TakeDamage (inphyDmg, inPlasmaDmg, infireDmg, iniceDmg, inelecDmg);


        Gamemanager.instance.setUIPlayervalues (currentHealth , maxHealth);
    }

    public override void Die () {
        base.Die ();

        PlayerController.instance.SetPlayerState (PlayerController.PlayerState.Dead);

        PlayerController.instance.Death ();


    }
}
