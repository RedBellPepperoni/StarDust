using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public string savepath;
    public InventoryObject weaponInventory;
    public InventoryObject currWeaponInventory;
    public InventoryObject relicInventory;

    [SerializeField] bool save;

    private void Awake () {
        if(instance ==null) 
        { instance = this; }
    }




    LevelDataBase LevelManager;
    

    
    private Save CreateSaveGameObject () 
    {

        Save save = new Save ();

        //saving Scene/Level Data
        LevelManager = Gamemanager.instance.GetLevelInfo ();

        save.levelData = LevelManager;
        save.currentLevelname = Gamemanager.instance.currentLevelname;


        ///saving weapon in hand Data
      ///  save.currWeaponData = Gamemanager.instance.WeaponsList;

        //saving coins information

        save.coins = Gamemanager.instance.GetCoinAmount();
        save.aCoins = Gamemanager.instance.GetAncientcoins ();


        //saving player health 

        save.pCurrHealth = Player_Damagable.instance.Getcurrhealth ();
        save.pMaxHealth = Player_Damagable.instance.Getmaxhealth();

        //saving playerAmmo

        save.pCurrAmmo = Gamemanager.instance.GetCurrentAmmo ();
        save.pMaxAmmo = Gamemanager.instance.GetmaxAmmo ();


        return save;

    }

    public void SaveGame () {


        if (save) {
            Save save = CreateSaveGameObject ();


            IFormatter formatter = new BinaryFormatter ();
            Stream stream = new FileStream (string.Concat (Application.persistentDataPath, savepath), FileMode.Create, FileAccess.Write);
            formatter.Serialize (stream, save);
            stream.Close ();





            Invoke ("InventoriesSave", 0.5f);

            Debug.Log ("Game Saved");
        }

       
    }

    public void LoadGame () {

        if (save) {
            if (File.Exists (string.Concat (Application.persistentDataPath, savepath))) {



               IFormatter formatter = new BinaryFormatter ();
              Stream stream = new FileStream (string.Concat (Application.persistentDataPath, savepath), FileMode.Open, FileAccess.Read);

               Save save = (Save)formatter.Deserialize (stream);
                stream.Close ();


                //loading Level Data
                Gamemanager.instance.currentLevelname = save.currentLevelname;

                LevelManager = save.levelData;
                Gamemanager.instance.SetLevelInfo (LevelManager);


                ///Loading Curretn weapon Data
              ///  Gamemanager.instance.WeaponsList = save.currWeaponData;
              ///  Gamemanager.instance.LoadCurrWeapons ();

                //Loading player Ammo Data

                Gamemanager.instance.SetAmmo (save.pMaxAmmo, save.pCurrAmmo);


                //loading Player Health Data
                Player_Damagable.instance.SetMaxHealth (save.pMaxHealth);
                Player_Damagable.instance.SetCurrenthealth (save.pCurrHealth);


               //Loading Player coins And Ancient coins
                Gamemanager.instance.SetCoinAmount (save.coins);
                Gamemanager.instance.SetAncientCoinAmount (save.aCoins);


                Gamemanager.instance.RetrivePlayerUIValue ();


                Invoke ("InventoriesLoad", 0.5f);



                Debug.Log ("Game Loaded");





            } else {
                Debug.Log ("No game saved!");

            }
        }

       

    }



    void InventoriesSave() 
    {
        weaponInventory.Save ();
        currWeaponInventory.Save ();

        

    }

    void InventoriesLoad () 
    {

        
        weaponInventory.Load();
        currWeaponInventory.Load ();

        Gamemanager.instance.LoadCurrWeapons ();

        Invoke ("InitWeponFix", 1);
    }
        
    public bool CheckSavegame() 
    {
        return File.Exists (string.Concat (Application.persistentDataPath, savepath));
    }
   
    void InitWeponFix() 
    { Gamemanager.instance.initializeWeapon (); }

}
