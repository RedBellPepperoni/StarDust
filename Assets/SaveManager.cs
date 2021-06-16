using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public void SaveGame() 
    {
        //saving coin and ancient coin amounts

        int coins = Gamemanager.instance.GetCoinAmount ();
        int aCoins = Gamemanager.instance.GetAncientcoins ();

        PlayerPrefs.SetInt ("PlayerCoins", coins);
        PlayerPrefs.SetInt ("AncientCoins", aCoins);

        // saving player health

        float health = Player_Damagable.instance.Getcurrhealth ();
        float maxhealth = Player_Damagable.instance.Getmaxhealth();

        PlayerPrefs.SetFloat ("PlayercurrentHealth", health);
        PlayerPrefs.SetFloat ("PlayermaxHealth", maxhealth);

        // saving ammo count

        int currAmmo = Gamemanager.instance.GetCurrentAmmo();
        int maxAmmo = Gamemanager.instance.GetmaxAmmo();

        PlayerPrefs.SetFloat ("CurrentAmmo", currAmmo);
        PlayerPrefs.SetFloat ("MaxAmmo", maxAmmo);

    
    }

    
   
}
