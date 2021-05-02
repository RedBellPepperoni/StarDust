using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionofOnion : QuestParent
{

    [SerializeField] Dialogue_manager DiaMan;


    private void OnTriggerEnter2D (Collider2D collision) 
    {
        
        if(collision.gameObject.tag == "QuestItem") 
        {

            if(collision.gameObject.GetComponent<Quest_Collectable> ().QuestRef == this) 
            {
                ProgressQuest ();
                Debug.Log ("Yeet");

                collision.gameObject.GetComponent<Collider2D> ().enabled = false;
            }
                
        }


    }


    public override void StartQuest () {
        base.StartQuest ();
        Debug.LogWarning ("BulbQuest Started");
        SetSpawnPrefebs ();

        SpawnerRef.SpawnObjects ();

        currentAmount = 0;

    }

    

    protected override void QuestCompleted () {
        base.QuestCompleted ();



        DiaMan.setCurrentDialogue (1);
        
    }

}
