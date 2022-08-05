using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickUp : PickUp
{
    [SerializeField] int _ammoAmount;
    protected override void OnPickup(PlayerMovement player)
    {
        TakeAmmo takeAmmo = player.GetComponent<TakeAmmo>();
        takeAmmo?.IncreaseAmmoAmount(_ammoAmount);
    }
}

   

