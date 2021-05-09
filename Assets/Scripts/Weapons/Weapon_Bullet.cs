using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Bullet : MonoBehaviour
{
    private float speed = 20f;
    private float physicalDamage = 10;
    private float plasmaDamage = 0;
    private float fireDamage = 0;
    private float iceDamage = 0;
    private float electricDamage = 0;

    public Rigidbody2D rb;

    [SerializeField] bool isEnemybullet;
   

    [SerializeField] private GameObject classicImpactprefab;
    [SerializeField] private GameObject BloodEffect;
    [SerializeField] private GameObject energyImpactprefab;
    [SerializeField] private GameObject explosiveImpactprefab;
    






    public void setDamage(float inphyDmg, float inPlasmaDmg, float infireDmg,float iniceDmg, float inelecDmg ) 
    {

        physicalDamage = inphyDmg;
        plasmaDamage = inPlasmaDmg;
        fireDamage = infireDmg;
        iceDamage = iniceDmg;
        electricDamage = inelecDmg;
    
    
    
    }
    public void setSpeed(float Speed) { speed = Speed; }

    public float getSpeed () { return speed; }

    private void Start()
    {
        transform.parent = null;

        Invoke ("Delete", 5);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
       
      


       if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagable") && !isEnemybullet && collision.gameObject.tag == "Enemy") //&& collision.gameObject.tag == "Enemy"
        {
            collision.GetComponent<Damagable> ().TakeDamage (physicalDamage, plasmaDamage, fireDamage, iceDamage, electricDamage);

            ShowImpact (2);
            Delete ();
        } 
        
        
        else if (collision.gameObject.tag == "Player" && isEnemybullet && collision.gameObject.layer == 11) {

            collision.GetComponent<Damagable> ().TakeDamage (physicalDamage, plasmaDamage, fireDamage, iceDamage, electricDamage);
            ShowImpact (2);
            Delete ();
        }

       else if(collision.gameObject.tag == "Enemy" && isEnemybullet) 
       {

           

            return;
       }

       else if(collision.gameObject.tag == "Shield" && isEnemybullet)
            { return; } 
       
       
       
       
       else if(!isEnemybullet && collision.gameObject.tag =="IgnorePlayerBullets") 
      {
            ShowImpact (1);
            Delete ();
       }
       
       else if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagable")  ) //&& collision.gameObject.tag == "Enemy"
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

    public void Move () {

        rb.velocity = transform.right * speed;
        
    }

    private void Delete () 
    { Destroy (gameObject); }

}
