using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Damagable : Damagable
{
    protected QuestParent QuestRef;
    [SerializeField]protected AI_BehaviourParent BehaviourRef;


    [SerializeField] protected GameObject coinLoot;
    [SerializeField] protected GameObject ammoLoot;
    [SerializeField] protected int lootcoinDropmin = 0;
    [SerializeField] protected int lootcoinDropmax = 5;
    [SerializeField] protected int lootammoDropmin = 0;
    [SerializeField] protected int lootammoDropmax = 6;

    [SerializeField] protected float HealthLevelMultiplier = 0.5f;
    [SerializeField] protected int npcLevel = 1;

    protected void Start () {

       

        //Setting max health according to Enemy Level and type
        SetHealth ();




    }

    protected virtual void SetHealth () {
        //Setting Health according to level
        maxHealth = maxHealth + maxHealth * (npcLevel - 1) * HealthLevelMultiplier;


        // Setting current health again cuz i m retarded;
        currentHealth = maxHealth;


    }

    public virtual void SetQuest (QuestParent quest) {
        QuestRef = quest;
    }

    public override void Die () {

        if (deathEffect != null) {
            GameObject g = Instantiate (deathEffect, DeathEffectTransform);
            g.transform.parent = null;
        }


        if (QuestRef != null && QuestRef.goalType == QuestParent.GoalType.Kill) {
            
            QuestRef.ProgressQuest ();



        }
        BehaviourRef.SetDead ();

        SpawnLoot ();
        Invoke (nameof (Delete), 1);

        Delete ();

    }

    void SpawnLoot () {
        int dropAmount = 0;
        GameObject g;
        if (Random.Range (0, 4) == 0) {
            dropAmount = Random.Range (lootcoinDropmin, lootcoinDropmax);
            if (dropAmount != 0) {
                for (int i = 0; i <= dropAmount; i++) {
                    g = Instantiate (coinLoot, transform.position, Quaternion.identity);
                    g.transform.parent = null;
                }

            }

        } else {
            dropAmount = Random.Range (lootammoDropmin, lootammoDropmax);

            if (dropAmount != 0) {
                for (int i = 0; i <= dropAmount; i++) {
                    g = Instantiate (ammoLoot, transform.position, Quaternion.identity);
                    g.transform.parent = null;
                }

            }

        }


    }
}
