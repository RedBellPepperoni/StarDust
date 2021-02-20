using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damagable : Damagable
{


    private void Start () {

        Gamemanager.instance.setUIPlayervalues (currentHealth, maxHealth);


    }
    public override void TakeDamage (int damage) {
        base.TakeDamage (damage);


        Gamemanager.instance.setUIPlayervalues (currentHealth , maxHealth);
    }

    public override void Die () {
        base.Die ();

        PlayerController.instance.SetPlayerState (PlayerController.PlayerState.Dead);

        PlayerController.instance.Death ();


    }
}
