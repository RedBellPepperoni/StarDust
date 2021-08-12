using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;


   
    [SerializeField] List<QuestManagerHelper> SavedQuests;
    public List<QuestParent> ActiveSideQuests;
    public List<QuestParent> ActiveMainQuests;

    QuestParent currentsideQuest;
    QuestParent currentmainQuest;






    public QuestParent GetCurrentQuest () { return currentsideQuest; }

    private void Awake () {

        if (instance == null) 
        {
            instance = this;
        }
    }

    //Add the currentMain Quest to the list of main Quest
    public void AddActiveMainQuest(QuestParent Quest) 
    {
        ActiveMainQuests.Add (Quest);

    }

    public void RemoveCompletedMainQuest (QuestParent Quest) {
        ActiveMainQuests.Remove (Quest);
    }

    // Added Started Side Quests
    public void AddActivesideQuest(QuestParent Quest) 
    {
        ActiveSideQuests.Add (Quest);
    
    }


    public void RemoveCompletedSideQuest(QuestParent Quest)  
    {
        ActiveSideQuests.Remove (Quest);
    }


    //Setting Side and Main Quest References

    public void SetCurrentSideQuest(QuestParent quest)
    {
        currentsideQuest = quest;
        UIManager.instance.SetQuestSideUI (currentsideQuest.GetQuestTitle (), currentsideQuest.GetQuestObjective ());
        

    }

    public void SetCurrentMainQuest (QuestParent quest) 
    {
        currentmainQuest = quest;
        UIManager.instance.SetQuestMainUI (currentmainQuest.GetQuestTitle (), currentmainQuest.GetQuestObjective ());
       
    }



    public void ChangeMainQuestObjective(QuestParent quest) 
    {

        UIManager.instance.UpdateMainObjective (quest.GetQuestObjective ());
    }

    
    public void ChangeSideQuestObjective(QuestParent quest) 
    {
        UIManager.instance.UpdateSideObjective (quest.GetQuestObjective ());
    }

    
}

