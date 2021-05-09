using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyafterTime : MonoBehaviour
{
    public float time = 2;

    private void Start () {
        Invoke ("Delete", time);
    }

    void Delete() {
        Destroy (gameObject);
    }
}
