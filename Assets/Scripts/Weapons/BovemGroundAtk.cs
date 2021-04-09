using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BovemGroundAtk : MonoBehaviour
{
    List<Damagable> damagableRef = new List<Damagable>();
    

    public void giveDamage() 
    { 
    foreach(Damagable d in damagableRef) 
    {
            d.TakeDamage (30, 0, 0, 0, 0);
        
        }
    
    }

    private void OnTriggerEnter2D (Collider2D collision) 
    {

       

        if(collision.gameObject.layer==LayerMask.NameToLayer("Damagable")) 
        {
            
            //damagableRef.Add (collision.gameObject.GetComponent<Damagable>());

        }

        if(collision.gameObject.layer == LayerMask.NameToLayer ("Player")) 
        {
            

            damagableRef.Add (collision.gameObject.GetComponent<Damagable> ());
        }
    }


    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagable")) {
            damagableRef.Remove (collision.gameObject.GetComponent<Damagable> ());

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer ("Player")) {


            damagableRef.Remove (collision.gameObject.GetComponent<Damagable> ());
        }
    }
}
