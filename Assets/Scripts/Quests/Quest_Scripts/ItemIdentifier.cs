using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdentifier : MonoBehaviour
{
    [SerializeField] string itemName; 
    
    public string GetItemname () { return itemName; }
}
