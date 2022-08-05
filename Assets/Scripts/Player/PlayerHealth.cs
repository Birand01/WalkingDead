using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable,IHealable
{
    public float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject bloodOverlay;
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] Color zeroHealthColor;
    [SerializeField] Color fullHealthColor;
    public bool isGameOver;

    public static PlayerHealth Instance { get; private set; }
    public float Health
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;

        }
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;


        healthBarSlider.value = Health = 100;
        isGameOver = false;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        SetHealthBarUI();
        BloodOverlayScreen();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        BloodOverlayScreen();
        CheckIfDead();
    }
    public void TakeHeal(float healthBooster)
    {
        currentHealth += healthBooster;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
    private void BloodOverlayScreen()
    {
        if (currentHealth <= 40)
        {
            bloodOverlay.SetActive(true);
        }
        else
        {
            bloodOverlay.SetActive(false);
        }
    }

    private void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            isGameOver = true;
        }
    }


    protected virtual void SetHealthBarUI()
    {
        float healthPercentage = CalculateHealthPercentage();
        healthBarSlider.value = healthPercentage;
        healthBarFillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, healthPercentage / 100);

    }
    protected virtual float CalculateHealthPercentage()
    {
        return ((float)Health / (float)maxHealth) * 100;
    }

    
}
