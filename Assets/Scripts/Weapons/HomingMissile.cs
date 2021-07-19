using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Weapon_Bullet
{
    public Transform target;
    public float rotateSpeed = 200f;
    bool hastarget = false;
    protected override void Start () {
        base.Start ();

        getPlayer ();
        speed = 10f;

        hastarget = true;
    }
    void getPlayer() 
    {
        target = PlayerController.instance.transform;
    
    }

    private void FixedUpdate () {
        if (hastarget) { Move (); }
    }

    public override void Move () {


        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize ();
         float rotateAmount = Vector3.Cross (direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;

    }

}
