using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAIMANFightQuest : QuestParent
{
    [SerializeField] Transform spawnposition;

    [SerializeField] GameObject CAIMANBoss;
    [SerializeField] Transform[] CameraTargets;

    public EnemyRoomManager EnemyRoomRef;


    public override void StartQuest () {
        base.StartQuest ();

        SpawnBoss ();

        SetCameratarget(PlayerController.instance.transform, 25);

    }

    void SpawnBoss() 
    {
        GameObject boss = Instantiate (CAIMANBoss, spawnposition);

        boss.GetComponentInChildren<CAIMANDamagable> ().SetQuest (this);
    }

    protected override void giveReward () {
        base.giveReward ();

        setCameraback ();
    }
}
