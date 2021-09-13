using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

    

    public List<Waypoints> waypointStruct;

    [SerializeField] protected GameObject nPC_Root;
    [SerializeField] protected Animator nPC_anm;
    [SerializeField] protected GameObject nPCbody;

    protected int currentindex;
    Vector2 targetpos;
    Vector2 npcPos;
    float angle;

    bool canMove = false;


    private void Start () {
        npcPos = nPC_Root.transform.position;

       
    }

    public void MoveNPC (float time) {

       Invoke("MovetoWaypoint",time );

        Debug.LogError (waypointStruct[currentindex].waypointslocations[0]);

    }

    protected void MovetoWaypoint () {

       

        if (waypointStruct[currentindex].IsWaypointValid ()) 
        {
            UpdateNPCFacingDir ();
            targetpos = waypointStruct[currentindex].GetnextWaypoint ();
            canMove = true;
            nPC_anm.SetFloat ("Movement", 1);
        } 
        else 
        {

            nPC_anm.SetFloat ("Movement", 0);
            StopMove (); }
    }

    protected void StopMove ()
    { canMove = false;
        
        waypointStruct[currentindex].ReachedDestination ();
    }

    protected void FixedUpdate () {

        if (canMove) {
            nPC_Root.transform.position = Vector2.MoveTowards (nPC_Root.transform.position, targetpos, 0.15f);


          
           

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector2.Distance (nPC_Root.transform.position, targetpos) < 0.001f) {
                canMove = false;
                MovetoWaypoint ();
            }
        }
    }

    protected void UpdateNPCFacingDir()
    {
        Vector2 aimDirection = (targetpos - waypointStruct[currentindex].GetfinalPos()).normalized;



        angle = Mathf.Atan2 (aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;


        if (angle > 90 || angle < -90) {
            flip (1);
 
        } 
        else 
        {
            flip (-1);
        }

    }

    public void NPCFaceTarget(Vector2 target) 
    {
        Vector2 aimDirection = (target - (Vector2)nPC_Root.transform.position);
        angle = Mathf.Atan2 (aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (angle > 90 || angle < -90) {
            flip (1);

        } else {
            flip (-1);
        }

    }

    public void NPCFacePlayer() 
    {
        NPCFaceTarget (PlayerController.instance.transform.position);
    }



    public void flip (float horizontal) {
        Vector3 playerScale = nPCbody.transform.localScale;
        if (horizontal < 0) {

            playerScale.x = -1;



        } else
            playerScale.x = 1;

        nPCbody.transform.localScale = playerScale;


    }


    public void SetWaypoint(int _point,Vector2 _posi) 
    {
        waypointStruct[currentindex].waypointslocations[_point].position = _posi;
    }
}
