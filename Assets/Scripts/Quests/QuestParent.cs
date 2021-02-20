using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestParent : MonoBehaviour
{
    public enum QuestProgress {Disabled, Started, Finished, Rewarded,Failed };
   
    public QuestProgress currentState = QuestProgress.Disabled;
    
    public bool ReturntoQuestGiver;

    public ObjectSpawner SpawnerRef;

    public enum GoalType
    {
        Fetch, Defense, Kill
    }

    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached () {
        return currentAmount >= requiredAmount;
    }
    [SerializeField] protected bool isRestartable;
    [SerializeField] protected string title;
    [SerializeField] protected string description;

    [SerializeField] protected int coinReward;
    

    [SerializeField] protected List<GameObject> Rewards;



    public bool GetcanRestart () { return isRestartable; }

   // [SerializeField] protected List<GameObject> QuestObjects;
  //  [SerializeField] protected List<Transform> TargetLocs;

    

    public virtual void StartQuest() 
    {
        currentState = QuestProgress.Started;
        QuestManager.instance.AddActiveQuest (this);

       
    
    }

    protected void QuestCompleted() 
    {
        currentState = QuestProgress.Finished;
        QuestManager.instance.RemoveCompletedQuest (this);
    
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


    public virtual void ProgressQuest () 
    {

        currentAmount++;
        Debug.LogError ("curramoutn  " + currentAmount);
        isQuestComplete ();

    }

    protected void isQuestComplete() 
    {

        if (currentAmount >= requiredAmount) 
        {
            currentState = QuestProgress.Finished;
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
        

}
