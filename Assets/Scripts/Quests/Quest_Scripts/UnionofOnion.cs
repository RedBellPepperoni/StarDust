using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnionofOnion : QuestParent
{

    


    [SerializeField] Dialogue_manager DiaMan;
    [SerializeField] Transform[] Locations;
    [SerializeField] GameObject[] DropLocations;
    [SerializeField] GameObject Dropzone;
    int dropcounter = 0;

    private void OnTriggerEnter2D (Collider2D collision) 
    {
        
        if(collision.gameObject.tag == "QuestItem") 
        {

            
            if(collision.gameObject.GetComponent<Quest_Collectable> ().QuestRef == this) 
            {
                ProgressQuest ();
                Debug.Log ("Yeet");

                Destroy (collision.gameObject);

                if (dropcounter < DropLocations.Length) {
                    DropLocations[dropcounter].SetActive (true);
                    dropcounter++;
                }
              
            }
                
        }


    }



    public override void PreStartQuest () {
        base.PreStartQuest ();

        currentAmount = 0;
        Invoke ("setCameraback", 8);
        Invoke ("startCargoLoc", 2);
    }

    public override void StartQuest () {
        base.StartQuest ();
        Debug.LogWarning ("BulbQuest Started");
       

        

       

       
    }

    
    void startCargoLoc() 
    {
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
