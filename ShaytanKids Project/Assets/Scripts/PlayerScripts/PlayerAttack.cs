using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public int attackDamage;
    public bool attacking;
    public int attackRadius;

    

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();
        if (attacking)
        {
            MeleeAttack();
        }
        
    }
    public void ReadInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
    }
    public void MeleeAttack()
    {
        for (int i = 0; i < enemySpawner.enemies.Count; i++)
        {
            if (Vector2.Distance(this.transform.position, enemySpawner.enemies[i].transform.position) <= attackRadius)
            {
                var enemyHealth = enemySpawner.enemies[i].GetComponent<EnemyHealthManager>();
                enemyHealth.TakeDamage(attackDamage);

            }

        }

    }

}
