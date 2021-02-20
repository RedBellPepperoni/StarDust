using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : AI_BehaviourParent
{
    // Start is called before the first frame update
    protected override void Start () {
        base.Start ();

        findTarget (getroamPosition (transform.position));
    }

    protected override void Awake () {
        base.Awake ();

        SetWeapon ();
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
                Invoke ("MovetorandomLocation", 0.7f);

                currentBehaviour = AIState.Roaming;

                break;

            case AIState.Roaming:


                findTarget (PlayerController.instance.transform.position);

                break;



            case AIState.Chase:
                if (PlayerController.instance.GetPlayerState () != PlayerController.PlayerState.Dead) {
                    AimAtTarget (PlayerController.instance.getAimposition());

                    chaseTarget (getroamPosition (PlayerController.instance.getposition ()));

                    if (inAttackrange ()) {

                        ai.isStopped = true;
                        // ai.SetPath (null);
                        Shoot ();


                    } else {
                        ai.isStopped = false;
                        findTarget (PlayerController.instance.transform.position);

                    }

                } else
                    currentBehaviour = AIState.Idle;

                break;

            case AIState.RunningtoCover:

                break;

            case AIState.Dead:

                ai.isStopped = true;

                break;


        }
    }


    protected void findTarget (Vector3 targetPosi) {

        if (Vector3.Distance (transform.position, targetPosi) <= targetRange) {
            //Player WithinRange
            if (!targetFound) {
                ai.destination = getroamPosition (targetPosi, 10, attackRange);
                ai.SearchPath ();

            }
            targetFound = true;

            currentBehaviour = AIState.Chase;



        } else {
            currentBehaviour = AIState.Idle;
            targetFound = false;
        }
    }

    void chaseTarget (Vector3 targetPosi) {

        if (Vector3.Distance (transform.position, targetPosi) < targetRange) {


            ai.destination = getroamPosition (targetPosi, 10, attackRange);



        } else
            currentBehaviour = AIState.Idle;
    }

    void HuntPlayer () {
        ai.isStopped = false;
        chaseTarget (getroamPosition (PlayerController.instance.getposition ()));

    }

    protected bool inAttackrange () {

        return (Vector3.Distance (transform.position, PlayerController.instance.getposition ()) < attackRange);
    }

    public void SetWeaponReference () {





    }

    public void Shoot () {
        RaycastHit2D hit;
        //  Vector3 rayDirection = PlayerController.instance.transform.position - endpointTransform.posi;

        if (!weaponScriptRef.isEmpty () && !weaponScriptRef.isReloading () && Time.time > nextFire) {
            LayerMask layerMask = ~1 << 7 | ~1 << 6;


            hit = Physics2D.Raycast (endpointTransform.position, endpointTransform.right, 100, layerMask);
            Debug.LogWarning (hit.collider);
            Debug.DrawRay (endpointTransform.position, endpointTransform.right, Color.white, 0.1f, true);
            if (hit.collider.gameObject.tag == "Player") {



                weaponScriptRef.OnShoot ();

                InstatiateBullet (endpointTransform);
            }

            nextFire = Time.time + (weaponScriptRef.GetEffectiveFireRate ());
            // else nextFire = 0f;


        } else if (weaponScriptRef.isEmpty ()) 
        {

            WeaponReload ();
        }
          

        // Debug.Log ("Weapon Reloading");
    }


    void InstatiateBullet (Transform aimGunEndPointTrasform) {
        GameObject bullet = Instantiate (weaponScriptRef.getBulletPrefab (), aimGunEndPointTrasform.position, aimGunEndPointTrasform.rotation);
        bullet.GetComponent<Weapon_Bullet> ().setDamage (weaponScriptRef.getWeaponDamage ());
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


    void WeaponReload() 
    {
        StartCoroutine (Reload(weaponScriptRef));
    }


    IEnumerator Reload (WeaponParent scriptRef) {

        yield return new WaitForSeconds (scriptRef.GetReloadTime ());

        scriptRef.Reload ();

        
        
    }
}
