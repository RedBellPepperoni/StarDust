using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_RoomEnemies : QuestParent
{
    EnemyRoomManager RoomRef;
    private void Start () {
        RoomRef = GetComponent<EnemyRoomManager> ();
    }

    public override void StartQuest () {

        currentState = QuestProgress.Started;


        SpawnerRef.SpawnObjects ();

        currentAmount = 0;


        
    }

    protected override void QuestCompleted () {
        currentState = QuestProgress.Finished;

        RoomRef.BeginnexttWave ();
    }
}
