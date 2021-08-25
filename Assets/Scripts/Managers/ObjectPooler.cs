using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    protected GameObject prefab;

    [SerializeField]
    protected int poolSize = 10;

    [SerializeField]
    protected bool expandable = false;

    // Pool of objets that are not in use
    protected List<GameObject> freeList;

    // Pool of Objects that are in use
    protected List<GameObject> usedList;

    private void Awake () {

        freeList = new List<GameObject> ();
        usedList = new List<GameObject> ();

        for(int i = 0;i< poolSize; i++) 
        {
            GenerateNewObject ();
        }
    }


    // Start and Spawn the stuff
    protected void GenerateNewObject() 
    {
        GameObject g = Instantiate (prefab);
        g.transform.parent = transform;
        g.SetActive (false);

        freeList.Add (g);
    }

    public GameObject GetObject() 
    {
        int totalFree = freeList.Count;

        if (totalFree == 0 && !expandable) return null;
        else if (freeList.Count == 0) GenerateNewObject ();

        GameObject g = freeList [totalFree - 1];
        freeList.RemoveAt (totalFree - 1);
        usedList.Add (g);

        return g;
    
    }

    public void ReturnObject(GameObject obj) 
    {
        Debug.Assert (usedList.Contains(obj));
        obj.SetActive (false);

        usedList.Remove (obj);
        freeList.Add (obj);
    }
}
