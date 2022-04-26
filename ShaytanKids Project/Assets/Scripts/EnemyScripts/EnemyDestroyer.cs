using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    public GameObject theEnemySpawner;
    public EnemySpawner enemySpawner;
    public float timer = 3;
    public bool enemyDied;
    public bool kidDied;
   
    // Start is called before the first frame update
    void Start()
    {
        theEnemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        enemySpawner = theEnemySpawner.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            enemyDied = false;
            kidDied = false;
        }
        DestroyDeadEnemies();
    }
    public void DestroyDeadEnemies()
    {
        for (int i = 0; i < enemySpawner.enemies.Count; i++)
        {
            if(enemySpawner.enemies[i] != null)
            {
                if (enemySpawner.enemies[i].GetComponent<EnemyHealthManager>().health <= 0)
                {

                    Destroy(enemySpawner.enemies[i].gameObject);
                    enemySpawner.enemies.RemoveAt(i);
                    enemySpawner.spawnCount--;
                    enemyDied = true;
                    timer = 0.23f;


                }
            }
            
        }

        for (int i = 0; i < enemySpawner.shaytanKids.Count; i++)
        {
            if(enemySpawner.shaytanKids[i].GetComponent<EnemyHealthManager>().health <= 0)
            {
                kidDied = true;
                timer = 0.23f;
            }
        }
    }


}
