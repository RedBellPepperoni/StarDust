using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDispenser : MonoBehaviour
{

    [SerializeField] private GameObject Water;
    [SerializeField] GameObject WaterPour;
    [SerializeField] GameObject BrokenGlass;
    [SerializeField] GameObject Glass;
    [SerializeField] GameObject Cups;


    private void Awake()
    {
        BrokenGlass.SetActive(false);
        Glass.SetActive(true);
    }
    public void StartWater()
    {



    }

    public void BreakGlass()
    {
        BrokenGlass.SetActive(true);
        Glass.SetActive(false);

    }
}

