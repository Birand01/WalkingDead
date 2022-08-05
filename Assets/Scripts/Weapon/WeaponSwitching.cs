using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
  
    [SerializeField] TMP_Text ammoInfoText;
    private void Awake()
    {
       
        SelectWeapon();
    }
    private void Update()
    {
        RaycastWeapon currentAmmo = FindObjectOfType<RaycastWeapon>();
        ammoInfoText.text = currentAmmo.ammoSO.currentAmmo + " / " + currentAmmo.ammoSO.magazineSize;


        int previousSelected = selectedWeapon;
        if(Input.GetAxisRaw("Mouse ScrollWheel")>0f)
        {
            selectedWeapon++;
            if(selectedWeapon==3)
            {
                selectedWeapon = 0;
            }
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            selectedWeapon--;
            if(selectedWeapon==-1)
            {
                selectedWeapon = 2;
            }
        }

        if(previousSelected!=selectedWeapon)
        {
            SelectWeapon();
        }

    }

    private void SelectWeapon()
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
        transform.GetChild(selectedWeapon).gameObject.SetActive(true);
    }

}
