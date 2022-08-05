using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    [SerializeField] float maxDistance,fireRate;
    [SerializeField] ParticleSystem muzzleFlash,hitEffect,bloodEffect;
    [SerializeField] Transform raycastOrigin;
   
    Ray ray;
    RaycastHit hit;
    private bool isFiring;
    private float nextTimeToFire=0;
   
    public AmmoSO ammoSO;


    private void OnEnable()
    {
       
      
        PlayerInput.OnShoot += IsFiring;
    }

    public void IsFiring(bool state)
    {
        isFiring = state;
        if(state && Time.time>=nextTimeToFire && ammoSO.currentAmmo>0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }
        else if(ammoSO.magazineSize<0 && ammoSO.currentAmmo>0)
        {
            isFiring = false;         
            muzzleFlash.Emit(0);
        }
    }
   
   

    private void Fire()
    {
        ammoSO.currentAmmo--;
        AudioManager.instance.Play("Shoot");
        muzzleFlash.Emit(1);
        ray.direction = raycastOrigin.forward;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            if(!hit.collider.gameObject.CompareTag("Zombie"))
            {              
                HitEffect(hitEffect,hit);
            }

            BodyDamage("Zombie", hit, 20);
                     
        }
       
    }
    private void BodyDamage(string collisionName,RaycastHit hit,float damage)
    {
        if(hit.collider.gameObject.CompareTag(collisionName))
        {
            IDamageable zombieHealth = hit.transform.GetComponent<IDamageable>();
            if (zombieHealth != null)
            {
                HitEffect(bloodEffect, hit);
                zombieHealth.TakeDamage(damage);
            }
        }
        
    }

    private void HitEffect(ParticleSystem particle, RaycastHit hit)
    {
        particle.transform.position = hit.point;
        particle.transform.forward = hit.normal;
        particle.Emit(1);

    }
    private void OnDisable()
    {
        PlayerInput.OnShoot -= IsFiring;
    }
}
