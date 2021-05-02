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
    public float maxVelocityChange = 10.0f;
    [SerializeField] List<GameObject> currWeaponList;
   
    [SerializeField] float moveSpeed = 8f;


    [SerializeField] private GameObject Gunarm;

    [SerializeField] private GameObject PlayerCamRef;
    [SerializeField] private GameObject BodyRootRef;




    float abilityRefresh = 5f;
    bool  abilityUsable = true;

    Vector2 targetvelocity;
   

    private Animator PlayerAnimator;

    //Referencing The RigidBody
    Rigidbody2D rb;
    public enum PlayerState { Idle, Moving, Interacting, Dead, onehandedWeapon, twohandedWeapon };

    private PlayerState CurrentState = PlayerState.Idle;

    public void SetPlayerState(PlayerState state) { CurrentState = state; }
    public PlayerState GetPlayerState() {return CurrentState; }
   

    float Zoom;

    private void Awake()
    {
        setAnimator();
       
        if (instance == null)
        {
          //  DontDestroyOnLoad(gameObject);
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
        //calculating the speed and direction of motion
         targetvelocity = movement;

        targetvelocity = transform.TransformDirection (targetvelocity);
        targetvelocity *= moveSpeed;

        //   rb.MovePosition (rb.position + movement * moveSpeed * Time.fixedDeltaTime);


        //adding required forces to move
        Vector2 velocity = rb.velocity;
        Vector2 velocityChange;
        velocityChange = (targetvelocity - velocity);

        velocityChange.x = Mathf.Clamp (velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = Mathf.Clamp (velocityChange.y, -maxVelocityChange, maxVelocityChange);

        rb.AddForce (velocityChange, ForceMode2D.Impulse);

        //checking state for animation
        SetWalkAnim (movement.magnitude);
           

        

        //SetAnim ();
        
    }


    void setAnimator()
    {
        PlayerAnimator = BodyRootRef.GetComponentInChildren<Animator>();

    }


     void SetWalkAnim(float movementMag)
    {

        if (Gamemanager.instance.CanCarryObject ()) 
        {
            PlayerAnimator.SetBool ("isCarrying", false);
        } 
        
        
        else 
        {
            PlayerAnimator.SetBool ("isCarrying", true);
        }



        PlayerAnimator.SetFloat ("Movement", movementMag);
    }

    

    
    public void Death() 
    {
        PlayerAnimator.SetBool ("isDead", true);
        //  Gunarm.SetActive (true);
        Action_Manager.instance.HideWeaponhand ();
        ShowPlayerhand ();
    }


    public void HidePlayerhand () 
    {
     //  Gunarm.SetActive (false);
        
    }

    public void ShowPlayerhand () 
    {
       Gunarm.SetActive (true);

    }


     void Dash() 
    {
        
        rb.AddForce (targetvelocity.normalized *400, ForceMode2D.Impulse);
    }

    IEnumerator RefreshAbility() 
    {

        float counter = 0f;
        float UIcounter = 0f;
        float counterStep = abilityRefresh / 10f;
        while (counter < abilityRefresh) {

          
            
            yield return new WaitForSeconds (counterStep);
            counter = counter + counterStep;

            UIcounter = UIcounter + 0.1f;
            UIManager.instance.AbilityProgress (UIcounter);
        }

        abilityUsable = true;
        

    }

    public void UseAbility() 
    {

        if (abilityUsable) {
            Dash ();
            abilityUsable = false;

            UIManager.instance.AbilityProgress (0);
            StartCoroutine ("RefreshAbility");

        }
    
    }

}






