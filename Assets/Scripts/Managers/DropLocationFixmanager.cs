using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLocationFixmanager : MonoBehaviour
{
    GameObject DropReference;
    [SerializeField]BoxCollider2D boxColRef;

    

    private void OnTriggerEnter2D (Collider2D collision) {



        if (collision.GetComponent<Interactable> () != null) 
        {
            if (collision.GetComponent<Interactable> ().isPicked == false) {

             

                DropReference = collision.gameObject;

                DropReference.transform.position =   new Vector3 (DropReference.transform.position.x, boxColRef.transform.position.y - boxColRef.size.y / 2, 0); 
            }

        }
    }
}
