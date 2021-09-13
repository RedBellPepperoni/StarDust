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
    [SerializeField] Animator displayUI;
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
    public Text RespawnText;
    public Color respawncolor;
    public Color noCoinscolor;

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
    [SerializeField] TextMeshProUGUI QuestName_Main;
    [SerializeField] TextMeshProUGUI QuestStatus_Main;
    [SerializeField] TextMeshProUGUI QuestName_Side;
    [SerializeField] TextMeshProUGUI QuestStatus_Side;

    [SerializeField] Animator aniMainQuest;
    [SerializeField] Animator aniSideQuest;



    /// <summary>
    /// Coins Variables
    /// </summary>
    [SerializeField] TextMeshProUGUI coinAmount;
    [SerializeField] TextMeshProUGUI ancientCoinAmount;

    /// <summary>
    /// Ability Variables
    /// </summary>
    [SerializeField] Image abilityImage;
    public Color abilityReadyColor;
    public Color abilityDisabledColor;


    public Image weaponIcon;

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
        HideRespawnUI ();

        
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

    public void AbilityReady(bool _ready) 
    {
        if (_ready) { abilityImage.color = abilityReadyColor; } else { abilityImage.color = abilityDisabledColor; }
    }

    public void SetMultiBtnDisplay(Action_Manager.MultibtnState state) 
    {
     
       // MultiBtnDisplay.sprite = MultibuttonSprites[(int)state];

    }

    public void ShowWeaponIcon() 
    {
        if (Gamemanager.instance.weaponScriptRef != null) { weaponIcon.sprite = Gamemanager.instance.weaponScriptRef.WeaponIcon; }
    }

    // Setting UI values for total coins
    public void SetCoinAmount(int inAmount) 
    {
        coinAmount.text = inAmount.ToString();
    
    }


    //Seting UI values for Total Ancient coins
    public void SetAncientCoinAmt(int inAmount) 
    {
        ancientCoinAmount.text = inAmount.ToString ();
    }

    /// <summary>
    /// Quest UI Functions
    /// </summary>
    

    // Animation and setting of text for main Quest


    public void SetQuestMainUI(string inQuestName, string inQuestDescrip) 
    {
        QuestName_Main.text = inQuestName;
        QuestStatus_Main.text = inQuestDescrip;

        aniMainQuest.Play ("QuestUI_NewQuest");

    }

    public void ResetQuestMainUI() 
    {
        aniMainQuest.Play ("QuestUI_QuestComplete");

        

    }

    public void UpdateMainObjective(string inQuestDescrip) 
    {
        QuestStatus_Main.text = inQuestDescrip;
    }


    public void SetQuestSideUI (string inQuestName, string inQuestDescrip) {

        
        QuestName_Side.text = inQuestName;
        QuestStatus_Side.text = inQuestDescrip;
        aniSideQuest.Play("QuestUI_NewQuest");
        



    }

    public void ResetQuestSideUI () {
       aniSideQuest.Play ("QuestUI_QuestComplete");
       



    }

    public void UpdateSideObjective (string inQuestDescrip) {

        aniSideQuest.Play ("QuestUI_UpdateDescrip");
        QuestStatus_Side.text = inQuestDescrip;
    }


    public void ShowRespawnUI() 
    {

        if(Gamemanager.instance.GetCoinAmount()>=200) 
        {
            RespawnText.color = respawncolor;
            RespawnText.text = "RESPAWN";
        }
        else 
        {
            RespawnText.color = noCoinscolor;
            RespawnText.text = "NOT ENOUGH COINS";
        }

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
        displayUI.SetBool ("Hide",Hide);

        
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
        isCinematic = true;
    
    }

    public void HideCinematicUI () 
    {
        HideMovementInputButtons (false);
        HideOtherInputButtons (false);
        HideDisplayUI (false);

        HideCinematicDisplay (true);
        isCinematic = false;
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


