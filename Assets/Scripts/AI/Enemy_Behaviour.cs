using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : AI_BehaviourParent
{
    // Start is called before the first frame update


    private bool isStopped = true;

    protected override void Start () {
        base.Start ();

        findTarget (getroamPosition (transform.position));
        Invoke("SetAIBehaviour",2.2f);
        Invoke ("setStart", 2.2f);

    }

    private void setStart() 
    { isStopped = false; }

    protected override void Awake () {
        base.Awake ();
        SetRandomPathTime ();
        SetWeapon ();
    }

    // Update is called once per frame
    void Update () {
      

    }


    void SetRandomPathTime () 
    {
        newPathTime = Random.Range (3, 10);
       
    
    }

    protected override void FixedUpdate () {

        if (!isStopped) { SetAIBehaviour (); }
        if (ai.velocity.magnitude > 0.5f) 
        {
            animator.SetBool ("isWalking", true);
           
        } else 
        {
            animator.SetBool ("isWalking",false);
        }
    }


    public override void SetDead () {
        currentBehaviour = AIState.Dead;



    }

    protected override void SetAIBehaviour () {




        


        switch (currentBehaviour) {

            case AIState.Idle:


                MovetorandomLocation ();

                currentBehaviour = AIState.Roaming;
                


                break;

            case AIState.Roaming:


                findTarget (PlayerController.instance.transform.position);

                break;



            case AIState.Chase:

                if (PlayerController.instance.GetPlayerState () != PlayerController.PlayerState.Dead) {

                   

                    canSeetarget = true;
                    
                    AimAtTarget (PlayerController.instance.getAimposition ());
        
                    chaseTarget (PlayerController.instance.getposition ());


                   

                    if (inAttackrange ()) {

                        ai.isStopped = true;


                        ai.SetPath (null);

                       
                        Shoot ();
                        

                    } else {
                        ai.isStopped = false;
                        findTarget (PlayerController.instance.transform.position);

                    }

                } 
                else { currentBehaviour = AIState.Roaming;
                   
                }

                break;

            case AIState.RunningtoCover:

                break;

            case AIState.Dead:

                ai.isStopped = true;

                break;


        }
    }


    protected void findTarget (Vector2 targetPosi) {


       
;
        if (Vector2.Distance (transform.position, targetPosi) <= targetRange) {
            //Player WithinRange


          


            if (!targetFound) {


                ai.destination = getroamPosition (targetPosi, 5f, attackRange);


                if (ai.hasPath)
                    ai.SetPath (null);


                ai.SearchPath ();



            }

            targetFound = true;


        

           currentBehaviour = AIState.Chase;
           

        } 
        
        else 
        
        {
            
            canSeetarget = false;
            targetFound = false;
            currentBehaviour = AIState.Idle;

           

        }
    }

    void chaseTarget (Vector3 targetPosi) {

        if (Vector2.Distance (transform.position, targetPosi) <= targetRange) {


            ai.destination = getroamPosition (targetPosi, 5, attackRange);



        } else
            
            currentBehaviour = AIState.Idle;
        
    }

    

    protected bool inAttackrange () {

        return (Vector2.Distance (transform.position, PlayerController.instance.getposition ()) <= attackRange);
    }

    public void SetWeaponReference () {





    }

    public void Shoot () 
    {
        RaycastHit2D hit;

        if (Time.time > nextFire) {
            LayerMask layerMask = ~1 << 7 | ~1 << 6;


            hit = Physics2D.Raycast (endpointTransform.position, endpointTransform.right, 100, layerMask);
          
          //  Debug.DrawRay (endpointTransform.position, endpointTransform.right, Color.white, 0.1f, true);
            if (hit.collider.gameObject.tag == "Player") {



                weaponScriptRef.OnShoot ();

                


                switch (weaponScriptRef.getWeaponClass ()) {

                    case WeaponParent.weaponType.Pistol:
                        InstatiateBullet (endpointTransform.position,endpointTransform.rotation.eulerAngles);
                        break;


                    case WeaponParent.weaponType.Shotgun:
                        Transform Spreadgunpoint = endpointTransform;

                        float rotOffset = weaponScriptRef.GetShotgunangle () / weaponScriptRef.GetBurstbulletCount ();

                        float newRot = -weaponScriptRef.GetShotgunangle () / 2;

                        for (int i = 1; i <= weaponScriptRef.GetBurstbulletCount (); i++) {


                            InstatiateBullet (endpointTransform.position, endpointTransform.rotation.eulerAngles + new Vector3 (0, 0, newRot));
                            newRot = newRot + rotOffset;
                        }

                        break;

                }


            }

            nextFire = Time.time + (weaponScriptRef.GetEffectiveFireRate ());
            SetRandomPathTime ();
            // else nextFire = 0f;


        } 
          

        // Debug.Log ("Weapon Reloading");
    }


    void InstatiateBullet (Vector3 aimGunEndPointPosition, Vector3 aimGunEndPointRotation ) {
        GameObject bullet = Instantiate (weaponScriptRef.getBulletPrefab (), aimGunEndPointPosition, Quaternion.Euler(aimGunEndPointRotation));
        bullet.GetComponent<Weapon_Bullet> ().setDamage (weaponScriptRef.getWeaponPhyDmg (), weaponScriptRef.getWeaponPlasmaDmg (), weaponScriptRef.getWeaponFireDmg (), weaponScriptRef.getWeaponIceDmg (), weaponScriptRef.getWeaponElecDmg ());
        bullet.GetComponent<Weapon_Bullet> ().setSpeed (weaponScriptRef.GetBulletSpeed ());
        bullet.GetComponent<Weapon_Bullet> ().Move ();
    }

    private void AimAtTarget (Vector3 aimTarget) {

        Vector3 aimDirection = (aimTarget - aimTransform.position).normalized;
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

    void SetWeapon () 
    {
        weaponScriptRef = weaponRootRef.transform.Find ("WeaponRoot").GetChild (0).gameObject.GetComponent<WeaponParent> ();
    }


    

   
}
