using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Action_Manager : MonoBehaviour
{

    public static Action_Manager instance;


    Vector2 movement;
    //button related varables
    [SerializeField] FloatingJoystick MovejoystickRef;
    bool AtkbtnisPressed = false;
    public enum MultibtnState { Grab, Shoot, Meleeatck, Drop, Heal, Talk, Examine, Charge };
 
    MultibtnState btnState = MultibtnState.Shoot;

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

    float savedAngle = 0;



    /// <summary>
    /// AutoAim_Variables
    /// </summary>
    [SerializeField] private float aimRange;
    List<GameObject> enemyinRange = new List<GameObject> ();
    [SerializeField] float delayCheck = 0.2f;
    GameObject closestEnemy;
    Vector3 enemyPos;
    bool lowergun;

    LayerMask layer = 1 << 6;



    private void Awake () {
        if (instance == null) {
            //  DontDestroyOnLoad (gameObject);
            instance = this;
        }
        initializeDependables ();
    }

    public void SetAimRange (float inAimRange) {
        aimRange = inAimRange;

    }

    void initializeDependables () {

        Manager = gameObject.GetComponent<Gamemanager> ();
        cam = Camera.main;

        vcam = PlayerCamRef.GetComponent<CinemachineVirtualCamera> ();
        


        Debug.Log (vcam);
    }


    private void Start () {
        ShowWeaponHand ();
        SetMultiBtnState ();
    }


    void Update () {
        if (PlayerController.instance.GetPlayerState () != PlayerController.PlayerState.Dead)
        //handle Input here
        {
            HandleMovement ();

            handleAiming ();
            // handleShooting ();
            //  Reload ();



        }

        if (AtkbtnisPressed) { handleShooting (); }

        if (Input.GetKey ("space")) { handleShooting (); }

        if (Input.GetKeyDown ("e")) { UseAbility (); }

    }

    public void SetMultiButtonFunc(MultibtnState state) 
    {
        btnState = state;
        SetMultiBtnState ();
    
    }

    public void MultibtnkPress () {

        switch(btnState) 
        {
            case MultibtnState.Shoot:

                AtkbtnisPressed = true;
                break;
           
        }
        
    }
    public void MultibtnRelease () {
        switch (btnState) {
            case MultibtnState.Shoot:

                AtkbtnisPressed = false;
                break;

           
        }

       
    }

    public void MultibtnClick () {

        if (btnState == MultibtnState.Grab||btnState == MultibtnState.Heal||btnState == MultibtnState.Examine||btnState== MultibtnState.Talk) {
            GameObject intObj = Gamemanager.instance.getInteObj ();

           
            if (intObj.GetComponent<Interactable> () != null) {

                

                intObj.GetComponent<Interactable> ().ObjPicked ();
            }


        } else if (btnState == MultibtnState.Drop) {
            Gamemanager.instance.DropObj ();
        }
    }


    private void FixedUpdate () 
    {
        if (PlayerController.instance.GetPlayerState () != PlayerController.PlayerState.Dead) {  //moving the physics body using the movement speed
            PlayerScriptRef.MovePlayer (movement);
        }


        if(Gamemanager.instance.getInteObj() ==null && Gamemanager.instance.CanCarryObject()) 
        { 
            
          //  btnState = MultibtnState.Shoot; 
        
        }
        // handleShooting ();
       // SetMultiBtnState ();

        
    }

    private void SetMultiBtnState() 
    {
        UIManager.instance.SetMultiBtnDisplay (btnState);
    }


    private void HandleMovement () {
        // movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = Input.GetAxisRaw("Vertical");

        // if (Input.touchCount > 0)
        // { 


        if (UIManager.instance.isCinematic == false)
            movement = MovejoystickRef.Direction;

        else
            movement = Vector2.zero;
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

    public void setZoom () {

        float zoomChange = 10f;



        zoom -= zoomChange * Time.deltaTime * 20f;


        zoom += zoomChange * Time.deltaTime * 20f;



        zoom = Mathf.Clamp (zoom, 10f, 18f);
        vcam.m_Lens.OrthographicSize = zoom;
    }


    public void flip (float horizontal) {
        Vector3 playerScale = PlayerSprite.transform.localScale;
        if (horizontal < 0) {

            playerScale.x = -1;



        } else
            playerScale.x = 1;
        PlayerSprite.transform.localScale = playerScale;


    }
   

    private void handleAiming () {

        CheckNearbyEnemies ();




        Vector3 aimLocalScale = Vector3.one;
        float angle;

        if (!lowergun) {
          
            Vector2 aimDirection = (enemyPos - aimTransform.position).normalized;



            angle = Mathf.Atan2 (aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;





            //Flip Gun if aiming at the other side



        } 
        
        else 
        {

            
           
            
            if (movement.magnitude != 0) {

                angle = Mathf.Atan2 (movement.y, movement.x) * Mathf.Rad2Deg;
                savedAngle = angle;
            }
            else 
            {
                angle = savedAngle;
            }



        }


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

        aimTransform.eulerAngles = new Vector3 (0, 0, angle);
    }


    public void handleShooting () {



        if (PlayerController.instance.GetPlayerState () != PlayerController.PlayerState.Dead)
            Manager.Onshoot ();





    }

    private void OnDrawGizmos () {
        Gizmos.DrawWireSphere (PlayerScriptRef.transform.position, aimRange);
    }

    public void HideWeaponhand () {
        aimTransform.gameObject.SetActive (false);

    }





    void CheckNearbyEnemies () {
        if (enemyinRange.Count != 0)
            enemyinRange.Clear ();

        Collider2D[] result = new Collider2D[10];
        Physics2D.OverlapCircleNonAlloc (PlayerScriptRef.transform.position, aimRange, result, layer);



        foreach (Collider2D c in result) {
            if (c != null && (c.tag == "Enemy" || c.tag == "Aimable")) {
                enemyinRange.Add (c.gameObject);



            }


        }



        ClosestEnemy ();

    }



    void ClosestEnemy () {
        float range = aimRange;

        enemyPos = Vector3.zero; ;

        if (enemyinRange.Count != 0) {
            foreach (GameObject enemyGameObject in enemyinRange) {


                float dist = Vector2.Distance (enemyGameObject.transform.position, PlayerController.instance.transform.position);


                if (dist < range) {
                    range = dist;
                    closestEnemy = enemyGameObject;
                }
            }

            if (closestEnemy != null) {
                enemyPos = closestEnemy.transform.position;

                lowergun = false;
            } else lowergun = true;
        } else {
            lowergun = true;
        }









    }


    public void ShowWeaponHand() 
    {
        aimTransform.gameObject.SetActive (true);
        PlayerController.instance.HidePlayerhand ();

    }

   
    public void UseAbility() 
    {
        PlayerController.instance.UseAbility ();
    }


    public void ActionSwapWeapons() 
    {
        Gamemanager.instance.WeaponSwitch ();
    }

}



