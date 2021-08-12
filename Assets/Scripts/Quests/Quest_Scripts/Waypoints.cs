using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Waypoints : MonoBehaviour
{
    public List<Transform> waypointslocations;
    public UnityEvent ReachedDesti;
    public UnityEvent Startmove;
    private int index = 0;


    public void StartMove() 
    {
        Startmove.Invoke ();
    }

    public Vector2 GetnextWaypoint() 
    {
        Vector2 loc =  waypointslocations[index].position;
        index++;

        return loc;
    }

    public void ReachedDestination () 
    {
        ReachedDesti.Invoke ();
    }


    public bool IsWaypointValid() 
    {
        return index < waypointslocations.Count;
    }

    public Vector2 GetfinalPos() 
    {
        return waypointslocations[waypointslocations.Count - 1].position;
    }
}
