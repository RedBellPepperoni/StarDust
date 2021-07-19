using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damagable : Damagable
{
    public static Player_Damagable instance;

    [SerializeField] AudioClip FootstepVar1;
    [SerializeField] AudioClip FootstepVar2;
    [SerializeField] AudioClip FootstepVar3;
    AudioSource audiosrc;
    private void Awake () {

       

        if (instance == null) {
            // DontDestroyOnLoad (gameObject);
            instance = this;


        }

        currentHealth = maxHealth;

        isDead = false;
    }

        private void Start () {
        audiosrc = GetComponent<AudioSource> ();
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

    public void PlayFootStepSound() 
    {
        int selector = Random.Range (0, 3);
        switch(selector)
        {
            case 0:
               audiosrc.PlayOneShot (FootstepVar1);
                break;
            case 1:
                audiosrc.PlayOneShot (FootstepVar2);
                break;
            case 2:
                audiosrc.PlayOneShot (FootstepVar3);
                break;
        
        }
    }
}
