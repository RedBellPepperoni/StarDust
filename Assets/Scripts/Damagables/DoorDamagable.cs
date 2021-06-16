using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDamagable : Damagable
{
    

    [SerializeField] DoorJamQuest Quest;
   

    public override void Die () {


        Quest.ProgressQuest ();

       


       
        base.Die ();
    }

}
