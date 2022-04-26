using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject bizzaro;
    public GameObject shaytanKid;
    public GameObject archangel;
    public GameObject[] kidSpawnLocations;
    public GameObject[] archangelSpawnLocations;
    public GameObject[] bizzaroSpawnLocations;
    public List<GameObject> enemies;
    public List<GameObject> shaytanKids;
    public List<GameObject> archangels;

    public float timer;
    public int spawnDelay;

    public int kidSpawnRange;
    public int bizzaroSpawnRange;
    public int angelSpawnRange;

    public bool canSpawn;
    public int spawnCount;
    public int spawnLimit;

    // Start is called before the first frame update
    void Start()
    {
        kidSpawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");
        archangelSpawnLocations = GameObject.FindGameObjectsWithTag("AngelSpawn");
        bizzaroSpawnLocations = GameObject.FindGameObjectsWithTag("BizzaroSpawn");
        enemies = new List<GameObject>();
        //spawnDelay = 3;
        spawnLimit = 5;
              
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawnCount();
        timer += Time.deltaTime;
        if (canSpawn)
        {
            SpawnEnemies();
        }
        
        kidSpawnRange = Random.Range(0, kidSpawnLocations.Length);
        bizzaroSpawnRange = Random.Range(0, bizzaroSpawnLocations.Length);
        angelSpawnRange = Random.Range(0, archangelSpawnLocations.Length);
        //Debug.Log(enemies);
    }
    public void CheckSpawnCount()
    {
        if(spawnCount < spawnLimit)
        {
            canSpawn = true;
        }
        else
        {
            canSpawn = false;
        }
    }
    public void SpawnEnemies()
    {
        if (timer >= spawnDelay)
        {
            //enemies.Add(Instantiate(bizzaro, bizzaroSpawnLocations[bizzaroSpawnRange].transform.position, Quaternion.identity));
            enemies.Add(Instantiate(shaytanKid, kidSpawnLocations[kidSpawnRange].transform.position, Quaternion.identity));
            //enemies.Add(Instantiate(archangel, archangelSpawnLocations[angelSpawnRange].transform.position, Quaternion.identity));
            shaytanKids.Add(shaytanKid);
            //archangels.Add(archangel);

            //Debug.Log("There are " + enemies.Count + "enemies");
            spawnCount++;
            timer = 0;
        }
    }
    

}

