using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaiderSelecter : AI_DisplaySelector
{
    
    
    

    //[SerializeField] int levelHealthMultiplier = 1;
    public enum raiderType { Pistoler, Shotgunner};
    [SerializeField] raiderType type;
   
    // Start is called before the first frame update
   

    public override void Die () {
        base.Die ();

        //Set Death Anim

        //Turnoff pathfinding

        


        // SpawnerRef


    }


    // Update is called once per frame
    void Update()
    {
        
    }


    
    
}

