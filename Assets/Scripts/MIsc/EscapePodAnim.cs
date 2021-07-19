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


        PodAnim.SetBool("Escape",true);
        
        Invoke ("Escape", 4);

    }
    

    void Escape () 
    {
        PodAnim.SetBool ("Escape", false);

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
            yield return new WaitForSeconds (0);

           Pod.transform.position = new Vector3 (Pod.transform.position.x, Pod.transform.position.y - offset, Pod.transform.position.z);

        }

        Destroy (Pod);
        Destroy (gameObject);
    }
}
