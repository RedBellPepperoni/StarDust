using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    [SerializeField] bool hasInfiniteAmmo = false;
    public enum fireType { Single, Auto, Burst};
    private fireType currWeaponFireMode = fireType.Single;
   public GameObject BulletPrefab;
    public enum weaponType { Pistol, Rifle, Shotgun, Projectile, Beam, Melee };
    [SerializeField] private weaponType currWeaponType = weaponType.Pistol;

    public Transform Endpoint;

  
    private bool weaponEmpty;

    [SerializeField] private float bulletSpeed = 10f;



   [SerializeField] private float physicalDamage = 10;
   [SerializeField] private float plasmaDamage = 0;
   [SerializeField] private float fireDamage = 0;
   [SerializeField] private float iceDamage = 0;
   [SerializeField] private float electricDamage = 0;




    [SerializeField] private float rateOfFire = 10f;

    [SerializeField] private int magazineSize = 10;
    [SerializeField] private int totalBullets = 300;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private int bulletUse = 1;
    [SerializeField] private float shotSpreadangle = 30;
    [SerializeField] private int burstCount = 5;

    private int reserveBullets = 300;
    private int currentBullets = 10;
    private bool isAnimated;
    private bool weaponReloading = false;



    private Animator weaponAnimator;

    [SerializeField] private bool isBurst;
    private bool charged;


    public weaponType getWeaponClass() { return currWeaponType; }
    private void setWeaponDamage(float inphyDmg, float inPlasmaDmg, float infireDmg, float iniceDmg, float inelecDmg) {
        physicalDamage = inphyDmg;
        plasmaDamage = inPlasmaDmg;
        fireDamage = infireDmg;
        iceDamage = iniceDmg;
        electricDamage = inelecDmg; 
    } //Setter for WeaponDamage
    public float getWeaponPhyDmg() {return physicalDamage; } //Getter for PhysicalDamage

    public float getWeaponFireDmg () { return fireDamage; } //Getter for PhysicalDamage

    public float getWeaponIceDmg () { return iceDamage; } //Getter for PhysicalDamage

    public float getWeaponPlasmaDmg () { return plasmaDamage; } //Getter for PhysicalDamage

    public float getWeaponElecDmg () { return electricDamage; } //Getter for PhysicalDamage


    public int getCurrentBullets() { return currentBullets; }//Getter for bullets in the magazine currently
    public int getMagazineSize() { return magazineSize; }//Getter for maximum magazine size of the weapon
    public int getReserveBullets() { return reserveBullets; }//Getter for current amount of Reserve bullets left
    public int getTotalBullets() { return totalBullets; }//Getter for total amount of Reserve bullets ;
    public fireType getWeaponfireType () { return currWeaponFireMode; } //Getter for Type of Weapon

    public float GetShotgunangle () { return shotSpreadangle; } //Getter for Spreadangle for shotgun
    public bool isEmpty() { return weaponEmpty; } //Getter for seeing if the weapon needs to stop firing
    public bool isReloading() { return weaponReloading; }
    public void SetReloading(bool reloadset) { weaponReloading = reloadset; }

    public int GetBurstbulletCount () { return burstCount; } //getter for getting number of bullets in a burst
    public float GetReloadTime() { return reloadTime; }
    public float GetBulletSpeed () { return bulletSpeed; }

    public float GetFireRate () { return rateOfFire; }

    public float GetEffectiveFireRate () 
    {//calculating the time in miliseconds required after a bullet is shot for the next bullet
        float effecROF = 100f / GetFireRate ();
        effecROF = effecROF / 100f;
        return effecROF;
    }
    public GameObject getBulletPrefab() {return BulletPrefab;} //Getter for accessing Bullet Prefab;
    public void Awake() 
    {
        weaponAnimator = GetComponent<Animator>();
        if (weaponAnimator == null)
            isAnimated = false;// Checking for stupiddd null execptions

        else isAnimated = true;


        currentBullets = magazineSize;

        reserveBullets = getTotalBullets();


       
        
    }


    public void OnShoot()
    { 
        switch (currWeaponType)
        {   //---------------PISTOL-------------------
            case weaponType.Pistol:
               

                 
                    if (isBurst && charged) 
                    {
                        LowerCurrentBulletCount (burstCount);
                    } 
                    else 

                    { LowerCurrentBulletCount (bulletUse); }
               
             break;

            //---------------RIFLE-------------------
            case weaponType.Rifle:

            break;


            //---------------SHOTGUN-------------------
            case weaponType.Shotgun:
               
                    LowerCurrentBulletCount (bulletUse);
                

                break;


        }
       
   }    



    private void LowerCurrentBulletCount(int decreaseCount)
    {
        currentBullets = currentBullets -decreaseCount; //lowering amount of bullets defined by decrease count
        if (currentBullets <= 0)
        {
            currentBullets = 0;// clamping  incase of negative values
            SetWeaponEmpty();
        }


        if (!hasInfiniteAmmo) 
        {
            reserveBullets = reserveBullets - decreaseCount; //also lowering the total reserve ammo each shot
            if (reserveBullets <= 0) {
                reserveBullets = 0;// clamping  incase of negative values
                SetWeaponEmpty ();
            }
        }
    }

    public void Weaponreload()
    {

        // if (isAnimated)
        // weaponAnimator.SetTrigger("Reload");
      
    }

    public void Reload()
    {
        
       
        if (reserveBullets >= magazineSize)
        {
            currentBullets = magazineSize;// Getting over stupid Exploits
            resetWeaponEmpty();

        }

        else if (reserveBullets > 0)
        {
            currentBullets = reserveBullets;// making sure player cant get more than the specified bullets
            resetWeaponEmpty();
        }

        else SetWeaponEmpty();

        weaponReloading = false;

        
    }


    public void BulletPickup(int pickedBullets)
    {

        reserveBullets = reserveBullets + pickedBullets; //adding picked bullets to the reserve ammo
        if (reserveBullets > totalBullets)
            reserveBullets = totalBullets;// clamping to not over add the max amount
    }


   

    private void SetWeaponEmpty()
    { weaponEmpty = true; }
    private   void resetWeaponEmpty()
    { weaponEmpty = false; }
}
