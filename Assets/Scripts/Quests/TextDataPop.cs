using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDataPop : MonoBehaviour
{
   public TextMeshPro mainText;
    public Animator textAnimator;
    public string additionalText = "";
    public void UpdateText(string _input) 
    {
        mainText.text =  string.Concat (_input,additionalText);
    }

    public void PopText() 
    {
        textAnimator.SetTrigger ("Pop");
    }
}
