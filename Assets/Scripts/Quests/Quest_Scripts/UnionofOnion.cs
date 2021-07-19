using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionofOnion : QuestParent
{

    [SerializeField] Dialogue_manager DiaMan;
    [SerializeField] Transform[] Locations;
    [SerializeField] GameObject Dropzone;
    private void OnTriggerEnter2D (Collider2D collision) 
    {
        
        if(collision.gameObject.tag == "QuestItem") 
        {

            if(collision.gameObject.GetComponent<Quest_Collectable> ().QuestRef == this) 
            {
                ProgressQuest ();
                Debug.Log ("Yeet");

                collision.gameObject.GetComponent<Collider2D> ().enabled = false;

                collision.gameObject.transform.parent = Dropzone.transform;
              
            }
                
        }


    }


    public override void StartQuest () {
        base.StartQuest ();
        Debug.LogWarning ("BulbQuest Started");
        SetSpawnPrefebs ();

        SpawnerRef.SpawnObjects ();

        currentAmount = 0;
        Invoke ("setCameraback", 8);
        StartCoroutine ("ShowCargoLoc");

       
    }

    

    IEnumerator ShowCargoLoc () 
    {
        for (int i = 0; i < Locations.Length; i++) 
        {
            Gamemanager.instance.cameraLookAt (Locations[i], 25);
        yield return new WaitForSeconds (2);

        }

        StopAllCoroutines ();
    }
    

    protected override void QuestCompleted () 
    {
        base.QuestCompleted ();



        DiaMan.setCurrentDialogue (1);


        GetComponent<Collider2D> ().enabled = false;
        
    }

    

   
}
