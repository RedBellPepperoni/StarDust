using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCall : MonoBehaviour
{
    public UnityEvent Triggercall;

        private void OnTriggerEnter (Collider other) 
        {

        Triggercall.Invoke ();

        }
}
