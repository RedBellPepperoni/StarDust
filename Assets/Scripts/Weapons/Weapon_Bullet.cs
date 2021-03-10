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
        Invoke ("Delete", 5);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
       
       if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagable")&& !isEnemybullet && collision.gameObject.tag != "Player") //&& collision.gameObject.tag == "Enemy"
       {
                collision.GetComponent<Damagable> ().TakeDamage (physicalDamage,plasmaDamage,fireDamage,iceDamage,electricDamage);

            ShowImpact ();
            Delete ();
        }

          


        else if (collision.gameObject.tag == "Player" && isEnemybullet && collision.gameObject.layer == 11) {

            collision.GetComponent<Damagable> ().TakeDamage (physicalDamage, plasmaDamage, fireDamage, iceDamage, electricDamage);
            ShowImpact ();
            Delete ();
        }

       else if(collision.gameObject.tag == "Enemy" && isEnemybullet) 
       {

           

            return;
       }



       else if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagable")  ) //&& collision.gameObject.tag == "Enemy"
       {
            collision.GetComponent<Damagable> ().TakeDamage (physicalDamage, plasmaDamage, fireDamage, iceDamage, electricDamage);

            ShowImpact ();
            Delete ();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer ("IgnoreCollision")) {
            return;
        }

       if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) 
       {
            ShowImpact ();
            Delete ();
        }

        
    }


    private void ShowImpact() 
    {
        GameObject effect;

        Quaternion Rot = Quaternion.identity;
        Rot.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360f));


        effect = Instantiate(classicImpactprefab, transform.position, Rot);


        Destroy(effect, 3f);




    }

    public void Move () {

        rb.velocity = transform.right * speed;
        
    }

    private void Delete () 
    { Destroy (gameObject); }

}
