using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Bullet : MonoBehaviour
{
    private float speed = 20f;
    private int weaponDamage = 10;
    public Rigidbody2D rb;

    [SerializeField] bool isEnemybullet;
   

    [SerializeField] private GameObject classicImpactprefab;
    [SerializeField] private GameObject energyImpactprefab;
    [SerializeField] private GameObject explosiveImpactprefab;
    [SerializeField] private GameObject laserImpactprefab;


    public void setDamage(int Damage) { weaponDamage = Damage; }
    public void setSpeed(float Speed) { speed = Speed; }

    public float getSpeed () { return speed; }

    private void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
       
       if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagable")&& !isEnemybullet && collision.gameObject.tag == "Enemy") 
       {
                collision.GetComponent<Damagable> ().TakeDamage (weaponDamage);

            ShowImpact ();
            Destroy (gameObject);
        }

          


        else if (collision.gameObject.tag == "Player" && isEnemybullet && collision.gameObject.layer == 11) {

            collision.GetComponent<Damagable> ().TakeDamage (weaponDamage);
            ShowImpact ();
            Destroy (gameObject);
        }

       else if(collision.gameObject.tag == "Enemy" && isEnemybullet) 
       {

           

            return;
       }
       
       if (collision.gameObject.layer == LayerMask.NameToLayer ("IgnoreCollision")) {
            return;
        }

       if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) 
       {
            ShowImpact ();
            Destroy (gameObject);
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
}
