using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestParent : MonoBehaviour
{//Quest Variables
    public UnityEvent QuestStart;
    public UnityEvent QuestComplete;
    


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

        if (currentAmount >= requiredAmount && ReturntoQuestGiver)
            return QuestFlow[QuestFlow.Length - 1];

        else if (currentAmount >= requiredAmount)
            return "";

        else if (QuestFlow.Length > 0)
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

        QuestStart.Invoke ();


    }

    protected virtual void QuestCompleted() 
    {
        currentState = QuestProgress.Finished;
       

        QuestComplete.Invoke ();

        Debug.LogWarning ("QuestComplete");
       
        if(SpawnerRef!=null)
        SpawnerRef.isQuestSpawner = false;


        if (!ReturntoQuestGiver) { giveReward (); }
    }


    public virtual void checkandGiveReward() 
    { 
      if(currentState == QuestProgress.Finished) 
      {
            giveReward ();
        }
    
    }

    protected virtual void giveReward() 
    {
        Gamemanager.instance.Addcoins (coinReward);
        currentState = QuestProgress.Rewarded;
        Debug.LogWarning ("QuestRewarded");

        QuestManager.instance.RemoveCompletedQuest (this);

        UIManager.instance.ResetQuestUI ();

    }

    public virtual void ProgressQuest () 
    {

        currentAmount++;


        QuestManager.instance.SetCurrentQuest (this);
        QuestManager.instance.SetQuestUI ();

        
          isQuestComplete (); 

    }

    public virtual void isQuestComplete() 
    {

        if (currentAmount >= requiredAmount) 
        {
            
            QuestCompleted ();
            
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


    protected void EndCinematic () {
        setCameraback ();
        UIManager.instance.HideCinematicUI ();
    }
}
