using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField]protected float maxHealth = 100;
    
    protected float currentHealth;

    protected bool isDead;

   
    public GameObject deathEffect;
    [SerializeField] GameObject Parent;
    [SerializeField]protected float HealModifier;


    public float Getcurrhealth() { return currentHealth; }
    public float Getmaxhealth() { return maxHealth; }

    public void SetMaxHealth (int health) { maxHealth = health; }

     void Awake()
    {
        if (Parent == null)
        { Parent = this.gameObject; }

        currentHealth = maxHealth;

        isDead = false;
    }
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;



        if (currentHealth <= 0 && !isDead)
        {
            currentHealth = 0;
            isDead = true;

            Die();
         //   Delete();
        }
    }

    public virtual void Die()
    {
        //  Instantiate(deathEffect, transform.position, Quaternion.identity);



       
    }

    public virtual void Delete()
    {

        Destroy(Parent);
    }


    public virtual void heal()
    {
        float healHealth =  maxHealth * (HealModifier /100);

        currentHealth += healHealth;

        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;

    }
}


