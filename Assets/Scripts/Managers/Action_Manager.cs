using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Action_Manager : MonoBehaviour
{

    public static Action_Manager instance;

    Vector2 movement;


    
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



    private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad (gameObject);
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
            handleShooting ();
            Reload ();



        } 
    }

    private void FixedUpdate()
    {
        if (PlayerController.instance.GetPlayerState () != PlayerController.PlayerState.Dead) 
        {  //moving the physics body using the movement speed
            PlayerScriptRef.MovePlayer (movement);
        }
    }


    private void HandleMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        setZoom();
    }

    public void setZoom()
    {

        float zoomChange = 10f;

        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomChange * Time.deltaTime * 20f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomChange * Time.deltaTime * 20f;
        }


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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Manager.StartReload();

        }

    }

    private void handleAiming()
    {

      
          
            Vector3 mousePosition = cam.ScreenToWorldPoint (Input.mousePosition);

            Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
            float angle = Mathf.Atan2 (aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
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


    private void handleShooting()
    {


        if (Input.GetMouseButton(0))
        {

            Manager.Onshoot();

        }




    }

    public void HideWeaponhand() 
    {
        aimTransform.gameObject.SetActive (false);

    }

    
}



