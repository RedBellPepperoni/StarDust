using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAIMANFightQuest : QuestParent
{
    [SerializeField] Transform spawnposition;

    [SerializeField] GameObject CAIMANBoss;

    public EnemyRoomManager EnemyRoomRef;


    public override void StartQuest () {
        base.StartQuest ();

        SpawnBoss ();

    }

    void SpawnBoss() 
    {
        GameObject boss = Instantiate (CAIMANBoss, spawnposition);

        boss.GetComponentInChildren<CAIMANDamagable> ().SetQuest (this);
    }

}
