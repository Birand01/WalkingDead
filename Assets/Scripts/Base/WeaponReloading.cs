using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloading : MonoBehaviour
{
    public delegate void OnShootReloadingAnimHandler(bool state);
    public static event OnShootReloadingAnimHandler OnShootReloadAnim;

    public AmmoSO ammoSO;
    private bool isReloading;
    private float reloadTime = 2f;
    protected  void Start()
    {
        ammoSO.currentAmmo = ammoSO.maxAmmo;
    }
    protected virtual void OnEnable()
    {
        isReloading = false;
        OnShootReloadAnim?.Invoke(false);
    }

    protected void Update()
    {
        Reloading();
        if (isReloading)
            return;
    }

    private void Reloading()
    {
        if (ammoSO.currentAmmo == 0 && ammoSO.magazineSize > 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        OnShootReloadAnim?.Invoke(true);
        yield return new WaitForSeconds(reloadTime);
        OnShootReloadAnim?.Invoke(false);
        if (ammoSO.magazineSize >= ammoSO.maxAmmo)
        {
            ammoSO.currentAmmo = ammoSO.maxAmmo;
            ammoSO.magazineSize -= ammoSO.maxAmmo;
        }
        else
        {
            ammoSO.currentAmmo = ammoSO.magazineSize;
            ammoSO.magazineSize = 0;
        }
        isReloading = false;
    }

}
