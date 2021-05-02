using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;


    [SerializeField]  List<QuestParent> Quests;

    public List<QuestParent> ActiveQuests;

    QuestParent currentQuest;


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

    private void SetQuestUI() 
    {
       
      
    }
}

