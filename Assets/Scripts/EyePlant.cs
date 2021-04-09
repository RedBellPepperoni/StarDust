using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePlant : MonoBehaviour
{
    
    [SerializeField] CircleCollider2D SenseColiider;
    [SerializeField]Animator Eyeanim;

    bool EyesOpen;
    Vector3 playerLoc;

    private void Start () {



        Eyeanim.SetBool ("OpenEyes", false);

    }


    void OpenEyes() 
    { 
      if(!EyesOpen) 
      {
            EyesOpen = true;

            Eyeanim.SetBool ("OpenEyes", true);



      }
    
    
    }

    void CloseEyes() 
    {
        if (EyesOpen) {
            EyesOpen = false;

            Eyeanim.SetBool ("OpenEyes", false);



        }


    }

    private void FixedUpdate () 
    {
        moveEyes ();

    }

    void moveEyes() 
    {

        if (EyesOpen) {


            Vector2 aimDirection = (transform.position - PlayerController.instance.transform.position);
          //  float angle = Mathf.Atan2 (aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            
            Eyeanim.SetFloat ("VectorX", aimDirection.x);
            Eyeanim.SetFloat ("VectorY", -aimDirection.y);

            //  if (angle > 90 || angle < -90) {




            // } else {

            //   }
        }
    }


    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            OpenEyes ();
           
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
           CloseEyes ();
            
        }
    }


}
