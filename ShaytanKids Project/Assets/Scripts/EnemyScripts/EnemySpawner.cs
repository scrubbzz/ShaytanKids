using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject[] spawnLocations;
    public List<GameObject> enemies;

    public float timer;
    public int spawnDelay;

    public int spawnRange;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");
        enemies = new List<GameObject>();
        //spawnDelay = 3;
              
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        SpawnEnemies();
        spawnRange = Random.Range(0, spawnLocations.Length);
        //Debug.Log(enemies);
    }
    public void SpawnEnemies()
    {
        if (timer >= spawnDelay)
        {
            enemies.Add(Instantiate(Enemy, spawnLocations[spawnRange].transform.position, Quaternion.identity));
            //Debug.Log("There are " + enemies.Count + "enemies");
    
            timer = 0;
        }
    }
    

}

