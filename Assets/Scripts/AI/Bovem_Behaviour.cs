using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bovem_Behaviour : AI_BehaviourParent
{
    // Start is called before the first frame update

    [SerializeField] GameObject GndAtkPrefab;
    float gndAtkCooldown = 7f;
    bool inGndAtckRange = false;
    bool canGndAtk = true;

    protected override void Start () {
        base.Start ();

        findTarget (getroamPosition (transform.position));
        SetAIBehaviour ();
    }

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


       SetAIBehaviour ();
       // if (canSeetarget) 
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

    public void Shoot () {
        RaycastHit2D hit;

       


        //  Vector3 rayDirection = PlayerController.instance.transform.position - endpointTransform.posi;

        if (Time.time > nextFire) {
            LayerMask layerMask = 1 << 11;
            

            hit = Physics2D.Raycast (endpointTransform.position, endpointTransform.right, 100, layerMask);


            Debug.LogWarning (hit.collider.tag);

            if (hit.collider.gameObject.CompareTag ("Player")) {

                

                weaponScriptRef.OnShoot ();

                InstatiateBullet (endpointTransform);
            }

            nextFire = Time.time + (weaponScriptRef.GetEffectiveFireRate ());
            SetRandomPathTime ();
            // else nextFire = 0f;


        } 
          

        // Debug.Log ("Weapon Reloading");
    }


    void InstatiateBullet (Transform aimGunEndPointTrasform) {
        GameObject bullet = Instantiate (weaponScriptRef.getBulletPrefab (), aimGunEndPointTrasform.position, aimGunEndPointTrasform.rotation);
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


    


    

    public void GroundAttack() 
    {
        if (canGndAtk) {
            canGndAtk = false;

            Vector3 randompos = new Vector3(PlayerController.instance.getposition ().x, PlayerController.instance.getposition ().y,0f) + new Vector3 (Random.insideUnitCircle.x * 4, Random.insideUnitCircle.y * 4, 0f);


            GndAtkPrefab.transform.position = randompos;

            GndAtkPrefab.GetComponentInChildren<Animator> ().SetBool ("Attack", true);
            Invoke ("HideGndAtack", 3.5f);
            StartCoroutine (refreshGndAtk ());
        }
    }


    private void HideGndAtack() {
        GndAtkPrefab.GetComponentInChildren<Animator> ().SetBool ("Attack", false);
        GndAtkPrefab.transform.position = new Vector3(0,2000,0);

    }

    IEnumerator refreshGndAtk () 
    {
        yield return new WaitForSeconds (gndAtkCooldown);

        canGndAtk = true;
        if (inGndAtckRange)
            GroundAttack ();
    }

    private void OnTriggerEnter2D (Collider2D collision) {
       
        if (collision.tag == "Player")
        {
          
            inGndAtckRange  = true;

            if (canGndAtk)
                GroundAttack ();
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.tag == "Player") 
        {

            inGndAtckRange =  false;
        }
    }
}
