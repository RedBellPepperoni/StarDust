using System;
using UnityEngine;


public class PlayerWeaponAim_mouse : MonoBehaviour
{
    [SerializeField]Camera cam;
    [SerializeField] GameObject WeaponRootRef;
    private GameObject weaponRef;
    [SerializeField]private Gamemanager Manager;
    
    private GameObject bulletPrefab;
    public Transform aimGunEndPointTrasform;
    [SerializeField]private Action_Manager ActionScriptRef;
  


    [SerializeField]private Transform aimTransform;
    
    private Animator aimAnimator;
    public GameObject getCurrentWeaponref() { return weaponRef; }

    private void Awake()
    {
       // aimTransform = transform.Find("Aim");
        
        
        //initializeWeapon();
       
        
    }

    private void Start()
    {
       // Manager.SetWeaponReference();
       // Manager.setUIvalues();
    }

    private void Update()
    {
       
    }

    



    

   

    







    /// <summary>
    /// Reference Util Functions
    /// </summary>
    /// <returns></returns>
    /// 




}



