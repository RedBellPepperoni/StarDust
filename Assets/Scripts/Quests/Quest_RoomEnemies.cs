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


        
        FillQuestFlow ();


        SetSpawnPrefebs ();

        SpawnerRef.SpawnObjects ();

        currentAmount = 0;

        base.StartQuest ();

        
        
    }

    protected override void QuestCompleted () {

        currentState = QuestProgress.Finished;
        Debug.LogWarning ("WAveFinished");
        RoomRef.BeginnexttWave ();

        giveReward ();

    }

    void FillQuestFlow() 
    {
        QuestFlow = new string[requiredAmount];


        QuestFlow[0] = "Kill all the Enemies";
        int i;
        for(i = 0; i <= requiredAmount - 1; i++) 
        {
            QuestFlow[i] = "Enemies left: " + (requiredAmount-i).ToString();
        
        }

        Debug.LogError (QuestFlow.Length);
    
    }
}
