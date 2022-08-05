using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDamageable
{
    Animator anim;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if(currentHealth<=0)
        {
            StartCoroutine(ZombieDeath());
        }
    }

    IEnumerator ZombieDeath()
    {
        anim.SetTrigger("Death");
        AudioManager.instance.Play("ZombieDeath");
        this.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
       
    }
}
