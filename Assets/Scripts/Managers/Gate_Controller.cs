using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Controller : MonoBehaviour
{
    Animator Gateanim;
    bool islocked = true;

    bool Open;

    
    [SerializeField] Color LockedColor;
    [SerializeField]Color UnlockedColor;

    [SerializeField]SpriteRenderer LeftIndicator;
    [SerializeField]SpriteRenderer RightIndicator;

    private void Awake () {
        Gateanim = GetComponent<Animator> ();
    }


    


    public void UnlockDoor() 
    {
        islocked = false;
        RightIndicator.color = UnlockedColor;
        LeftIndicator.color = UnlockedColor;

    }

    public void LockDoor () 
    {
        islocked = true;
        RightIndicator.color = LockedColor;
        LeftIndicator.color = LockedColor;

        CloseSesame ();
    }


    public void OpenSesame () 
    {

        if(!islocked) 
        {

             Gateanim.SetBool ("Open",true); 
           
        }
       


    
    }

    public void CloseSesame() 
    {
         Gateanim.SetBool ("Open",false);
        
    }

    private void OnTriggerEnter2D (Collider2D collision) {


        if (collision.gameObject.layer == LayerMask.NameToLayer ("Player")&& !Open) 
        {
           
                OpenSesame ();
            Open = true;
           
        }
    }

    

    private void OnTriggerExit2D (Collider2D collision) {



        if (collision.gameObject.layer == LayerMask.NameToLayer ("Player")&& Open) 
        {
            
                CloseSesame ();
            Open = false;
               
            
        }
    }
}
