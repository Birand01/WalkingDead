using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private void OnEnable()
    {
        PlayerInput.OnPlayerAnim += MovementAnimation;
        PlayerInput.OnShootAnim += ShootAnimation;
        WeaponReloading.OnShootReloadAnim += ReloadAnimation;
        Scope.OnScopeEvent += ScopeAnimation;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void MovementAnimation(float horizontal,float vertical)
    {
        anim.SetFloat("Speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }
    private void ShootAnimation(bool state)
    {
        anim.SetBool("Shoot", state);
    }
    private void ReloadAnimation(bool state)
    {
        anim.SetBool("Reloading", state);
    }
    private void ScopeAnimation(bool state)
    {
        anim.SetBool("Scope", state);

    }
    private void OnDisable()
    {
        WeaponReloading.OnShootReloadAnim -= ReloadAnimation;
        PlayerInput.OnShootAnim -= ShootAnimation;
        PlayerInput.OnPlayerAnim -= MovementAnimation;
        Scope.OnScopeEvent -= ScopeAnimation;
    }
}
