using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    // [SerializeField] CinemachineVirtualCamera vcam;


    public CinemachineBrain vcam;

    [SerializeField] GameObject InteractobjRef;
    [SerializeField] GameObject carryObj;
    [SerializeField] List<GameObject> HealthObjects;

    [SerializeField] private GameObject HoldInvRef;
    [SerializeField] Transform objpickTransform;

    private bool canCarryObj = true;

    [SerializeField] private GameObject UIRef;
    [SerializeField] private GameObject playerAim;
    

    [SerializeField] private GameObject lookatRef;

    public CinemachineVirtualCamera VcamRef;


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


    private int coinAmount = 0;
    private int ancientCoinamt = 0;
    private int totalAmmoCount = 100;
    private int currentAmmoCount = 100;


   

    


    private void Awake()
    {

        Application.targetFrameRate = 120;

        if (instance == null) {
           // DontDestroyOnLoad (gameObject);
            instance = this;
        }


        uimanagerRef =  UIRef.GetComponent<UIManager>();
       playerAimRef = playerAim.GetComponent <PlayerWeaponAim_mouse>();



        uimanagerRef.SetPlayerAmmoValues (currentAmmoCount, totalAmmoCount);
        uimanagerRef.SetCoinAmount (coinAmount);


    }


    public void cameraLookAt(Transform target,float size) 
    {
        vcam.ActiveVirtualCamera.LookAt = target;
        vcam.ActiveVirtualCamera.Follow = target;


        VcamRef.m_Lens.OrthographicSize = size;



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

        Action_Manager.instance.SetAimRange (weaponScriptRef.GetAimRange());

       
    }

    

    public void setUIPlayervalues(float curHealth,float maxHealth) 
    {
        uimanagerRef.setPlayerUIvalues (curHealth, maxHealth);
    
    }
        

   
    public void Onshoot()
    {
  
       
                if (currentAmmoCount > 0 && Time.time > nextFire)
                {

                       if (currentAmmoCount - weaponScriptRef.getWeaponBulletuse () >= 0) 
                       {
                            weaponScriptRef.OnShoot ();
                            nextFire = Time.time + (weaponScriptRef.GetEffectiveFireRate ());


                            LowerAmmoCount ();


                                  switch (weaponScriptRef.getWeaponClass ()) 
                                  {
                     
                                       case WeaponParent.weaponType.Pistol:
                                                                            InstantiateBullet (aimGunEndPointTrasform.position, aimGunEndPointTrasform.rotation.eulerAngles);
                                                                            break;


                                       case WeaponParent.weaponType.Shotgun:
                                                                            Transform Spreadgunpoint = aimGunEndPointTrasform;
 
                                                                            float rotOffset = weaponScriptRef.GetShotgunangle () / weaponScriptRef.GetBurstbulletCount ();

                                                                            float newRot = -weaponScriptRef.GetShotgunangle () / 2;

                                                                            for (int i = 1; i <= weaponScriptRef.GetBurstbulletCount (); i++) 
                                                                            {


                                                                              InstantiateBullet (aimGunEndPointTrasform.position, aimGunEndPointTrasform.rotation.eulerAngles + new Vector3 (0, 0, newRot));
                                                                              newRot = newRot + rotOffset;
                                                                            }

                                                                             break;

                                  }



                       }

                    
                }

                else if(currentAmmoCount <= 0) 
                {
                  currentAmmoCount = 0;
                }
                   

        
              //  else
               //     Debug.Log("Weapon Reloading");


               
        
        
    }


    public void AddAmmo(int ammo) 
    {
           currentAmmoCount = currentAmmoCount + ammo;

           if(currentAmmoCount >= totalAmmoCount) 
           {
              currentAmmoCount = totalAmmoCount;
           }

           uimanagerRef.SetPlayerAmmoValues (currentAmmoCount,totalAmmoCount);


    }


    void LowerAmmoCount() 
    {

       
            currentAmmoCount = currentAmmoCount - weaponScriptRef.getWeaponBulletuse ();


             uimanagerRef.SetPlayerAmmoValues (currentAmmoCount, totalAmmoCount);
    }




    void InstantiateBullet(Vector3 position,Vector3 rotation) 
    {
        GameObject bullet = Instantiate (weaponScriptRef.getBulletPrefab (), position, Quaternion.Euler(rotation));
        bullet.GetComponent<Weapon_Bullet> ().setDamage (weaponScriptRef.getWeaponPhyDmg (), weaponScriptRef.getWeaponPlasmaDmg (), weaponScriptRef.getWeaponFireDmg (), weaponScriptRef.getWeaponIceDmg (), weaponScriptRef.getWeaponElecDmg ());
        bullet.GetComponent<Weapon_Bullet> ().setSpeed (weaponScriptRef.GetBulletSpeed ());
        bullet.GetComponent<Weapon_Bullet> ().Move ();
    }
   

    void initializeWeapon()
    {
        
        currWeaponref = WeaponRootRef.transform.Find("WeaponRoot").GetChild(0).gameObject;
       

        Debug.Log(currWeaponref);
        

        SetWeaponReference();
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

            //  GameObject obj =Instantiate (ObjRef, objpickTransform);
            // obj.transform.localPosition = Vector3.zero;

            ObjRef.transform.parent = objpickTransform;
            ObjRef.transform.localPosition = Vector3.zero;


            //carryObj = obj;
            carryObj.GetComponent<Interactable> ().isPicked = true;
            canCarryObj = false;

            Action_Manager.instance.SetMultiButtonFunc (Action_Manager.MultibtnState.Drop);

        }

        


    }

    

    public void DropObj() 
    {
        if (carryObj != null)
 
        {



            carryObj.GetComponent<Interactable> ().Dropped ();
           
            
           
            objpickTransform.GetChild (0).transform.parent = null;
            

           // GameObject obj = Instantiate (carryObj, PlayerController.instance.transform);
           // obj.transform.parent = null;
            carryObj = null;

           // objpickTransform.GetChild (0).gameObject.GetComponent<Interactable> ().Delete ();

            canCarryObj = true;
            Action_Manager.instance.SetMultiButtonFunc (Action_Manager.MultibtnState.Shoot);

        }
    }


    public void AddHealth(float inhealthvalue) 
    {
        Player_Damagable.instance.heal (inhealthvalue);
    }

    public void SetintObjRef(GameObject inObject) 
    {

        if(InteractobjRef!=null) 
        {
            InteractobjRef.GetComponent<Interactable> ().HideObjInteraction ();
        }

        InteractobjRef = inObject;
    }

    public void UnSetintObjRef () 
    {
        InteractobjRef = null;
    }

    public GameObject getInteObj () 
    { return InteractobjRef; }


    public int GetCoinAmount() 
    {
        return coinAmount;
    }

    public void Addcoins(int coins) 
    {
        coinAmount = coinAmount + coins;
        Debug.LogError (coinAmount);
        UIManager.instance.SetCoinAmount (coinAmount);
    }
}


