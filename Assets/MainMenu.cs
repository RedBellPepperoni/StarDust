using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int levelno = 1;

<<<<<<< Updated upstream
=======

    private void Start () {
        if (SaveManager.instance.CheckSavegame ()) {
            SaveManager.instance.LoadGame ();
        }


    }

       
>>>>>>> Stashed changes
    public void Startgame() 
    {

        


        if (SaveManager.instance.CheckSavegame ()) 
        {
            SaveManager.instance.LoadGame ();

            

            SceneManager.LoadScene (Gamemanager.instance.currentLevelname);
        } 
        
        else { SceneManager.LoadScene (1); }
       
    }
}
