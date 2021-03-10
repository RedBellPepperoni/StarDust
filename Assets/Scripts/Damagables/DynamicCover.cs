using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCover : Damagable
{

    public Animator debrieanim;
    [SerializeField]List<GameObject> debrisPrefabs;
    public GameObject[] hideRef;
    public Collider2D[] colliderref;


    int DamageState = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage (float inphyDmg, float inPlasmaDmg, float infireDmg, float iniceDmg, float inelecDmg) {
        base.TakeDamage (inphyDmg, inPlasmaDmg, infireDmg, iniceDmg, inelecDmg);


        Animate ();

    }

    public override void Die () {
        base.Die ();

        SpawnDebris (4);
        foreach (GameObject g in hideRef) 
        {
            g.SetActive (false);
        }

        foreach (Collider2D c in colliderref) 
        {
            c.enabled = false;
         }

       Invoke("Delete" ,3 );
    }

    void Animate () 
    {
        if (currentHealth <= maxHealth*0.8f && currentHealth > maxHealth*0.6f && DamageState == 0) 
        {
             debrieanim.SetTrigger ("Stage1");
            
            SpawnDebris (1);

            DamageState++;

        }

        else if(currentHealth <= maxHealth * 0.6f && currentHealth > maxHealth * 0.4f && DamageState == 1) 
        {
            debrieanim.SetTrigger ("Stage2");
            
            SpawnDebris (2);
            DamageState++;
        } 
        
        else if (currentHealth <= maxHealth * 0.4f && currentHealth > maxHealth * 0.2f && DamageState == 2) 
        {
            debrieanim.SetTrigger ("Stage3");

           
            SpawnDebris (3);
            DamageState++;
        }
    }


    void SpawnDebris (int state) 
    {

        Vector2 force = new Vector2 (Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f));

        GameObject obj;
      switch (state) 
      {
            case 1: 
                obj =  Instantiate (debrisPrefabs[0],transform);
                obj.GetComponent<Rigidbody2D> ().SetRotation (Random.Range (0, 120));
                obj.GetComponent<Rigidbody2D> ().AddForce (force);
                obj.GetComponent<Rigidbody2D> ().AddTorque(1);



                break;


            case 2:
                 obj = Instantiate (debrisPrefabs[0], transform);
                obj.GetComponent<Rigidbody2D> ().SetRotation (Random.Range (0, 120));
                obj.GetComponent<Rigidbody2D> ().AddForce (force);
                obj.GetComponent<Rigidbody2D> ().AddTorque (1);

                break;

            case 3:


                obj = Instantiate (debrisPrefabs[0], transform);
                obj.GetComponent<Rigidbody2D> ().SetRotation (Random.Range (0, 120));
                obj.GetComponent<Rigidbody2D> ().AddForce (force);
                obj.GetComponent<Rigidbody2D> ().AddTorque (1);

                force = new Vector2 (Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f));

                obj = Instantiate (debrisPrefabs[1], transform);
                obj.GetComponent<Rigidbody2D> ().SetRotation (Random.Range (0, 120));
                obj.GetComponent<Rigidbody2D> ().AddForce (force);
                break;

            case 4:

                foreach (GameObject g in debrisPrefabs) {

                    force = new Vector2 (Random.Range (-0.2f, 0.2f), Random.Range (-0.2f, 0.2f));

                    obj = Instantiate (g, transform);
                    obj.GetComponent<Rigidbody2D> ().SetRotation (Random.Range (0, 120));
                    obj.GetComponent<Rigidbody2D> ().AddForce (force);

                }

                break;
        }
    
    
    }


}
