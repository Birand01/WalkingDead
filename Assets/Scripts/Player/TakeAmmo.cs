using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAmmo : MonoBehaviour
{
    public AmmoSO[] ammoSO;

    public void IncreaseAmmoAmount(int amount)
    {
        for (int i = 0; i < ammoSO.Length; i++)
        {
            ammoSO[i].magazineSize += amount;
        }
       
    }

}
