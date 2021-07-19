using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DisplaySelector : Damagable
{
    
    [SerializeField] protected Color[] ShirtColors;
    [SerializeField] protected Color[] PantColors;
    [SerializeField] protected Color[] ShoeColors;
    [SerializeField] protected Color[] SkinColor;


    [SerializeField] protected GameObject[] helmetArray;
    [SerializeField] protected GameObject[] armourArray;
    [SerializeField] protected GameObject[] faceAccesories;


    [SerializeField] protected SpriteRenderer[] ShirtSprites;
    [SerializeField] protected SpriteRenderer[] PantSprites;
    [SerializeField] protected SpriteRenderer[] ShoeSprite;
    [SerializeField] protected SpriteRenderer[] BodySprite;

    protected QuestParent QuestRef;
    protected AI_BehaviourParent BehaviourRef;


    [SerializeField] protected GameObject coinLoot;
    [SerializeField] protected GameObject ammoLoot;
    [SerializeField] protected int lootcoinDropmin = 0;
    [SerializeField] protected int lootcoinDropmax = 5;
    [SerializeField] protected int lootammoDropmin = 0;
    [SerializeField] protected int lootammoDropmax = 6;
    


    //[SerializeField] int levelHealthMultiplier = 1;
    
    [SerializeField] protected float HealthLevelMultiplier = 0.5f;
    [SerializeField] protected int npcLevel = 1;

    // Start is called before the first frame update
    protected void Start () {

        BehaviourRef = transform.parent.gameObject.GetComponent<AI_BehaviourParent> ();

        //Setting max health according to Enemy Level and type
        SetHealth ();

        //Setting Armour Display according to Level and type
        SetArmourSprite ();

        //Setting Random Colors to body sprites for Uniqueness
        SetClothesSprite ();



    }



    public override  void Die () {
        base.Die ();

        //Set Death Anim

        //Turnoff pathfinding


        if(QuestRef != null && QuestRef.goalType == QuestParent.GoalType.Kill) 
        {
            
            QuestRef.ProgressQuest ();

            
        
        }
            

        BehaviourRef.SetDead ();

        SpawnLoot ();
        Invoke (nameof (Delete), 1);

        GetComponent<Collider2D> ().enabled = false;


        // SpawnerRef


    }

    void SpawnLoot() 
    {
        int dropAmount=0;
        GameObject g;
        if (Random.Range(0,4) == 0) 
        {
            dropAmount = Random.Range (lootcoinDropmin, lootcoinDropmax);
            if(dropAmount != 0) 
            {   
                for (int i = 0; i<=dropAmount; i++) 
                {
                    g = Instantiate (coinLoot,transform.position,Quaternion.identity);
                    g.transform.parent = null;
                }

            }
        
        }


        else 
        { 
           dropAmount = Random.Range (lootammoDropmin, lootammoDropmax);

            if (dropAmount != 0) {
                for (int i = 0; i <= dropAmount; i++) {
                     g  =Instantiate (ammoLoot, transform.position, Quaternion.identity);
                    g.transform.parent = null;
                }

            }

        }


    }


    // Update is called once per frame
    void Update () {

    }


    protected void SetHealth () {
        //Setting Health according to level
        maxHealth = maxHealth + maxHealth * (npcLevel - 1) * HealthLevelMultiplier;


        // Setting current health again cuz i m retarded;
        currentHealth = maxHealth;


    }


    protected void SetArmourSprite () 
    {
        int RandomAcccesory = Random.Range (0, faceAccesories.Length);
        faceAccesories[RandomAcccesory].SetActive (true);

        //Selecting Enemy Helmet Sprite according to Level

        helmetArray[npcLevel - 1].SetActive (true);
        armourArray[npcLevel - 1].SetActive (true);


    }

    protected void SetClothesSprite () 
    { //Setting Shirt Color




        //Randomly Selecting Shirt and sleave Color
        int index = Random.Range (0, ShirtColors.Length);

        foreach (SpriteRenderer Shirt in ShirtSprites) {

            Shirt.color = ShirtColors[index];
        }

        //Randomly Selecting Pant Color

        index = Random.Range (0, PantColors.Length);
        foreach (SpriteRenderer Shirt in PantSprites) {

            Shirt.color = PantColors[index];
        }
        //Randomly Selecting Shoe Color

        index = Random.Range (0, ShoeColors.Length);

        foreach (SpriteRenderer Shirt in ShoeSprite) {

            Shirt.color = ShoeColors[index];
        }


        //Randomly Selecting Skin tone
        index = Random.Range (0, SkinColor.Length);

        foreach (SpriteRenderer Body in BodySprite) {
            Body.color = SkinColor[index];
        }
    }

    public virtual void SetQuest(QuestParent quest) 
    {
        QuestRef = quest;
    }

}


   

