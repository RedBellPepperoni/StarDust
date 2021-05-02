using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager instance;
    // [SerializeField] Dialogue[] Dialogues;

    public Dialogue_manager dialogueMan;

    public Dialogue Dialogref;
    string Charactername = "Bea";


    private int index = 0;
    public float typingSpeed;

    public TextMeshProUGUI speakerName, dialogue;

    [SerializeField] Animator anim;




    private void Awake () {
        if (instance == null) {
            instance = this;

        } else
            Destroy (gameObject);
    }

    private void Start () {
        //StartCoroutine (Type ());

        anim = GetComponent<Animator> ();
    }


    public void SetDialoguemanagerReference (Dialogue_manager reference) {
       
        dialogueMan = reference;
        index = 0;

        if (dialogueMan != null) {
            SetcurrDialogueReference ();
            Charactername = dialogueMan.charName;

            DialogueStart ();
        } else {
            ClearcurrDialogueRef ();
            DialogueEnd ();
        }

    }

    public void SetcurrDialogueReference () {
        Dialogref = dialogueMan.Dialogref;
    }

    public void ClearcurrDialogueRef () {
        Dialogref = null;
    }

    IEnumerator Type () {
        setSpeakername ();

        foreach (char letter in Dialogref.questSentences[index].ToCharArray ()) {
            dialogue.text += letter;
            yield return new WaitForSeconds (typingSpeed);

        }
    }


    public void DialogueStart () {
        anim.SetTrigger ("Open");

    }

    public void DialogueEnd () {
        if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Dialogue_Idle")) { anim.SetTrigger ("Close"); }
    }

    public void Nextsentence () {
        if (index <= Dialogref.questSentences.Length - 1) {

            dialogue.text = "";
            StopAllCoroutines ();
            StartCoroutine (Type ());
            index++;

        } else {
            index = 0;
            dialogue.text = "";

            dialogueMan.QuestDialogueCompleted ();
            DialogueEnd ();
        }
    }

    


    void setSpeakername() 
    {
        speakerName.text = Charactername;


    }
}
