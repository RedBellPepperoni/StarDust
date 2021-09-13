using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBarrel : Damagable
{
    [SerializeField] Collider2D selfCollider;
    [SerializeField] int forcepw = 30;
    
    public GameObject outline;

    private void Awake () {

       
        
    }


    public override void Die () {



        onDeath.Invoke ();

        GetComponent<Animator> ().Play ("BarrelPreBlast");
        Invoke ("blast", 1);
    }

    public void ShowOutline ()
    {
        outline.SetActive (true);
    }

    public void hideOutline() 
    {
        outline.SetActive (false);
    }

    void blast() {

        ShowOutline ();
        LayerMask mask = 1 << 11 | 1 << 6;
        selfCollider.enabled = false;
        Collider2D[] colliderarray = Physics2D.OverlapCircleAll (transform.position, 10f, mask);


        
        

        foreach (Collider2D c in colliderarray) {



            c.GetComponent<Damagable> ().TakeDamage (20, 0, 30, 0, 0);

            Vector2 force = (c.gameObject.transform.position - transform.position) * forcepw;


            if (c.gameObject.GetComponentInParent<Rigidbody2D> () != null) {


                if (c.gameObject.GetComponentInParent<Rigidbody2D> ().velocity.magnitude <= 11) { c.gameObject.GetComponentInParent<Rigidbody2D> ().AddForce (force, ForceMode2D.Impulse); }




            }


           


        }

        if (deathEffect != null) {
            GameObject g = Instantiate (deathEffect, DeathEffectTransform);
            g.transform.parent = null;
        }

        Delete ();
    }
}
