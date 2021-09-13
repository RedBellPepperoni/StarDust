using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoTerminal : MonoBehaviour
{

    [SerializeField] string displayText = "";
    [SerializeField] TextMeshPro dialogue;
    [SerializeField] Animator textanim;
    [SerializeField] float typingSpeed = 2;
    [SerializeField] GameObject MarkerRef;

    [SerializeField] AudioClip PopupSound;
    [SerializeField] AudioClip PopDownSound;
    [SerializeField] AudioClip TextPop;

    public AudioSource audSource;

    bool hasPopup;

    // Start is called before the first frame update

    private void Start () {
        MarkerRef.SetActive (true);
    }

    private void OnTriggerEnter2D (Collider2D collision) 
    {
        if(collision.CompareTag("Player") && !hasPopup) 
        {
            MarkerRef.SetActive (false);
            PopUpInfo ();
        }
       
    }

    private void OnTriggerExit2D (Collider2D collision) 
    {
        if (collision.CompareTag ("Player") && hasPopup) {
            StartCoroutine (PopDownType ());
        }



    }

    void PopUpInfo() 
    {
        
        Invoke ("Typetext", 1);
         hasPopup = true;
    }

    void PopDownInfo() 
    {
        
        dialogue.text = "";
        hasPopup = false;
        MarkerRef.SetActive (true);
    }

    void Typetext() 
    {
        StartCoroutine (Type ());
    }


    IEnumerator Type () {
        

        foreach (char letter in displayText) {
            dialogue.text += letter;
            audSource.PlayOneShot (TextPop);
            yield return new WaitForSeconds (typingSpeed);

        }
    }


    IEnumerator PopDownType () 
    {
        int i = 0;
       while (i<1) 
       {
            yield return new WaitForSeconds (3);
            i = 2;

       }
        PopDownInfo ();
        StopAllCoroutines ();

    
    }
}
