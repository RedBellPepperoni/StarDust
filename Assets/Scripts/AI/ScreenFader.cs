using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{

    public static ScreenFader instance;

   

    public Image blackImage;
    [SerializeField] float alpha = 0f;



    private void Awake () {
       
        
        if (instance == null) {
            
            instance = this;
        }
    }

    public void SceneFadeIN() {
        StopAllCoroutines ();
        StartCoroutine (FadeIn ());
    }
    
    public void SceneFadeOut() {
        StopAllCoroutines ();
        StartCoroutine (FadeOut ());
    }



    IEnumerator FadeIn() 
    {
        alpha = 1;
        while(alpha>0) 
        {
            alpha -= Time.deltaTime;
            blackImage.color = new Color (0, 0, 0, alpha);
            yield return new WaitForSeconds (0);
        }

       
    }

    IEnumerator FadeOut() 
    {
        alpha = 0;
        
        while(alpha<1) 
        {
            alpha += Time.deltaTime;
            blackImage.color = new Color (0, 0, 0, alpha);
            yield return new WaitForSeconds (0);
        
        }
       
    }
}
