using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestParent : MonoBehaviour
{//Quest Variables
    public UnityEvent QuestStart;
    public UnityEvent QuestComplete;

    protected bool isMainQuest;
    public float questStartTimer = 1;
    public float questRewardTimer = 1;

   
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


    public virtual void PreStartQuest() 
    {
        Invoke ("StartQuest", questStartTimer);
    
    }


    public virtual void StartQuest() 
    {
        currentState = QuestProgress.Started;


        if (isMainQuest) {
            QuestManager.instance.AddActiveMainQuest (this);

            QuestManager.instance.SetCurrentMainQuest (this);
            
        } 
        else 
        {
            QuestManager.instance.AddActivesideQuest (this);

            QuestManager.instance.SetCurrentSideQuest (this);
        }

        Debug.LogError ("Started");
        

        QuestStart.Invoke ();


    }

    protected virtual void QuestCompleted() 
    {
        currentState = QuestProgress.Finished;
       

        QuestComplete.Invoke ();

        Debug.LogWarning ("QuestComplete");
       
        if(SpawnerRef!=null)
        SpawnerRef.isQuestSpawner = false;


        if (!ReturntoQuestGiver) { Invoke("giveReward",questRewardTimer); }
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

        if(isMainQuest) 
        {
            QuestManager.instance.RemoveCompletedMainQuest (this);

            UIManager.instance.ResetQuestMainUI ();

        }
        else 
        {
            QuestManager.instance.RemoveCompletedSideQuest (this);

            UIManager.instance.ResetQuestSideUI ();
        }

        

    }

    public virtual void ProgressQuest () 
    {

        currentAmount++;

        if (isMainQuest) {
            QuestManager.instance.ChangeMainQuestObjective (this);
        } 
        else 
        {
            QuestManager.instance.ChangeSideQuestObjective (this);
        }

        
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
