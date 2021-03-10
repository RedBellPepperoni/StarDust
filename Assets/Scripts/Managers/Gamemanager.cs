using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField] GameObject carryObj;
    [SerializeField] List<GameObject> HealthObjects;

    [SerializeField] private GameObject HoldInvRef;
    [SerializeField] private GameObject HealthBAgRef;

    private bool canCarryObj;

    [SerializeField] private GameObject UIRef;
    [SerializeField] private GameObject playerAim;
    [SerializeField] private int Healthbagspace = 3;

    [SerializeField] private GameObject lookatRef;


   // private WeaponClass curWeaponClass;
    private UIManager uimanagerRef;
    private PlayerWeaponAim_mouse playerAimRef;
    private GameObject bulletPrefab;
    Transform aimGunEndPointTrasform;

    private WeaponParent weaponScriptRef;
    public enum uiRefreshType{ Player, Weapon};
    
    float nextFire = 0;

    private GameObject currWeaponref;
    [SerializeField] private GameObject WeaponRootRef;

    public bool IsHealthBagEmpty () { return HealthObjects.Count < Healthbagspace; }

    public void AddHealthObjtoBag (GameObject inHealthObj) 
    {
       if(HealthObjects.Count <3) 
       {
           
            GameObject obj =  Instantiate (inHealthObj, HealthBAgRef.transform);
            HealthObjects.Add (obj);
        }



    }

    private void Awake()
    {

        Application.targetFrameRate = 120;

        if (instance == null) {
           // DontDestroyOnLoad (gameObject);
            instance = this;
        }


        uimanagerRef =  UIRef.GetComponent<UIManager>();
       playerAimRef = playerAim.GetComponent <PlayerWeaponAim_mouse>();


        


    }


    private void Start()
    {
        initializeWeapon();

    }
    public void SetWeaponReference()
    {

        weaponScriptRef = currWeaponref.GetComponent<WeaponParent>();
        uimanagerRef.SetcurrentWeaponref(currWeaponref);

        aimGunEndPointTrasform = weaponScriptRef.Endpoint;

        bulletPrefab = weaponScriptRef.getBulletPrefab();
      

        setUIweaponvalues();
    }

    public void setUIweaponvalues () {

        int bulletsinMag = weaponScriptRef.getCurrentBullets ();
        int magSize = weaponScriptRef.getMagazineSize ();
        int totalBullets = weaponScriptRef.getTotalBullets ();
        int reserveBullets = weaponScriptRef.getReserveBullets ();

        uimanagerRef.setWeaponUIvalues (bulletsinMag, magSize, totalBullets, reserveBullets);


    }

    public void setUIPlayervalues(float curHealth,float maxHealth) 
    {
        uimanagerRef.setPlayerUIvalues (curHealth, maxHealth);
    
    }
        

   
    public void Onshoot()
    {
  
       
                if (!weaponScriptRef.isEmpty()&& !weaponScriptRef.isReloading() && Time.time > nextFire)
                {

                     weaponScriptRef.OnShoot ();
                     nextFire = Time.time + (weaponScriptRef.GetEffectiveFireRate ());



                  switch (weaponScriptRef.getWeaponClass()) 
                  {
                       case WeaponParent.weaponType.Pistol:
                            InstantiateBullet (aimGunEndPointTrasform.position, aimGunEndPointTrasform.rotation.eulerAngles);
                       break;


                       case WeaponParent.weaponType.Shotgun:

                                Transform Spreadgunpoint = aimGunEndPointTrasform;

                                float rotOffset = weaponScriptRef.GetShotgunangle () / weaponScriptRef.GetBurstbulletCount ();

                                float newRot = -weaponScriptRef.GetShotgunangle () / 2;

                                for (int i = 1; i <= weaponScriptRef.GetBurstbulletCount(); i++) 
                                {

                       
                                  InstantiateBullet (aimGunEndPointTrasform.position,aimGunEndPointTrasform.rotation.eulerAngles + new Vector3(0,0,newRot));
                                   newRot = newRot + rotOffset;
                                }
 
                       break;

                  }



                    

                    
                }

                else if(weaponScriptRef.isEmpty()) 
                {
            StartReload ();
        }
                   

        
              //  else
               //     Debug.Log("Weapon Reloading");


               
        
        setUIweaponvalues();
    }

    void InstantiateBullet(Vector3 position,Vector3 rotation) 
    {
        GameObject bullet = Instantiate (weaponScriptRef.getBulletPrefab (), position, Quaternion.Euler(rotation));
        bullet.GetComponent<Weapon_Bullet> ().setDamage (weaponScriptRef.getWeaponPhyDmg (), weaponScriptRef.getWeaponPlasmaDmg (), weaponScriptRef.getWeaponFireDmg (), weaponScriptRef.getWeaponIceDmg (), weaponScriptRef.getWeaponElecDmg ());
        bullet.GetComponent<Weapon_Bullet> ().setSpeed (weaponScriptRef.GetBulletSpeed ());
        bullet.GetComponent<Weapon_Bullet> ().Move ();
    }
    public void StartReload()
    {


                if (weaponScriptRef.getCurrentBullets() < weaponScriptRef.getMagazineSize())
                {
                    weaponScriptRef.SetReloading(true);

                    StartCoroutine(Reload(weaponScriptRef));
           
                   Debug.Log("startedReload");
                }

                else Debug.Log("AlreadyFull");

               
    }

    void initializeWeapon()
    {
        
        currWeaponref = WeaponRootRef.transform.Find("WeaponRoot").GetChild(0).gameObject;
       

        Debug.Log(currWeaponref);
        

        SetWeaponReference();
    }


      IEnumerator Reload(WeaponParent scriptRef)
      {

        yield return new WaitForSeconds(scriptRef.GetReloadTime());

        scriptRef.Reload();

    //    Debug.Log("reloaded");
        setUIweaponvalues();
      }


    public bool CanCarryObject() 
    {
        return canCarryObj;
    }

    public void PickupObject(GameObject ObjRef) 
    {
        if (carryObj == null) 
        {
            carryObj = ObjRef;

            canCarryObj = false;
        }

       


    }

    public void DropObj() 
    {
        if (carryObj != null) 
        {
            Instantiate (carryObj, transform);
            canCarryObj = false;
        }
    }
}
