using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemy : AI_Parent
{
    



    protected void findTarget (Vector2 targetPosi)
    {

        if (Vector2.Distance (transform.position, targetPosi) <= targetRange) {
            //Player WithinRange

            if (!targetFound)
            {
                ai.destination = getroamPosition (targetPosi, 5f, attackRange);
                  if (ai.hasPath)
                      ai.SetPath (null);


                      ai.SearchPath ();



            }

            targetFound = true;




            currentBehaviour = AIState.Chase;


        } else {

            canSeetarget = false;
            targetFound = false;
            currentBehaviour = AIState.Idle;



        }
    }
}
