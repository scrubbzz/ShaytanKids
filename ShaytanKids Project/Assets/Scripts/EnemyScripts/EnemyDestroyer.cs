using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    public EnemySpawner enemySpawner;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyDeadEnemies();
    }
    public void DestroyDeadEnemies()
    {
        for (int i = 0; i < enemySpawner.enemies.Count; i++)
        {
            if (enemySpawner.enemies[i].GetComponent<EnemyHealthManager>().health <= 0)
            {

                Destroy(enemySpawner.enemies[i].gameObject);
                enemySpawner.enemies.RemoveAt(i); 

            }
        }
    }

}
