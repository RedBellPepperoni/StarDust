using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkEventscript : MonoBehaviour
{
    [SerializeField] BovemGroundAtk atkRef;

    public void GiveGamage() 
    {
        atkRef.giveDamage ();
    
    }
}
