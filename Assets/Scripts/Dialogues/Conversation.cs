using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    [SerializeField]Conversation otherNpc;


    [SerializeField] string[] selfLines;
    [SerializeField] string[] otherLines;
     string[] TempselfLines;
   //  string[] TempotherLines;


    public void SetTempSelfLines() 
    {
        System.Array.Copy (TempselfLines, otherNpc.otherLines, otherNpc.otherLines.Length);


    }



    public string SaySelfLine(int index) 
    {
        return TempselfLines[index];
    } 

    public string SayOtherLines(int index) 
    {
        return otherLines[index];
    }
    
}
