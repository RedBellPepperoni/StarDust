using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Bullet : MonoBehaviour
{
   protected float speed = 20f;
    [SerializeField]protected float physicalDamage = 10;
    [SerializeField]protected float plasmaDamage = 0;
    [SerializeField]protected float fireDamage = 0;
    [SerializeField]protected float iceDamage = 0;
    [SerializeField]protected float electricDamage = 0;

    public Rigidbody2D rb;

    [SerializeField] protected bool isEnemybullet;
   

    [SerializeField] protected GameObject classicImpactprefab;
    [SerializeField] protected GameObject BloodEffect;
    [SerializeField] protected GameObject energyImpactprefab;
    [SerializeField] protected GameObject explosiveImpactprefab;
    






    public virtual void setDamage(float inphyDmg, float inPlasmaDmg, float infireDmg,float iniceDmg, float inelecDmg ) 
    {

        physicalDamage = inphyDmg;
        plasmaDamage = inPlasmaDmg;
        fireDamage = infireDmg;
        iceDamage = iniceDmg;
        electricDamage = inelecDmg;
    
    
    
    }
    public virtual void setSpeed(float Speed) { speed = Speed; }

    public virtual float getSpeed () { return speed; }

    protected virtual void Start()
    {
        transform.parent = null;

        Invoke ("Delete", 5);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
       
      


       if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagable") && !isEnemybullet && collision.gameObject.CompareTag("Enemy")) //&& collision.gameObject.tag == "Enemy"
        {
            collision.GetComponent<Damagable> ().TakeDamage (physicalDamage, plasmaDamage, fireDamage, iceDamage, electricDamage);

            ShowImpact (2);
            Delete ();
        } 
        
        
        else if (collision.gameObject.CompareTag("Player") && isEnemybullet && collision.gameObject.layer == 11) {

            collision.GetComponent<Damagable> ().TakeDamage (physicalDamage, plasmaDamage, fireDamage, iceDamage, electricDamage);
            ShowImpact (2);
            Delete ();
        }

       else if(collision.gameObject.tag == "Enemy" && isEnemybullet) 
       {

           

            return;
       }

       else if(collision.gameObject.CompareTag("Shield") && isEnemybullet)
            { return; } 
       
       
       
       
       else if(!isEnemybullet && collision.gameObject.CompareTag("IgnorePlayerBullets") && collision.gameObject.layer == LayerMask.NameToLayer ("Damagable")) 
      {

           
            ShowImpact (1);
            Delete ();
       }

       else if ( collision.gameObject.CompareTag("IgnoreBullets") && collision.gameObject.layer == LayerMask.NameToLayer ("Damagable")) 
       {
            ShowImpact (1);
            Delete ();
        }


       
       else if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagable") && collision.gameObject.CompareTag("IgnorePlayerBullets") && isEnemybullet) //&& collision.gameObject.tag == "Enemy"
       {
            collision.GetComponent<Damagable> ().TakeDamage (physicalDamage, plasmaDamage, fireDamage, iceDamage, electricDamage);

            

            ShowImpact (1);
            Delete ();
       } 
       
       else if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagable") && !collision.gameObject.CompareTag("IgnorePlayerBullets")) //&& collision.gameObject.tag == "Enemy"
          {

           

            collision.GetComponent<Damagable> ().TakeDamage (physicalDamage, plasmaDamage, fireDamage, iceDamage, electricDamage);



            ShowImpact (1);
            Delete ();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer ("IgnoreCollision")) {
            return;
        }

       if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) 
       {
            ShowImpact (1);
            Delete ();
        }

       
    }


    private void ShowImpact(int incase) 
    {
        GameObject effect;

        Quaternion Rot = Quaternion.identity;
        Rot.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360f));

        switch(incase) 
        {
            case 1: effect = Instantiate(classicImpactprefab, transform.position, Rot);
                break;
            case 2: effect = Instantiate (BloodEffect, transform.position, Rot);

                break;

            default: effect = Instantiate (classicImpactprefab, transform.position, Rot);
                break;
        }
        


        Destroy(effect, 3f);




    }

    public virtual void Move () {

        rb.velocity = transform.right * speed;
        
    }

    protected virtual void Delete () 
    { Destroy (gameObject); }

}
