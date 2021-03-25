using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPhysics : MonoBehaviour
{
    private void Start () {
        transform.parent = null;

        Invoke ("StopPhysicsSim", 2);
    }


    void StopPhysicsSim() 
    {
        Rigidbody2D body = GetComponent<Rigidbody2D> ();
        body.simulated = false;
    }
}
