using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : Interactable
{

    enum DropItem {coin,ammo,healthkit,nothing };
    DropItem Item = DropItem.nothing;



    [SerializeField] int[] Weights;
    [SerializeField] GameObject[] Items;

    [SerializeField] int coinAmountMin = 20;
    [SerializeField] int coinAmountMax = 30;
    [SerializeField] int ammoAmountMin = 40;
    [SerializeField] int ammoAmountMax = 40;


    [SerializeField] GameObject LidClose;
    [SerializeField] GameObject LidOpen;


    int total;
    int randomNumber;



    // float missionCardDrop;

    private void Start () {
        DisplayAnim = DisplayUIRef.GetComponent<Animator> ();
    }


    void setDropitem () 
    {

        calcTotal ();
        randomNumber = GetRandomNumber ();
        
        
        for( int i=0; i<Weights.Length; i++) 
        { 
        
            if(randomNumber <= Weights[i]) 
            {
                
                
                GameObject g= Instantiate (Items[i], transform.position,Quaternion.identity);
                
                if(g.GetComponent<Credits_Pickable>()) 
                {

                    int credits = Random.Range (coinAmountMin, coinAmountMax);

                    g.GetComponent<Credits_Pickable> ().setCreditAmount (credits);
                
                }




                g.SetActive (true);

                g.transform.parent = null;




                Item = (DropItem)i;

                return;
            } 
            else 
            {
                randomNumber -= Weights[i];

                
            }
        
        }

    
    }

    void calcTotal() 
    {
        int temptotal = 0;

        foreach(int value in Weights) 
        {
            temptotal = temptotal + value;
        
        }



        total = temptotal;
    }

    int GetRandomNumber() 
    {
        return Random.Range (0, total);
    }


    public override void ObjPicked () {
        base.ObjPicked ();


        LidOpen.SetActive (true);
        LidClose.SetActive (false);
        setDropitem ();

        GetComponent<BoxCollider2D> ().enabled = false;
        enabled = false;

    }
}
