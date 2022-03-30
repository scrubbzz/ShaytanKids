using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public int maxHealth;

    public HealthBarManager healthBarManager;

    public int healthRegenTimer = 3;
    public float currentHealthRegenTimer;
    public bool tookDamage;
    public int regenRate;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBarManager.SetMaxHealth(maxHealth);

        currentHealthRegenTimer = healthRegenTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(20);
            tookDamage = true;
            currentHealthRegenTimer = healthRegenTimer;
        }
        StartRegenTimer();
        RegenerateHealth();
        healthBarManager.playerHealth.value = currentHealth;
    }
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarManager.SetHealth(currentHealth);
    }
    public void StartRegenTimer()
    {
        if (tookDamage)
        {
            currentHealthRegenTimer -= Time.deltaTime;
        }
       
        if(currentHealthRegenTimer <= 0)
        {
            currentHealthRegenTimer = 0;
        }
    }
    public void RegenerateHealth()
    {
        if(tookDamage && currentHealthRegenTimer == 0)
        {
            currentHealth += regenRate * Time.deltaTime;
            if(currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
                currentHealthRegenTimer = healthRegenTimer;
                tookDamage = false;
            }
        }
        
    }
}
