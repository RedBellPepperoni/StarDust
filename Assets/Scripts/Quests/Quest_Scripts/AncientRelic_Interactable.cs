using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientRelic_Interactable : QuestInteractable
{
    [SerializeField] string RelicID;


    [SerializeField] List<GameObject> RelicWeapons;

    public void OpenRelic()
    {
    
    
    }


    public override void ObjPicked () {
        base.ObjPicked ();

        Debug.LogError (RelicID);

        Gamemanager.instance.AddRelic (RelicID);
        

        Destroy (gameObject);
    }
}
