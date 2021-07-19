using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomIdentifier : MonoBehaviour
{
    [SerializeField] string roomName;



    private void OnTriggerEnter2D (Collider2D collision) 
    {
        if (collision.CompareTag ("Player")) { EnterRoom (); }
    }

    private void OnTriggerExit2D (Collider2D collision) 
    {
        if (collision.CompareTag ("Player")) { ExitRoom (); }
    }

    void EnterRoom ()
    {
        UIManager.instance.SetRoomName (roomName);
    }

    void ExitRoom() 
    {
        UIManager.instance.SetRoomName ("");
    }

}
