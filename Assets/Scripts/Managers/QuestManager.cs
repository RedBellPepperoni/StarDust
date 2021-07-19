using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;


   
    [SerializeField] List<QuestManagerHelper> SavedQuests;
    public List<QuestParent> ActiveQuests;

    QuestParent currentQuest;




    public QuestParent GetCurrentQuest () { return currentQuest; }

    private void Awake () {

        if (instance == null) 
        {
            instance = this;
        }
    }


    public void AddActiveQuest(QuestParent Quest) 
    {
        ActiveQuests.Add (Quest);
    
    }


    public void RemoveCompletedQuest(QuestParent Quest)  
    {
        ActiveQuests.Remove (Quest);
    }

    public void SetCurrentQuest(QuestParent quest)
    {
        currentQuest = quest;
        SetQuestUI ();

    }

    public void SetQuestUI() 
    {


        UIManager.instance.SetQuestUI (currentQuest.GetQuestTitle (), currentQuest.GetQuestObjective ());
      
    }
}

