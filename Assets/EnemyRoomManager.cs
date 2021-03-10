using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomManager : MonoBehaviour
{
    

    public int Totalwaves = 1;
    int currentWave = 1;
    bool battlestarted = false;

    bool roomCleared = false;

    BoxCollider2D colliderRef;

    [SerializeField]List<Terminal_GateUnlocker> GateTerminals;

    [SerializeField] List<QuestParent> EnemyWaveQuest; 
  
    

    private void Start () 
    {
       
    }

    private void OnTriggerEnter2D (Collider2D collision) {

        if (!roomCleared && collision.gameObject.tag == "Player") 
        {

            BeginBattle ();
        
        }


    }


    void BeginBattle () 
    {
        if (!battlestarted) 
        {






           Invoke ("BeginnexttWave", 2 );
            battlestarted = true;
            LockAllDoors ();

            colliderRef.enabled = false;
            
        }
    }

    public void BeginnexttWave() 
    {
        if (currentWave <= Totalwaves) {
            EnemyWaveQuest[currentWave - 1].StartQuest ();

            currentWave++;

        } else 
        
        {
            ClearRoom ();
        }

    }


    void ClearRoom() 
    {
        roomCleared = true;
        UnlockAllDoors ();
    
    }


    void LockAllDoors() 
    { 
        foreach(Terminal_GateUnlocker g in GateTerminals) 
        {
            g.Lock ();
        }
    }

    void UnlockAllDoors () 
    { 
        foreach(Terminal_GateUnlocker g in GateTerminals) 
        {
            g.PlayerUnlock ();
        }
    
    }

}
