using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AI_Parent : MonoBehaviour
{
    private Vector3 startingPosi;


    public enum AIState { Idle, Roaming, Interacting, Chase, RunningtoCover, Dead };
    protected AIState currentBehaviour;
    public Animator animator;

    protected bool canSeetarget = false;


    [SerializeField] protected AIPath pathgen;
    [SerializeField] protected IAstarAI ai;

    [SerializeField] protected GameObject ObjectRef;
    [SerializeField] protected float attackRange = 30f;
    [SerializeField] protected Transform aimTransform;

    protected bool isMoving = false;
    [SerializeField] protected float targetRange = 100f;
    [SerializeField] protected float newPathTime = 3f;

    
    [SerializeField] protected GameObject ObjectSprite;
    


    [SerializeField] protected Transform AimRootRight;
    [SerializeField] protected Transform AimRootLeft;

    
    protected bool targetFound = false;


    protected virtual void Start () {

        ai = GetComponent<IAstarAI> ();

    }

    protected virtual void Awake () {
        pathgen = GetComponent<AIPath> ();
        pathgen.repathRate = newPathTime;
        currentBehaviour = AIState.Idle;

    }



    protected Vector2 GetRandomDir () 
    {
        return new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized;
    }


    protected Vector2 getroamPosition (Vector2 startPosi, float min = 5f, float max = 150f) {
        return startPosi + GetRandomDir () * Random.Range (min, max);
    }


    protected virtual void SetAIBehaviour () {



    }


    protected virtual void MovetorandomLocation () {
        ai.destination = getroamPosition (transform.position);

    }

    protected virtual void MoveToPosition(Vector2 _position) 
    {
        ai.destination =_position;
    }


    protected virtual void FixedUpdate () {
        SetAIBehaviour ();
        playAnim ();

        // Debug.LogWarning(currentBehaviour);
    }

    protected virtual void flip (float horizontal) {
        Vector3 playerScale = ObjectSprite.transform.localScale;
        if (horizontal < 0) {

            playerScale.x = -1;



        } else
            playerScale.x = 1;
        ObjectSprite.transform.localScale = playerScale;


    }




    public virtual void SetDead () {

        currentBehaviour = AIState.Dead;



    }

    void playAnim () {

        switch (currentBehaviour) {
            case AIState.Chase:
                animator.SetBool ("isWalking", true);

                break;
            case AIState.Idle:
                animator.SetBool ("isWalking", false);
                break;



        }
    }

}


