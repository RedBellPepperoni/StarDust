using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadNPC : MonoBehaviour
{
    [SerializeField] protected Color[] ShirtColors;
    [SerializeField] protected Color[] PantColors;
    [SerializeField] protected Color[] ShoeColors;
    [SerializeField] protected Color[] SkinColor;


   


    [SerializeField] protected SpriteRenderer[] ShirtSprites;
    [SerializeField] protected SpriteRenderer[] PantSprites;
    [SerializeField] protected SpriteRenderer[] ShoeSprite;
    [SerializeField] protected SpriteRenderer[] BodySprite;


    private void Start () {
        SetClothesSprite ();
    }


    protected void SetClothesSprite () { //Setting Shirt Color




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
}
