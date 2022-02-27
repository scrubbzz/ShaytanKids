using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public List<GameObject> enemies;

    public float timer;
    public int spawnDelay;

    public Vector2 spawnRange;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        //spawnDelay = 3;
              
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        SpawnEnemies();
        spawnRange = new Vector2(Random.Range(0, 20), 4);
    }
    public void SpawnEnemies()
    {
        if (timer >= spawnDelay)
        {
            enemies.Add(Instantiate(Enemy, spawnRange, Quaternion.identity));
            Debug.Log("There are " + enemies.Count + "enemies");
    
            timer = 0;
        }
    }

}

