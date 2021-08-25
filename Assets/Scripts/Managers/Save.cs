using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Save
{

    //List Storing Player Level completion Data
    public LevelDataBase levelData = new LevelDataBase ();
   

    ///List Storing data about current weapons held by the player
   /// public List<ItemObject> currWeaponData = new List<ItemObject> ();

    //List Storing Current Level
    public string currentLevelname;


    //Vars storing Coins and AncientCoins value
    public int coins = 0;
    public int aCoins = 0;


    //Vars Storing Player Current and Max health
    public float pCurrHealth = 0;
    public float pMaxHealth = 0;


    //Vars Storing Player Current and Max Ammo
    public int pCurrAmmo = 0;
    public int pMaxAmmo = 0;

    //Vars Storing Player WeaponData

    

    



   // public List<AncientRelic_Interactable> pRelics = new List<AncientRelic_Interactable> (); 
}