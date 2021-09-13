using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePodAnim : MonoBehaviour
{

    public GameObject Pod;
     [SerializeField]Animator PodAnim;
    bool escaped = false;

    private void Start () {
       // PodAnim = Pod.GetComponent<Animator> ();
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag ("Player")&&!escaped) {
            ReleasePod ();
            escaped = true;
        }
    }


    void ReleasePod () {


        PodAnim.SetTrigger("Escape");
        
        Invoke ("Escape", 4);

    }
    

    void Escape () 
    {
        

        
    }

    
}
