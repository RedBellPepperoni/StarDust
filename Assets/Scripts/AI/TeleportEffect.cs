using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : MonoBehaviour
{
    
   

    public GameObject SpawnEffect;
    [SerializeField] List<GameObject> ObjectRef;
    
    private void Start () 
    {
        GameObject obj = Instantiate (SpawnEffect, transform);

        foreach (GameObject g in ObjectRef) {
            g.SetActive (false);
        }


        Invoke ("ShowObject", 2f);
    }


    private void ShowObject() 
    {
        foreach (GameObject g in ObjectRef) {
            g.SetActive (true);
        }
    }

}
