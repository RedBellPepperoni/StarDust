using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTest : QuestParent
{
    public override void StartQuest () {
        base.StartQuest ();


        SpawnerRef.SpawnObjects ();

        currentAmount = 0;


        Debug.LogError ("Q U E S T Started");
    }
}
