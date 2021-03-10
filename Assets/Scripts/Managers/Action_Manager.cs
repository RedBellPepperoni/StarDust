using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Action_Manager : MonoBehaviour
{

    public static Action_Manager instance;
    

    Vector2 movement;

    [SerializeField] FixedJoystick MovejoystickRef;
    
    private Camera cam;
    CinemachineVirtualCamera vcam;
    private float zoom = 6f;
    
    /// <summary>
    /// Player Related variables
    /// </summary>
    [SerializeField] private GameObject PlayerCamRef;
    [SerializeField] private PlayerController PlayerScriptRef;
    [SerializeField] private GameObject PlayerSprite;
    private Gamemanager Manager;

    /// <summary>
    /// Aim related Variables
    /// </summary>
    [SerializeField] private Transform AimRootRight;
    [SerializeField] private Transform AimRootLeft;
    [SerializeField] private Transform aimTransform;
    public Transform aimGunEndPointTrasform;



    /// <summary>
    /// AutoAim_Variables
    /// </summary>
    [SerializeField] private float aimRange;
    List<GameObject> enemyinRange = new List<GameObject>();
    [SerializeField] float delayCheck = 0.2f;
    GameObject closestEnemy;
    Vector3 enemyPos;

    LayerMask layer = 1 << 6;

    private void Awake() {
        if (instance == null) {
          //  DontDestroyOnLoad (gameObject);
            instance = this;
        }
        initializeDependables ();
    }

    void initializeDependables()
    {

        Manager = gameObject.GetComponent<Gamemanager>();
        cam = Camera.main;

        vcam = PlayerCamRef.GetComponent<CinemachineVirtualCamera>();

       

        Debug.Log(vcam);
    }





    void Update()
    {
        if (PlayerController.instance.GetPlayerState () != PlayerController.PlayerState.Dead)
            //handle Input here
        {
            HandleMovement ();

            handleAiming ();
           // handleShooting ();
            //  Reload ();

            

        }

       
    }

    private void FixedUpdate()
    {
        if (PlayerController.instance.GetPlayerState () != PlayerController.PlayerState.Dead) 
        {  //moving the physics body using the movement speed
            PlayerScriptRef.MovePlayer (movement);
        }

       // handleShooting ();
    }


    private void HandleMovement()
    {
        // movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = Input.GetAxisRaw("Vertical");

       // if (Input.touchCount > 0)
       // { 
            
            movement = MovejoystickRef.Direction ;
/*
        Vector3 aimLocalScale = Vector3.one;
        if (movement.x < 0) {
            flip (-1);

            aimLocalScale.y = -1f;


            aimTransform.position = AimRootRight.position;
        } else if(movement.x > 0) {
            flip (1);
            aimLocalScale.y = 1f;

            aimTransform.position = AimRootLeft.position;
        }
        aimTransform.localScale = aimLocalScale;

        */

        

           // setZoom ();
       // }
    }

    public void setZoom()
    {

        float zoomChange = 10f;

        
       
            zoom -= zoomChange * Time.deltaTime * 20f;
       
        
            zoom += zoomChange * Time.deltaTime * 20f;
        


        zoom = Mathf.Clamp(zoom, 10f, 18f);
        vcam.m_Lens.OrthographicSize = zoom;
    }


    public void flip(float horizontal)
    {
        Vector3 playerScale = PlayerSprite.transform.localScale;
        if (horizontal < 0)
        {

            playerScale.x = -1;



        }

        else
            playerScale.x = 1;
        PlayerSprite.transform.localScale = playerScale;


    }
    void Reload()
    {
        
            Manager.StartReload();

        

    }

    private void handleAiming()
    {
        
        CheckNearbyEnemies ();
        
           // Vector3 mousePosition = cam.ScreenToWorldPoint (Input.mousePosition);

       


      

            Vector2 aimDirection = (enemyPos - aimTransform.position).normalized;

       

            float angle = Mathf.Atan2 (aimDirection.y,aimDirection.x) * Mathf.Rad2Deg;


            aimTransform.eulerAngles = new Vector3 (0, 0, angle);


            //Flip Gun if aiming at the other side

            Vector3 aimLocalScale = Vector3.one;
            if (angle > 90 || angle < -90) {
                flip (-1);

                aimLocalScale.y = -1f;


                aimTransform.position = AimRootRight.position;
            } else {
                flip (1);
                aimLocalScale.y = 1f;

                aimTransform.position = AimRootLeft.position;
            }
            aimTransform.localScale = aimLocalScale;

        
    }


    public void handleShooting()
    {



        if (PlayerController.instance.GetPlayerState () != PlayerController.PlayerState.Dead)
            Manager.Onshoot();





    }

    public void HideWeaponhand() 
    {
        aimTransform.gameObject.SetActive (false);

    }

    

    

    void CheckNearbyEnemies () {
        if(enemyinRange.Count!=0)
        enemyinRange.Clear ();

        Collider2D[] result = new Collider2D[10];
        Physics2D.OverlapCircleNonAlloc (PlayerScriptRef.transform.position, aimRange, result, layer);



        foreach (Collider2D c in result) {
            if (c != null && c.tag == "Enemy") {
                enemyinRange.Add (c.gameObject);



            }


        }

      

            ClosestEnemy ();
      
    }

   

    void ClosestEnemy () {
        float range = aimRange;

         enemyPos = Vector3.zero; ;

        if (enemyinRange.Count != 0) {
            foreach (GameObject enemyGameObject in enemyinRange) 
            {


                float dist = Vector2.Distance (enemyGameObject.transform.position, PlayerController.instance.transform.position);

                
                if (dist < range) {
                    range = dist;
                    closestEnemy = enemyGameObject;
                }
            }

            enemyPos = closestEnemy.transform.position;
        }

        else {
            enemyPos = Vector3.zero; 
        }

        
                    

                 
                
        
                
        
    }

}



