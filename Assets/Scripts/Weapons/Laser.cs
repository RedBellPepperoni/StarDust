using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    
    public LineRenderer line;
    public Transform firepoint;
    public Transform endpoint;
    
    public GameObject startVFX;
    public GameObject endVFX;
    [SerializeField] LayerMask hitlayermask;

    bool LaserEnabled = false;

    [SerializeField] private List<ParticleSystem> particles;
    
    [SerializeField] float DamageTimer = 0.1f;
    [SerializeField] int damage = 1;
    private float currtimer = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLaser ();
    }


    public void EnableLaser() 
    {
        
        line.enabled = true;
        foreach(ParticleSystem p in particles)
        {
            p.Play ();
        }

        LaserEnabled = true;
    }

    public void DisableLaser() 
    {
        line.enabled = false;
        foreach (ParticleSystem p in particles) {
            p.Stop ();
        }

        LaserEnabled = false;
    }

    void UpdateLaser() 
    {

        if (LaserEnabled) {
            line.SetPosition (0, firepoint.position);
            startVFX.transform.position = (Vector2)firepoint.position;
            Vector2 heading = (Vector2)endpoint.position - (Vector2)firepoint.position;




            RaycastHit2D hit = Physics2D.Raycast (firepoint.position, heading, heading.magnitude, hitlayermask);




            if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Damagable") || hit.collider.gameObject.layer == LayerMask.NameToLayer ("Player")) {
                line.SetPosition (1, hit.point);
                TryHitDamage (hit.collider.gameObject);



            } else if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Obstacle")) {
                line.SetPosition (1, hit.point);


            } else
                line.SetPosition (1, endpoint.position);



            endVFX.transform.position = line.GetPosition (1);

        }
    }

    

    void TryHitDamage(GameObject obj) 
    {

        

        if(currtimer>=DamageTimer) 
        {


            if (obj.CompareTag ("Player")) 
            {
                Player_Damagable.instance.TakeDamage (0, damage*50, 0, 0, 0);
            }
            else 
            { obj.GetComponent<Damagable> ().TakeDamage (0, damage, 0, 0, 0); }
            currtimer = 0;
        }
        currtimer = currtimer + Time.deltaTime;
    
    }
    

    private void OnTriggerEnter2D (Collider2D collision) 
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("Obstacle") )
        { 


        }
    }
}
