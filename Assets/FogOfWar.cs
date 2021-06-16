using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] SpriteRenderer Fog;
    float temp;


    private void Start () {
       
    }

    private void OnTriggerEnter2D (Collider2D collision) 
    {
        
        if(collision.gameObject.tag=="Player") 
        {

            StartCoroutine ("Fadeout");
        }


    }


    IEnumerator Fadeout() 
    {
        temp = 0.05f;
        
        while (Fog.color.a > 0) 
       {
            Fog.color = new Color (Fog.color.r, Fog.color.b, Fog.color.b, Fog.color.a - temp);

            yield return new WaitForSeconds (0.1f);
        
       }


        StopAllCoroutines ();
    }

}
