using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Weapon_Bullet
{
    protected Transform target;
    public float rotateSpeed = 200f;
    public float timetoExplode = 4; 
    bool hastarget = false;
    [SerializeField] int forcepw = 10;
    public Animator anim;


    protected override void Start () {
        transform.parent = null;


        Invoke (nameof(preBlast), timetoExplode);
        getPlayer ();
        speed = 10f;

        hastarget = true;
    }
    void getPlayer() 
    {
        target = PlayerController.instance.transform;
    
    }

    private void FixedUpdate () {
        if (hastarget) { Move (); }
    }

    public override void Move () {


        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize ();
         float rotateAmount = Vector3.Cross (direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;

    }

    void Blast() 
    {
        LayerMask mask = 1 << 11 | 1 << 6;
        
        Collider2D[] colliderarray = Physics2D.OverlapCircleAll (transform.position, 6f, mask);




        foreach (Collider2D c in colliderarray) {



            c.GetComponent<Damagable> ().TakeDamage (10, 0, 10, 0, 0);

            Vector2 force = (c.gameObject.transform.position - transform.position) * forcepw;


            if (c.gameObject.GetComponentInParent<Rigidbody2D> () != null) {


                if (c.gameObject.GetComponentInParent<Rigidbody2D> ().velocity.magnitude <= 11) { c.gameObject.GetComponentInParent<Rigidbody2D> ().AddForce (force, ForceMode2D.Impulse); }




            }


            


        }

        GameObject g = Instantiate (explosiveImpactprefab, transform);
        g.transform.parent = null;
        Delete ();


    }


    void preBlast() 
    {
        anim.SetTrigger ("Boom");
        Invoke (nameof (Blast), 1.2f);
    }


    protected override void DamageCall () {
        hastarget = false;
        preBlast ();
    }

    protected override void OnTriggerEnter2D (Collider2D collision) 
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagable") ) //&& collision.gameObject.tag == "Enemy"
       {
            if (collision.gameObject.CompareTag ("Enemy") && isEnemybullet)
                return;
             else   
                DamageCall ();
            


        } 

        

        else if (collision.gameObject.layer == LayerMask.NameToLayer ("Obstacle")) {
            
            DamageCall ();

        } 
        
        else if (collision.gameObject.layer == LayerMask.NameToLayer ("IgnoreCollision")) {
            return;
        }

    }

}
