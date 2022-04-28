using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public int maxHealth;

    public GameObject healthBar;
    public HealthBarManager healthBarManager;

    public int healthRegenTimer = 3;
    public float currentHealthRegenTimer;
    public bool tookDamage;
    public int regenRate;

    float deathAnimLength;
    GameoverScreen gameoverScreen;
    
    void Start()
    {
        gameoverScreen = GameObject.Find("GameManager").GetComponent<GameoverScreen>();

        healthBar = GameObject.Find("HealthBar");
        healthBarManager = healthBar.GetComponent<HealthBarManager>();
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBarManager.SetMaxHealth(maxHealth);

        currentHealthRegenTimer = healthRegenTimer;

        regenRate = 7;
    }

    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(20);
            tookDamage = true;
            currentHealthRegenTimer = healthRegenTimer;
        }*/
        StartRegenTimer();
        RegenerateHealth();

        if (currentHealth <= 0){
            StartCoroutine(PlayerDeath());
        }

        healthBarManager.playerHealth.value = currentHealth;
    }

  public void TakeDamage(int damage)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyArrow")
        {
            TakeDamage(10);
            tookDamage = true;
            currentHealthRegenTimer = healthRegenTimer;
            Debug.Log("you got shot");
        }
    }

    IEnumerator PlayerDeath()
    {
        // (death animation here)
        yield return new WaitForSeconds(deathAnimLength); // wait for death animation to finish.

        gameoverScreen.GameOverSceneSwitch(); // switches to deathScene and waits for a button prompt.
    }
}
