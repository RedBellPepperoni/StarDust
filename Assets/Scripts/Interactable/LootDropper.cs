using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
   


    private Transform objTransform;
    private float delay = 0;
    private float holdtime = 1f;
    
    private Vector3 offset;

    private void Awake ()
    {
        objTransform = transform;
        offset = new Vector3 (Random.Range (-3, 3), Random.Range (-3, 3), offset.z);

    }


    private void Update () 
    {
        if(holdtime>=delay) 
        {
            

            objTransform.position += offset * Time.deltaTime;
            delay += Time.deltaTime;
        }
    }

}
