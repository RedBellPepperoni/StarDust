using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject currentWeaponRef;
        
    


    [SerializeField] private TextMeshProUGUI currHealthRef;
    [SerializeField] private TextMeshProUGUI maxHealthRef;
    [SerializeField] private Slider healthbar;

    [SerializeField] GameObject RespawnScreen;


    [SerializeField] private TextMeshProUGUI currAmmo;
    [SerializeField] private TextMeshProUGUI maxAmmo;
    [SerializeField] private Slider ammoBar;

    [SerializeField] private Image abilityProg;

    [SerializeField] private Image MultiBtnDiaplay;


    [SerializeField] TextMeshProUGUI QuestName;
    [SerializeField] TextMeshProUGUI QuestStatus;


    [SerializeField] TextMeshProUGUI coinAmount;
    [SerializeField] TextMeshProUGUI ancientCoinAmount;
    

    public void SetcurrentWeaponref(GameObject weaponref) { currentWeaponRef = weaponref; }

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null) {
            // DontDestroyOnLoad (gameObject);
            instance = this;
        }


        

        
    }


    

    public void setPlayerUIvalues(float curHealth,float maxHealth) 
    {

        float healthpercent = curHealth / maxHealth;


        healthbar.value = healthpercent;


    }

    public void SetPlayerAmmoValues(float incurrAmmo,float inmaxAmmo) 
    {
        float ammoPercent = incurrAmmo / inmaxAmmo;



        ammoBar.value = ammoPercent;

        

        currAmmo.text = incurrAmmo.ToString ();
        maxAmmo.text = inmaxAmmo.ToString ();
    }

    public void AbilityProgress (float inFillamt) 
    {
        abilityProg.fillAmount = inFillamt;
    
    }

    public void SetMultiBtnDisplay(Color incolor) 
    {
        MultiBtnDiaplay.color = incolor;
    }

    public void SetCoinAmount(int inAmount) 
    {
        coinAmount.text = inAmount.ToString();
    
    }

    public void SetAncientCoinAmt(int inAmount) 
    {
        ancientCoinAmount.text = inAmount.ToString ();
    }

    public void SetQuestUI(string inQuestName, string inQuestDescrip) 
    {
        QuestName.text = inQuestName;
        QuestStatus.text = inQuestDescrip;
    }

    public void ResetQuestUI() 
    {
        QuestName.text = "";
        QuestStatus.text = "";

    }

    public void ShowRespawnUI() 
    {
        RespawnScreen.SetActive (true);


    
    }
    public void HideRespawnUI() 
    {
        RespawnScreen.SetActive (false);
    }

}


