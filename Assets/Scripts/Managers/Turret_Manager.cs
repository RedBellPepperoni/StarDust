using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Manager : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    public GameObject turretSprite;
    Vector3 aimlocalScale = Vector3.zero;
    [SerializeField] Animator anim;

    bool canAim = true;
    bool shooting;
    public float phyDmg = 0;
    public float plasmaDmg = 0;
    public float iceDmg = 0;
    public float fireDmg = 0;
    public float elecDmg = 0;

    [SerializeField] float timetoHeat = 4f;
    [SerializeField] float timetoCool = 3f;

    public SpriteRenderer muzzle1;
    public SpriteRenderer muzzle2;

    [SerializeField] float speed = 10;

    [SerializeField] Transform aim1;
    [SerializeField] Transform aim2;

    [SerializeField] GameObject bulletPrefab;




    void aim () {


        Vector2 aimDirection = (PlayerController.instance.transform.position - aimTransform.position).normalized;



        float angle = Mathf.Atan2 (aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;



        aimTransform.eulerAngles = new Vector3 (0, 0, angle);
    }

    private void Start () {
        
    }

    private void FixedUpdate () {



        if (canAim) { aim (); }
    }



    public void shoot1 () {

        aim1.localEulerAngles = new Vector3 (0, 0, 90 + Random.Range (-10, 10));


        GameObject bullet = Instantiate (bulletPrefab, aim1);
        bullet.GetComponent<Weapon_Bullet> ().setDamage (phyDmg, plasmaDmg, fireDmg, iceDmg, elecDmg);
        bullet.GetComponent<Weapon_Bullet> ().setSpeed (speed);
        bullet.GetComponent<Weapon_Bullet> ().Move ();

    }

    public void shoot2 () {
        aim2.localEulerAngles = Vector3.zero;
        aim2.localEulerAngles = new Vector3 (0, 0, 90 + Random.Range (-10, 10));
        GameObject bullet = Instantiate (bulletPrefab, aim2);
        bullet.GetComponent<Weapon_Bullet> ().setDamage (phyDmg, plasmaDmg, fireDmg, iceDmg, elecDmg);
        bullet.GetComponent<Weapon_Bullet> ().setSpeed (speed);
        bullet.GetComponent<Weapon_Bullet> ().Move ();
    }



    void Behaviour () {



    }

   

    public void Shoot () {

        StopAllCoroutines ();
        anim.SetBool ("canShoot", true);

        StartCoroutine ("Heatup");


    }

    void StopShoot () {
        StopAllCoroutines ();

        anim.SetBool ("canShoot", false);

        StartCoroutine ("CoolDown");
    }


    IEnumerator Heatup () {
        float counter = 0;
        float step = timetoHeat / 20;
        while (counter <= timetoHeat) {

            

            yield return new WaitForSeconds (step);
            counter += step;


            muzzle1.color = new Color (muzzle1.color.r, muzzle1.color.g, muzzle1.color.b, muzzle1.color.a + 0.05f);
            
            muzzle2.color = new Color (muzzle2.color.r, muzzle2.color.g, muzzle2.color.b, muzzle2.color.a + 0.05f);

        }

        muzzle1.color = new Color (muzzle1.color.r, muzzle1.color.g, muzzle1.color.b, 1);
        muzzle2.color = new Color (muzzle2.color.r, muzzle2.color.g, muzzle2.color.b, 1);

        StopShoot ();
    }

    IEnumerator CoolDown () {
        float counter = timetoCool;
        float step = timetoCool / 20;
        while (counter >= 0) {

            

            yield return new WaitForSeconds (step);
            counter -= step;
            muzzle1.color = new Color (muzzle1.color.r, muzzle1.color.g, muzzle1.color.b,muzzle1.color.a - 0.05f );
            muzzle2.color = new Color (muzzle2.color.r, muzzle2.color.g, muzzle2.color.b,muzzle2.color.a - 0.05f);
        }

        muzzle1.color = new Color (muzzle1.color.r, muzzle1.color.g, muzzle1.color.b,0);
        muzzle2.color = new Color (muzzle2.color.r, muzzle2.color.g, muzzle2.color.b, 0);

        if (canAim)
        Shoot ();
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        
        
        if (collision.tag == "Player") {

            anim.SetBool ("Begin", true);
            
            canAim = true;

            Invoke ("Shoot",2f);
            
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.tag == "Player") {

            canAim = false;
           


            StopShoot ();

            anim.SetBool ("Begin", false);
        }
    }

    public void Damaged() 
    {
        StopAllCoroutines();
        canAim = false;

        anim.SetBool ("canShoot", false);

        anim.SetBool ("Begin", false);
    }
}

