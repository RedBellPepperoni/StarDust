using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationIDTExtChanger : MonoBehaviour
{

    [SerializeField] TextMeshPro Text;
    public string inputText;

    // Start is called before the first frame update
    void Start()
    {
        ChangeText ();
    }

    void ChangeText() 
    {
        Text.text = inputText;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
