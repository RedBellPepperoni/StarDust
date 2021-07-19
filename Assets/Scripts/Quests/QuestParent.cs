using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestParent : MonoBehaviour
{//Quest Variables

    [SerializeField]string questName;
    [SerializeField] bool registertoQuestMaster;

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
    [SerializeField] public string title;
    [SerializeField] protected string description;
    [SerializeField] protected string[] QuestFlow;
    [SerializeField] protected int coinReward;


    [SerializeField] protected List<GameObject> Rewards;


    public string GetQuestTitle () { return title; }
    public virtual string GetQuestObjective () {

        if (currentAmount >= requiredAmount)
            return "";

        else if (QuestFlow.Length>0)
            return QuestFlow[currentAmount];

        else return "";
    }


    public string GetQuestname () { return questName; }
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

        QuestManager.instance.SetCurrentQuest (this);
        QuestManager.instance.SetQuestUI ();


    }

    protected virtual void QuestCompleted() 
    {
        currentState = QuestProgress.Finished;
        QuestManager.instance.RemoveCompletedQuest (this);

        UIManager.instance.ResetQuestUI ();
       
        if(SpawnerRef!=null)
        SpawnerRef.isQuestSpawner = false;

        giveReward ();
    }




    protected virtual void giveReward() 
    {
        Gamemanager.instance.Addcoins (coinReward);
        
    }

    public virtual void ProgressQuest () 
    {

        currentAmount++;


        QuestManager.instance.SetCurrentQuest (this);
        QuestManager.instance.SetQuestUI ();

        if (!ReturntoQuestGiver) 
         { isQuestComplete (); }

    }

    public virtual void isQuestComplete() 
    {

        if (currentAmount >= requiredAmount) 
        {
            currentState = QuestProgress.Finished;
            QuestCompleted ();
            Debug.LogError ("Q U E S T Completed");
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

    protected virtual void setCameraback () {
        Gamemanager.instance.cameraLookAt (PlayerController.instance.transform, 13);


        UIManager.instance.HideCinematicUI ();
       
    }

    protected virtual void SetCameratarget(Transform inCameratarget,int Zoom) 
    {
        Gamemanager.instance.cameraLookAt (inCameratarget, Zoom);
    }
}
