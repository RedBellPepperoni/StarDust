using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject currentWeaponRef;
        
    [SerializeField]private GameObject currentBulletsref;
    [SerializeField]private GameObject magazineSizeref;
    [SerializeField]private GameObject currReserveref;
    [SerializeField] private GameObject totalReserveref;


    [SerializeField] private TextMeshProUGUI currHealthRef;
    [SerializeField] private TextMeshProUGUI maxHealthRef;
    [SerializeField] private Slider healthbar;


    private TextMeshProUGUI currentBullets;
    private TextMeshProUGUI magazineSize;

    private TextMeshProUGUI currentReserve;
    private TextMeshProUGUI totalReserve;
    

    public void SetcurrentWeaponref(GameObject weaponref) { currentWeaponRef = weaponref; }


    private void Awake()
    {
        currentBullets = currentBulletsref.GetComponent<TextMeshProUGUI>();
        magazineSize = magazineSizeref.GetComponent<TextMeshProUGUI>();

        currentReserve = currReserveref.GetComponent<TextMeshProUGUI>();
        totalReserve = totalReserveref.GetComponent<TextMeshProUGUI>();

        
    }


    public void setWeaponUIvalues(int bulletsinMag, int magSize, int totalBullets,int reserveBullets)
    {


        currentBullets.text = bulletsinMag.ToString();
        magazineSize.text = magSize.ToString();

        currentReserve.text = reserveBullets.ToString();
        totalReserve.text = totalBullets.ToString();

        
    }

    public void setPlayerUIvalues(float curHealth,float maxHealth) 
    {

        float healthpercent = curHealth / maxHealth;
        healthbar.value = healthpercent;

    }
}


