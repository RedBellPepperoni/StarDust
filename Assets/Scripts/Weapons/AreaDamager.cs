using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamager : MonoBehaviour
{

    [SerializeField]List<Damagable> ThingstoDamage;

    [SerializeField]float phyDamage = 0;
    [SerializeField] float plasDamage = 0;
    [SerializeField] float fireDamage = 0;
    [SerializeField] float iceDamage = 0;
    [SerializeField] float elecDamage = 0;

    bool damgeStarted = false;

    private void OnTriggerEnter2D (Collider2D collision) {
        

        if(collision.gameObject.layer == LayerMask.NameToLayer("Damagble")|| collision.gameObject.layer == LayerMask.NameToLayer ("Player")) 
        {

           Damagable Item = collision.GetComponent<Damagable> ();

            ThingstoDamage.Add (Item);

            if (!damgeStarted) 
            {
                damgeStarted = true;
                StartCoroutine ("GiveDamage");
                
            }

        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer ("Damagble") || collision.gameObject.layer == LayerMask.NameToLayer ("Player")) {

            ThingstoDamage.Remove(collision.GetComponent<Damagable> ());
            if(ThingstoDamage.Count==0) 
            {
                StopAllCoroutines ();
                damgeStarted = false;
            }

        }
    }

    IEnumerator GiveDamage () 
    {
        while (damgeStarted) {
            

            yield return new WaitForSeconds (1);
            foreach (Damagable d in ThingstoDamage) {
                d.TakeDamage (phyDamage, plasDamage, fireDamage, iceDamage, elecDamage);
            }
        }
    }
}
