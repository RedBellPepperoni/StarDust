using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int levelno = 1;
    [SerializeField]
    public void Startgame() 
    {
        SceneManager.LoadScene (levelno);
    }

    private void Awake () {
        
    }
}
