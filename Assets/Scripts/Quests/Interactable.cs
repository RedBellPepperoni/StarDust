using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Collider2D))]
public class Interactable : MonoBehaviour
{

    [SerializeField]protected GameObject DisplayUIRef;
    protected Animator DisplayAnim;

     public enum Type {Interactable, Pickable, Consumable }
    [SerializeField]protected Type pickableType = Type.Interactable;

    public bool isPicked = false;

    protected bool canBePicked = false;


    private void Start () {
       DisplayAnim =  DisplayUIRef.GetComponent<Animator> ();
    }

    protected virtual void OnTriggerEnter2D (Collider2D collision)     
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.layer == 11) {
            if (Gamemanager.instance.CanCarryObject ()) 

            {


                switch (pickableType) {

                    case Type.Interactable:
                        canBePicked = true;

                        Gamemanager.instance.SetintObjRef (this.gameObject);
                        Action_Manager.instance.SetMultiButtonFunc (Action_Manager.MultibtnState.Interact);

                        break;


                    case Type.Pickable:
                        canBePicked = true;

                        Gamemanager.instance.SetintObjRef (this.gameObject);
                        Action_Manager.instance.SetMultiButtonFunc (Action_Manager.MultibtnState.Interact);

                        break;


                    case Type.Consumable:
                        ObjPicked ();


                        break;


                }


                ShowDisplayUI ();
            }
        }
    }

    protected virtual void OnTriggerExit2D (Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.layer == 11) {
            canBePicked = false;
            Gamemanager.instance.SetintObjRef (null);

            if (Gamemanager.instance.CanCarryObject ()) 
            {
                Action_Manager.instance.SetMultiButtonFunc (Action_Manager.MultibtnState.Shoot);
            }

            else
                Action_Manager.instance.SetMultiButtonFunc (Action_Manager.MultibtnState.Drop   );


           // 
            HideDisplayUI ();

        }
    }

    public virtual void ObjPicked() 
    { 
    



    
    }


    public virtual void Dropped() 
    { }


    public void Delete () {
        Destroy (this.gameObject);
    }


    protected void ShowDisplayUI() 
    {

       // if (DisplayAnim.GetBool ("Open") == false)
            DisplayAnim.SetBool ("Open",true);
    }

    protected void HideDisplayUI () 
    {
       // if (DisplayAnim.GetBool ("Open") == true)
            DisplayAnim.SetBool ("Open", false);
    }
}
