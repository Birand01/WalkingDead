using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : PickUp
{
    public delegate void OnGameWinEventHandler();
    public static event OnGameWinEventHandler OnGameWin;
    protected override void OnPickup(PlayerMovement player)
    {
        OnGameWin?.Invoke();
    }

    
}
