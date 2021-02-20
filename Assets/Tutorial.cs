using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] GameObject Red;
    [SerializeField] GameObject Green;
    int tutorailsteps = 0;

    void Start()
    {
        
    }

    


    // Update is called once per frame
    void Update()
    {


        
        
    }

    private void OnTriggerEnter2D (Collider2D collision) 
    {
        if(collision.gameObject.tag == "Player") 
        {
            Red.SetActive (false);
          
        }

      //  Invoke ("TimedStop", 5);

    }


    void TimedStop() 
        {
        Red.SetActive (true);
        Green.SetActive (false);
    }

}
