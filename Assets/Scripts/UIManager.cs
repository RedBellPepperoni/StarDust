using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject currentWeaponRef;


    /// <summary>
    /// Health Variables
    /// </summary>
    /// 

    public bool isCinematic;
    [SerializeField] Animator cinemabarAnim;
    

    [SerializeField] GameObject moveJoystick;
    [SerializeField] GameObject otherButtons;
    [SerializeField] GameObject displayUI;
    [SerializeField] GameObject CinematicUI;

    [SerializeField] private TextMeshProUGUI currHealthRef;
    [SerializeField] private TextMeshProUGUI maxHealthRef;
    [SerializeField] private Slider healthbar;

    [SerializeField] List<Sprite> MultibuttonSprites;

    [SerializeField] List<GameObject> SecurityAccessDisplays;


    /// <summary>
    /// Roomvariables
    /// </summary>
    [SerializeField] private TextMeshProUGUI currentRoom;
    [SerializeField] private string roomName;


    [SerializeField] GameObject RespawnScreen;

    /// <summary>
    /// Weapon Ammo Variables
    /// </summary>
    [SerializeField] private TextMeshProUGUI currAmmo;
    [SerializeField] private TextMeshProUGUI maxAmmo;
    [SerializeField] private Slider ammoBar;

    [SerializeField] private Image abilityProg;

    [SerializeField] private Image MultiBtnDisplay;

    /// <summary>
    /// Quest Variables
    /// </summary>
    [SerializeField] TextMeshProUGUI QuestName;
    [SerializeField] TextMeshProUGUI QuestStatus;



    /// <summary>
    /// Coins Variables
    /// </summary>
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

        HideSecurityAccessDisplay ();

        HideCinematicUI ();

        
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

    public void SetMultiBtnDisplay(Action_Manager.MultibtnState state) 
    {
       

        MultiBtnDisplay.sprite = MultibuttonSprites[(int)state];

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

    public void SetRoomName(string name) 
    {   
        
        if (name != "") 
        {
            roomName = name;
           
        } 
        else 
        { 
            roomName = "Lobby";
            
        }

        currentRoom.text = roomName;
    }

    void HideMovementInputButtons(bool Hide) 
    {
        moveJoystick.SetActive (!Hide);
    }

    void HideOtherInputButtons(bool Hide) 
    {
        otherButtons.SetActive (!Hide);
    }

    void HideDisplayUI(bool Hide) 
    {
        displayUI.SetActive (!Hide);
    }

    void HideCinematicDisplay(bool Hide) 
    {
       

        if(Hide) 
        {
            cinemabarAnim.SetBool ("SlideIn", false);
        }  
        else  cinemabarAnim.SetBool ("SlideIn", true);


    }
    public void ShowCinematicUI() 
    {
        HideMovementInputButtons (true);
        HideOtherInputButtons (true);
        HideDisplayUI (true);



        HideCinematicDisplay (false);
    
    }

    public void HideCinematicUI () 
    {
        HideMovementInputButtons (false);
        HideOtherInputButtons (false);
        HideDisplayUI (false);

        HideCinematicDisplay (true);
    }

    public void SetSecurityAccessDisplay(int i) 
    {
        SecurityAccessDisplays[i-1].SetActive(true);
    }
    public void HideSecurityAccessDisplay() 
    { 
       foreach(GameObject g in SecurityAccessDisplays) 
       { g.SetActive (false); }
    }

}


