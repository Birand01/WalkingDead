using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtPickUp : PickUp
{
    [SerializeField] float _healAmount;
    protected override void OnPickup(PlayerMovement player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        health?.TakeHeal(_healAmount);
    }
}
