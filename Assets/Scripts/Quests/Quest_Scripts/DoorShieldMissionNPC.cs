using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorShieldMissionNPC : NPC_StaticBEhav
{
    // Start is called before the first frame update

    private void Awake () {

       


       

    }


    public void TakeCover() 
    {
        NPCAnim.Play ("Dave_TakeCover");
    }



    public void standuptest() 
    {
        NPCAnim.Play ("Dave_CoverToIdle");
    }

}
