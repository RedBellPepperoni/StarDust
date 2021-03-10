using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public enum SpawnerType { fixLocfixObj, fixLocranObj, ranLocfixObj, ranLocranObj };
    public bool isQuestSpawner;
    public QuestParent QuestRef;

    public SpawnerType currentype;

    public int numObjects;
    [SerializeField] List <Transform> spawnPoints;
  
    [SerializeField] List<GameObject> SpawnPrefabs;

   


    void Start()
    {

        isQuestSpawner = !(QuestRef == null);
       // SpawnObjects ();



    }

    public void SpawnObjects() 
    { 
        //Declaring Temporary Variable
        int numSpawn;

        int Randomspawn;
        

        //Declaring Temporary list to allow for popping out of elements so that enemies dont spawn on the same spawnPoint
        List<Transform> TempPoints = spawnPoints;


        numSpawn = numObjects;

        switch (currentype) {
            case SpawnerType.fixLocfixObj:

                for (int i = 0; i < TempPoints.Count; i++) {

                    if (spawnPoints.Count == SpawnPrefabs.Count) 
                    {
                        GameObject obj = Instantiate (SpawnPrefabs[i], TempPoints[i]);

                        if (isQuestSpawner) 
                        {

                            obj.GetComponentInChildren<AI_DisplaySelector> ().SetQuest (QuestRef);
                        }

                    }

                }

                

                break;



            case SpawnerType.fixLocranObj:

               

                foreach (Transform t in spawnPoints) 
                {
                    Randomspawn = Random.Range (0, SpawnPrefabs.Count);
                    GameObject obj = Instantiate (SpawnPrefabs [Randomspawn] , t);

                    if (isQuestSpawner) {

                        obj.GetComponentInChildren<AI_DisplaySelector> ().SetQuest (QuestRef);
                    }
                }


                break;


            case SpawnerType.ranLocfixObj:

                foreach (GameObject g in SpawnPrefabs) {
                    int tempPointIndex = Random.Range (0, TempPoints.Count);
                    GameObject obj = Instantiate (g, TempPoints[tempPointIndex]);

                    TempPoints.RemoveAt (tempPointIndex);

                    if (isQuestSpawner) {

                        obj.GetComponentInChildren<AI_DisplaySelector> ().SetQuest (QuestRef);
                    }
                }

                break;


            case SpawnerType.ranLocranObj:

                for (int i = 1; i <= numSpawn; i++) {
                    Randomspawn = Random.Range (0, SpawnPrefabs.Count);
                    int tempPointIndex = Random.Range (0, TempPoints.Count);

                    GameObject obj = Instantiate (SpawnPrefabs[Randomspawn], TempPoints[tempPointIndex]);


                    TempPoints.RemoveAt (tempPointIndex);


                    if (isQuestSpawner) {

                        obj.GetComponentInChildren<AI_DisplaySelector> ().SetQuest (QuestRef);
                    }
                }

                break;






        }



        TempPoints.Clear ();








    }


    public void SetQuestRef(QuestParent Quest) 
    {
        QuestRef = Quest;

    }
    
    public void setSpawnPrefabs(List<GameObject> inPrefabs) 
    {
        SpawnPrefabs = inPrefabs;
    
    
    }

    void Update()
    {
        
    }
}
