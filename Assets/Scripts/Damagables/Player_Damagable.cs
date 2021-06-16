using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damagable : Damagable
{
    public static Player_Damagable instance;
   
    
    private void Awake () {

       

        if (instance == null) {
            // DontDestroyOnLoad (gameObject);
            instance = this;


        }

        currentHealth = maxHealth;

        isDead = false;
    }

        private void Start () {

        Gamemanager.instance.setUIPlayervalues (currentHealth, maxHealth);


    }
    public override void TakeDamage (float inphyDmg, float inPlasmaDmg, float infireDmg, float iniceDmg, float inelecDmg) {
        base.TakeDamage (inphyDmg, inPlasmaDmg, infireDmg, iniceDmg, inelecDmg);


        Gamemanager.instance.setUIPlayervalues (currentHealth , maxHealth);
    }

    public override void Die () {
        

        PlayerController.instance.SetPlayerState (PlayerController.PlayerState.Dead);
        
        PlayerController.instance.Death ();
        UIManager.instance.ShowRespawnUI ();


    }

    public override void heal (float health) {
        //  float healHealth =  maxHealth * (HealModifier /100);

        currentHealth += health;

        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;


        Gamemanager.instance.setUIPlayervalues (currentHealth, maxHealth);
    }

    public void Respawn() 
    {
        isDead = false;
        currentHealth = maxHealth;
        Gamemanager.instance.setUIPlayervalues (currentHealth, maxHealth);


        PlayerController.instance.SetPlayerState (PlayerController.PlayerState.Idle);
        PlayerController.instance.Respawn ();

    }
}
