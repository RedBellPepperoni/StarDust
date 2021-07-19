using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string LevelID = "Ship";
    public float ChangeTime = 1f;
    public Transform playerloc;

    private void Awake () {
        PlayerController.instance.gameObject.transform.position = playerloc.position;

        ScreenFader.instance.SceneFadeIN ();
    }


    void FadeandLoad() 
    {
        SceneManager.LoadScene (LevelID);
    }

    public void changeLevel() 
    {
        ScreenFader.instance.SceneFadeOut();

        Invoke ("FadeandLoad", 0.5f);
           
    
    }


    private void OnTriggerEnter2D (Collider2D collision) {
        if(collision.CompareTag("Player")) 
        {

            Invoke ("changeLevel",ChangeTime);
        }
    }



}
