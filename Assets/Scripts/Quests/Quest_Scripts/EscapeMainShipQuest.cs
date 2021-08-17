using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EscapeMainShipQuest : QuestParent
{
    public UnityEvent UnlockTerminals;
    public UnityEvent TriggerCall;


    bool pickedRelic = false;
    [SerializeField] BoxCollider2D LevelSwitcherCollider;
    [SerializeField] QuestInteractable Terminal;
    [SerializeField] Transform Playerloc;


    public override void StartQuest () {
        base.StartQuest ();


        UIManager.instance.SetSecurityAccessDisplay (2);

        UIManager.instance.HideCinematicUI ();
        setCameraback ();
        Terminal.SetCanProgress ();
        UnlockTerminals.Invoke ();
    }



    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.CompareTag ("Player") && pickedRelic ) {

            Debug.LogError (pickedRelic);

            ProgressQuest ();

            UIManager.instance.ShowCinematicUI ();
            Action_Manager.instance.PlayerMoveStop ();
            PlayerController.instance.gameObject.transform.position = Playerloc.position;
           

            TriggerCall.Invoke ();
            Invoke ("StartLevelTransition", 1);



        }




    }


    void StartLevelTransition ()
    {
        LevelSwitcherCollider.enabled = true;
    }

    public void SetPickedRelic() 
    {
        pickedRelic = true;
    }



}
