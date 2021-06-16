using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePodAnim : MonoBehaviour
{

    public GameObject Pod;
     Animator PodAnim;


    private void Start () {
        PodAnim = Pod.GetComponent<Animator> ();
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag ("Player")) {
            ReleasePod ();
        }
    }


    void ReleasePod () {
        PodAnim.Play ("Pod_Close");

        Invoke ("Escape", 1);

    }


    void Escape () 
    {
        StartCoroutine ("Move");
    }

    IEnumerator Move()
    {
        int i = 0;
        float offset = 0f;

        while (i < 50) 
        {
            i++;
            offset = offset + i/10;
            yield return new WaitForSeconds (0.05f);

            transform.position = new Vector3 (transform.position.x, transform.position.y - offset, transform.position.z);

        }

        Destroy (gameObject);
    }
}
