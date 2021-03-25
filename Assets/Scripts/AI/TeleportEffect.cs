using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : MonoBehaviour
{
    [SerializeField] Renderer[] rend;
    [SerializeField] private Shader shader;
     private Material mat;
    
    private float dissolveAmt;
    // private bool isDisolving;
    float time = 1;




    private void Start () 
    {
        mat = new Material (shader);

        foreach (Renderer r in rend) { r.material = mat; }
        

        StartCoroutine ("Countdown");
    }


    IEnumerator Countdown () {
        float counter = time;
        while (counter > 0) {

            yield return new WaitForSeconds (0.05f);


            counter-=0.05f;
            mat.SetFloat ("_DissolveAmt",counter);
        }
        
    }

}
