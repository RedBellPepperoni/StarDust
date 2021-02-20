using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// PUBLIC VARIABLES
    /// </summary>
    public static PlayerController instance;

    [SerializeField] GameObject PlayerSprite;

    [SerializeField] List<GameObject> currWeaponList;
   
    [SerializeField] float moveSpeed = 8f;




    [SerializeField] private GameObject PlayerCamRef;
    [SerializeField] private GameObject BodyRootRef;




    private Animator PlayerAnimator;

    //Referencing The RigidBody
    Rigidbody2D rb;
    public enum PlayerState { Idle, Moving, Interacting, Dead, onehandedWeapon, twohandedWeapon };

    private PlayerState CurrentState;

    public void SetPlayerState(PlayerState state) { CurrentState = state; }
    public PlayerState GetPlayerState() {return CurrentState; }
   

    float Zoom;

    private void Awake()
    {
        setAnimator();
       
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

   

    
    public Vector3 getposition()
    { return transform.position;  }

    public Vector3 getAimposition () {

        
        
        return new Vector3 (transform.position.x, transform.position.y + 2f, transform.position.z); }


    public void MovePlayer(Vector2 movement)
   {

        

            rb.MovePosition (rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

            //checking state for animation
            if (movement.magnitude > 0) {
                CurrentState = PlayerState.Moving;
              
            } else if (movement.magnitude == 0) {
                CurrentState = PlayerState.Idle;
               
            }

        SetAnim ();
        
    }


    void setAnimator()
    {
        PlayerAnimator = BodyRootRef.GetComponentInChildren<Animator>();

    }


     void SetAnim()
    {
        switch(CurrentState) 
        {
            case PlayerState.Moving:
                PlayerAnimator.SetBool ("isWalking", true);
                break;

            case PlayerState.Idle:
                PlayerAnimator.SetBool ("isWalking", false);
                break;

            case PlayerState.Dead:
                PlayerAnimator.SetBool ("isDead", true);
                break;
        
        }

    }

    
    public void Death() 
    {
        SetAnim ();
        Action_Manager.instance.HideWeaponhand ();
    
    }
}
