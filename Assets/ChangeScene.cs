using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string LevelID = "Ship";
    [SerializeField] LevelInfo levelData;
    public float ChangeTime = 1f;
    public Transform playerloc;

    private void Awake () {

        PlayerController.instance.gameObject.transform.position = playerloc.position;

        ScreenFader.instance.SceneFadeIN ();

       

        
    }

    private void Start () {
        Gamemanager.instance.currentLevelname = levelData.levelName;
        SaveManager.instance.SaveGame ();
    }
    void FadeandLoad() 
    {
        Debug.LogWarning (LevelID);
        SceneManager.LoadScene (LevelID);
    }

    public void changeLevel() 
    {
        ScreenFader.instance.SceneFadeOut();

        Gamemanager.instance.SetLevelComplete (levelData.levelName);
        //saving Data
        SaveManager.instance.SaveGame ();

       

        Invoke ("FadeandLoad", 1f);


    }


    private void OnTriggerEnter2D (Collider2D collision) {
        if(collision.CompareTag("Player")) 
        {

            Invoke ("changeLevel",ChangeTime);
        }
    }

    

}
