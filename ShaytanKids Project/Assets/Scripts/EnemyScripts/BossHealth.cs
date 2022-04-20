using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    Image healthBar;
    GameObject boss;
    
    // when the boss spawns in, this gameObject should be enabled.
    // otherwise this should be disabled (so the bar isn't always on screen).
    
    void Start()
    {
        healthBar = GetComponent<Image>();
        //get reference to the boss here.
    }

    void Update()
    {
        //healthBar.fillAmount = boss.currentHealth / boss.maxHealth;
    }
}
