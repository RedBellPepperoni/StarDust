using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Behaviour : AI_BehaviourParent
{

    public float minWaittime = 5f;
    public float maxWaittime = 13f;
    [SerializeField] float ViewDistance = 60f;

    GameObject CurrentChatter;

    public enum NPCType {Roamer, LabWorker, Chatter, ComputerWorker, Repairman, DefenseTarget, RescueTarget };

    [SerializeField] NPCType workType = NPCType.Roamer;

    // Start is called before the first frame update
    protected override void Start ()
    {
        base.Start ();

    }

    protected override void Awake () {
        base.Awake ();

       
    }

    // Update is called once per frame
    void Update () {

    }




    public override void SetDead () {
        currentBehaviour = AIState.Dead;

    }

    protected override void SetAIBehaviour () {
        base.SetAIBehaviour ();

        switch (currentBehaviour) {
            case AIState.Idle:



                StartCoroutine (WaitTillRoam (Random.Range (minWaittime, maxWaittime)));
               

                break;

            case AIState.Roaming:

                findWorkStation ();
                

                break;



            case AIState.Interacting:





                
                break;

            case AIState.RunningtoCover:

                break;

            case AIState.Dead:

                ai.isStopped = true;

                break;


        }
    }


    protected void findWorkStation () 
    {
        switch(workType) 
        {
            case NPCType.LabWorker:


                break;

            case NPCType.Chatter:
               
                


                break;
        }
        

       
    }

   

    void ChatWithOthers() 
    {
        Collider[] hitColliders = Physics.OverlapSphere (transform.position, ViewDistance );

        List<GameObject> NPCCollider = new List<GameObject>();
      
        foreach(var hitcollider in hitColliders) 
        {
            if (hitcollider.gameObject.tag == "NPC") 
            {

                NPCCollider.Add (hitcollider.gameObject);
            }
        
        
        }

       CurrentChatter = NPCCollider[Random.Range (0, NPCCollider.Count)];

    }

    
    public void SetWeaponReference () {





    }

    

    private void AimAtTarget (Transform aimTarget) {

        Vector3 aimDirection = (aimTarget.position - aimTransform.position).normalized;
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
            aimLocalScale.y = +1f;
            aimTransform.position = AimRootLeft.position;
        }
        aimTransform.localScale = aimLocalScale;

    }

    IEnumerator WaitTillRoam (float time) {

        yield return new WaitForSeconds (time);

       
        currentBehaviour = AIState.Roaming;


    }
    
    
   
}
