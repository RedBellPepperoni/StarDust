using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{

    public UnityEvent onDeath;

    [SerializeField]protected float maxHealth = 100;
    
    protected float currentHealth;
    public Transform DeathEffectTransform;
    protected bool isDead;

   
    public GameObject deathEffect;
    [SerializeField] protected GameObject Parent;
    [SerializeField]protected float HealModifier;




    [SerializeField] protected float physicalDamReduction = 0;
    [SerializeField] protected float plasmaDamReduction = 0 ;
    [SerializeField] protected float fireDamReduction = 0;
    [SerializeField] protected float iceDamReduction = 0;
    [SerializeField] protected float electricDamReduction = 0;

    



    public float Getcurrhealth() { return currentHealth; }
    public float Getmaxhealth() { return maxHealth; }

    public virtual void SetMaxHealth (float health) { maxHealth = health; }
    public virtual void SetCurrenthealth(float health) { currentHealth = health; }

     void Awake()
    {
        if (Parent == null)
        { Parent = this.gameObject; }

        currentHealth = maxHealth;

        isDead = false;

       
    }
    public virtual void TakeDamage(float inphyDmg, float inPlasmaDmg, float infireDmg, float iniceDmg, float inelecDmg)
    {
        //Note: Add a elemeental Damage Proc Logic and DOT effects

       currentHealth = currentHealth -  CalcDamageReduction (inphyDmg,inPlasmaDmg,infireDmg,iniceDmg,inelecDmg);





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
        onDeath.Invoke ();


        if (deathEffect != null) 
        {
            GameObject g = Instantiate (deathEffect, DeathEffectTransform);
            g.transform.parent = null;
        }




        Delete();

       
    }

    public virtual void Delete()
    {
        
        Destroy(Parent);
    }


    public virtual void heal(float health)
    {
      //  float healHealth =  maxHealth * (HealModifier /100);

        currentHealth += health;

        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;



    }

    protected virtual float CalcDamageReduction (float inphyDmg, float inPlasmaDmg, float infireDmg, float iniceDmg, float inelecDmg) 
    {
        float finalDamage = 0;





        finalDamage = finalDamage + CalcPhyDmg(inphyDmg);
        finalDamage = finalDamage + CalcPlasmaDmg (inPlasmaDmg);
        finalDamage = finalDamage + CalcFireDmg (infireDmg);
        finalDamage = finalDamage + CalcIceDmg (iniceDmg);
        finalDamage = finalDamage + CalcElecDmg (inelecDmg);

        return finalDamage;


    }

    private float CalcPhyDmg(float inDmg) 
    {
        float Dmg = 0;

        if (physicalDamReduction > 0 && inDmg > 0) 
        { 
        
            if(physicalDamReduction < 100)
            {
                Dmg =  (inDmg / 100) * (100 - physicalDamReduction );
            }
            
        
        } 
        else 
        {
            Dmg = inDmg;
         
        }

        return Dmg;

    }


    private float CalcPlasmaDmg (float inDmg) {
        float Dmg = 0;

        if (plasmaDamReduction > 0 && inDmg > 0) {

            if (plasmaDamReduction < 100) {
                Dmg =  (inDmg /100)* (100 - plasmaDamReduction);
            }


        } else {
            Dmg = inDmg;
        }

        return Dmg;

    }


    private float CalcFireDmg (float inDmg) {
        float Dmg = 0;

        if (fireDamReduction > 0 && inDmg >0) {

            if (fireDamReduction < 100) {
                Dmg =  (inDmg/100) * (100 - fireDamReduction );
            }


        } else {
            Dmg = inDmg;
        }

        return Dmg;

    }

    private float CalcIceDmg (float inDmg) {
        float Dmg = 0;

        if (iceDamReduction > 0 && inDmg > 0) {

            if (iceDamReduction < 100) {
                Dmg =  (inDmg /100)* (100 - iceDamReduction );
            }


        } else {
            Dmg = inDmg;
        }

        return Dmg;

    }

    private float CalcElecDmg (float inDmg) {
        float Dmg = 0;

        if (electricDamReduction > 0) {

            if (electricDamReduction < 100) {
                Dmg = inDmg * (electricDamReduction / 100);
            }


        } else {
            Dmg = inDmg;
        }

        return Dmg;

    }


    protected virtual void CanProc ()
    { 
    
    
    }


    protected virtual void ProcElement () 
    { 
        
    
    }
}



