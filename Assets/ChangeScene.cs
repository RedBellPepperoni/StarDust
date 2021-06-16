using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string LevelID = "Ship";
    public Transform playerloc;

    private void Awake () {
        PlayerController.instance.gameObject.transform.position = playerloc.position;
    }


    public void changeLevel() 
    {
        SceneManager.LoadScene (LevelID);    }


    private void OnTriggerEnter2D (Collider2D collision) {
        if(collision.CompareTag("Player")) 
        {

            changeLevel ();
        }
    }



}
