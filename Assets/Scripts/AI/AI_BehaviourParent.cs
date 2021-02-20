using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AI_BehaviourParent : MonoBehaviour
{ private Vector3 startingPosi;


    public enum AIState { Idle, Roaming, Interacting , Chase, RunningtoCover, Dead };
    protected AIState currentBehaviour;

    
    public enum subState { Reloading, Chatting, Doingtask, Atacking }
    protected subState currentState;
    [SerializeField] protected GameObject PlayerRef;
    [SerializeField] protected float attackRange = 30f;
    [SerializeField] protected Transform aimTransform;
    protected bool isMoving = false;
    protected float nextFire = 0f;
    [SerializeField] protected float targetRange = 100f;

    [SerializeField] protected AIPath pathgen;
    
    [SerializeField] protected float newPathTime = 3f;

    [SerializeField]protected GameObject weaponRootRef;
    protected WeaponParent weaponScriptRef;
    [SerializeField]protected GameObject ObjectSprite;
    [SerializeField]protected Transform endpointTransform;


    [SerializeField] protected Transform AimRootRight;
    [SerializeField] protected Transform AimRootLeft;

    [SerializeField] protected IAstarAI ai;
    protected bool targetFound = false;
    

    protected virtual void Start () {
       
        ai = GetComponent<IAstarAI> ();
       
    }

    protected virtual void Awake()
    {
        pathgen = GetComponent<AIPath>();
       
        pathgen.repathRate = newPathTime;

        // Debug.LogError (pathgen.repathRate);


        currentBehaviour = AIState.Idle;
       
    }



    protected  Vector3 GetRandomDir()
    { return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; }


    protected Vector3 getroamPosition(Vector3 startPosi,float min = 5f, float max = 80f)
    {
        return startPosi + GetRandomDir() * Random.Range(min, max);
    }


    protected virtual void SetAIBehaviour()
    { 
    
        
    
    }

    
    


    

    protected virtual void MovetorandomLocation() 
    {
        ai.destination = getroamPosition (transform.position);
    
    }
    

    private void FixedUpdate()
    {
        SetAIBehaviour();

       // Debug.LogWarning(currentBehaviour);
    }

    protected virtual void flip(float horizontal)
    {
        Vector3 playerScale = ObjectSprite.transform.localScale;
        if (horizontal < 0)
        {

            playerScale.x = -1;



        }

        else
            playerScale.x = 1;
        ObjectSprite.transform.localScale = playerScale;


    }

    


    public virtual void SetDead() {

        currentBehaviour = AIState.Dead;

        
       
    }

}
