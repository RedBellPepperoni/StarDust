using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Controller : MonoBehaviour
{
    Animator Gateanim;
    bool islocked = false;



    [SerializeField] AudioClip audio_open;
    [SerializeField] AudioClip audio_close;

    [SerializeField]bool isSingle = false;

    bool Open;
    [SerializeField]bool PlayerDetection = true; 
    
    [SerializeField] Color LockedColor;
    [SerializeField]Color UnlockedColor;

    [SerializeField]SpriteRenderer LeftIndicator;
    [SerializeField]SpriteRenderer RightIndicator;


    AudioSource source;
    private void Awake () {
        Gateanim = GetComponent<Animator> ();

        source = GetComponent<AudioSource> ();


    }


    public void PlayOpenSound() 
    {
        source.PlayOneShot (audio_open);
        
    }

    public void PlayCloseSound () {
        source.PlayOneShot (audio_close);

    }




    public void UnlockDoor() 
    {
        islocked = false;
        RightIndicator.color = UnlockedColor;

        if (!isSingle) { LeftIndicator.color = UnlockedColor; }

    }

    public void LockDoor () 
    {
        islocked = true;
        RightIndicator.color = LockedColor;
        if (!isSingle) { LeftIndicator.color = LockedColor; }

        CloseSesame ();
    }


    public void OpenSesame () 
    {

        if(!islocked) 
        {

             Gateanim.SetBool ("Open",true); 
              Open = true;

        }
       


    
    }

    public void CloseSesame() 
    {
         Gateanim.SetBool ("Open",false);
        Open = false;

    }

    private void OnTriggerEnter2D (Collider2D collision) {

       

        if ((collision.CompareTag("Player")|| collision.CompareTag("Enemy") || collision.CompareTag ("NPC")) && !Open && PlayerDetection) 
        {

            
           
                OpenSesame ();
          
           
        }
    }

    

    private void OnTriggerExit2D (Collider2D collision) {



        if ((collision.CompareTag ("Player") || collision.CompareTag ("Enemy") || collision.CompareTag("NPC"))&& Open && PlayerDetection) 
        {
            
                CloseSesame ();
           
               
            
        }
    }


}
