using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Damagable
{
    [SerializeField] Turret_Manager turretmanRef;

    public override void Die () {
        base.Die ();


    }
}
