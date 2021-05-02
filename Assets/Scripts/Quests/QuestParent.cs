using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestParent : MonoBehaviour
{//Quest Variables

    public string questName;

    public enum QuestProgress { Disabled, Started, Finished, Rewarded, Failed };

    public QuestProgress currentState = QuestProgress.Disabled;

    public bool ReturntoQuestGiver;

    public ObjectSpawner SpawnerRef;

    public enum GoalType {
        Fetch, Defense, Kill, Collect
    }


    //Quest Spawning vars
    [SerializeField]List<GameObject> SpawnPrefabs;
    [SerializeField] ObjectSpawner.SpawnerType SpawnType;
    


    // Goals variables
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;
    
    public bool IsReached () {
        return currentAmount >= requiredAmount;
    }


    //Additional Quest Variables
    [SerializeField] protected bool isRestartable;
    [SerializeField] protected string title;
    [SerializeField] protected string description;

    [SerializeField] protected int coinReward;


    [SerializeField] protected List<GameObject> Rewards;



    public bool GetcanRestart () { return isRestartable; }

    // [SerializeField] protected List<GameObject> QuestObjects;
    //  [SerializeField] protected List<Transform> TargetLocs;

    public virtual void SetSpawnPrefebs ()
    {
        SpawnerRef.numObjects = requiredAmount;
        SpawnerRef.currentype = SpawnType;
        
        SpawnerRef.QuestRef = this;
        SpawnerRef.isQuestSpawner = true;
        SpawnerRef.setSpawnPrefabs (SpawnPrefabs);
    }

    public virtual void StartQuest() 
    {
        currentState = QuestProgress.Started;
        QuestManager.instance.AddActiveQuest (this);

       
    
    }

    protected virtual void QuestCompleted() 
    {
        currentState = QuestProgress.Finished;
        QuestManager.instance.RemoveCompletedQuest (this);
        SpawnerRef.isQuestSpawner = false;

        giveReward ();
    }

/*    protected void SetQuest_Fetch() 
    { 
      for(int i=0; i<= TargetLocs.Count; i ++) 
      {
            GameObject QuestObj = Instantiate (QuestObjects[i], TargetLocs[i]);


      }

      if(ReturntoQuestGiver) goal.requiredAmount = TargetLocs.Count + 1;

      else goal.requiredAmount = TargetLocs.Count;


        goal.currentAmount = 0;
    }

    */


    protected virtual void giveReward() 
    {
        Gamemanager.instance.Addcoins (coinReward);
        Debug.LogError ("Rewarded");
    }

    public virtual void ProgressQuest () 
    {

        currentAmount++;
        
        if (!ReturntoQuestGiver) 
         { isQuestComplete (); }

    }

    public virtual void isQuestComplete() 
    {

        if (currentAmount >= requiredAmount) 
        {
            currentState = QuestProgress.Finished;
            QuestCompleted ();
           // Debug.LogError ("Q U E S T Completed");
        }

    }

    public virtual void QuestFailed () 
    {
        currentState = QuestProgress.Failed;



        if (isRestartable) 
        {
            currentState = QuestProgress.Disabled;

        }

    }
        

}
