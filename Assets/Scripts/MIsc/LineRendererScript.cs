using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public LineRenderer line;
    public bool canupdate = false;


    private void Start () 
    {
        UpdateLineRender ();
    }

    private void FixedUpdate () 
    {
        if(canupdate) 
        {
            UpdateLineRender ();  
        }

    }

    public void UpdateLineRender() 
    {
        line.SetPosition (0, StartPoint.position);
        line.SetPosition (1, EndPoint.position);
    }
}
